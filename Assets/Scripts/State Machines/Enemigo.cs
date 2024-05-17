using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemigo : StateMachine
{
    [SerializeField] Estado estadoAlEncontrarObjetivo;
    [SerializeField] Estado estadoAlPerderObjetivo;

    [Header("Game Manager")]
    [SerializeField] int puntos;

    List<IObjectTracker> trackers = new List<IObjectTracker>();
    protected override void Awake()
    {
        base.Awake();
        IObjectTracker[] _trackers = GetComponents<IObjectTracker>();
        trackers = _trackers.ToList();
        Vida vida = GetComponent<Vida>();
        if (!vida)
        {
            vida = GetComponentInChildren<Vida>();
        }
        vida.NoHealth += OnDead;
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

    private void OnDead()
    {
        GameManager.SubirPuntuacion(puntos);
        Destroy(gameObject);
    }
}
