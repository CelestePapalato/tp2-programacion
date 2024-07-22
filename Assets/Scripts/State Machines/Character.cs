using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Character : StateMachine
{
    [Header("Al recibir daño")]
    [SerializeField] float tiempoAturdido;

    [Header("Seguimiento de objetos")]
    [SerializeField] Estado estadoAlEncontrarObjetivo;
    [SerializeField] Estado estadoAlPerderObjetivo;

    public UnityAction OnDead;

    Vida vida;
    Movement movement;
    Esperar aturdimiento;

    List<IObjectTracker> trackers = new List<IObjectTracker>();

    public Movement MovementComponent { get; }

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
            vida.Damaged += OnDamageReceived;
        }
    }

    private void OnDisable()
    {
        if (vida)
        {
            vida.NoHealth -= Dead;
            vida.Damaged -= OnDamageReceived;
        }
    }

    public void TargetUpdate(Transform newTarget)
    {
        foreach (IObjectTracker tracker in trackers)
        {
            tracker.Target = newTarget;
        }

        if (newTarget)
        {
            CambiarEstado(estadoAlEncontrarObjetivo);
        }
        else
        {
            CambiarEstado(estadoAlPerderObjetivo);
        }
    }

    private void Dead()
    {
        if (movement)
        {
            movement.Direction = Vector2.zero;
        }
        OnDead?.Invoke();
        this.enabled = false;
    }

    private void OnDamageReceived()
    {
        estadoActual?.DañoRecibido();
        if (aturdimiento && tiempoAturdido > 0)
        {
            aturdimiento.Tiempo = tiempoAturdido;
            CambiarEstado(aturdimiento);
        }
    }
}
