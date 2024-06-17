using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemigo : StateMachine
{
    public static UnityAction<int> OnDead;

    [SerializeField] Estado estadoAlEncontrarObjetivo;
    [SerializeField] Estado estadoAlPerderObjetivo;

    [Header("Game Manager")]
    [SerializeField] int puntos;

    Vida vida;

    List<IObjectTracker> trackers = new List<IObjectTracker>();
    protected override void Awake()
    {
        base.Awake();
        IObjectTracker[] _trackers = GetComponents<IObjectTracker>();
        trackers = _trackers.ToList();
        vida = GetComponent<Vida>();
        if (!vida)
        {
            vida = GetComponentInChildren<Vida>();
        }
    }

    private void OnEnable()
    {
        if (vida)
        {
            vida.NoHealth += Dead;
        }
    }

    private void OnDisable()
    {
        if(vida)
        {
            vida.NoHealth -= Dead;
        }
    }

    public void TargetUpdate(Transform newTarget)
    {
        foreach (IObjectTracker tracker in trackers)
        {
            tracker.Target = newTarget;
        }

        if(newTarget) {
            CambiarEstado(estadoAlEncontrarObjetivo);
        }
        else
        {
            CambiarEstado(estadoAlPerderObjetivo);
        }
    }

    public override void CambiarEstado(Estado estado)
    {
        base.CambiarEstado(estado);
    }

    private void Dead()
    {
        OnDead?.Invoke(puntos);
        Destroy(gameObject);
    }
}
