using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Character : StateMachine
{
    [Header("Al recibir daño")]
    [SerializeField] protected float tiempoAturdido;

    [Header("Seguimiento de objetos")]
    [SerializeField] protected Estado estadoAlEncontrarObjetivo;
    [SerializeField] protected Estado estadoAlPerderObjetivo;

    public UnityAction OnDead;

    protected Vida vida;
    protected Movement movement;
    protected Esperar aturdimiento;

    protected List<IObjectTracker> trackers = new List<IObjectTracker>();

    public Movement MovementComponent { get => movement; }

    protected override void Awake()
    {
        base.Awake();
        IObjectTracker[] _trackers = GetComponents<IObjectTracker>();
        trackers = _trackers.ToList();
        movement = GetComponent<Movement>();
        aturdimiento = GetComponent<Esperar>();
        vida = GetComponentInChildren<Vida>();
        
    }
    protected void OnEnable()
    {
        if (vida)
        {
            vida.NoHealth += Dead;
            vida.Damaged += OnDamageReceived;
        }
    }

    protected void OnDisable()
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

    protected virtual void Dead()
    {
        if (movement)
        {
            movement.Direction = Vector2.zero;
        }
        OnDead?.Invoke();
        this.enabled = false;
    }

    protected void OnDamageReceived()
    {
        estadoActual?.DañoRecibido();
        if (aturdimiento && tiempoAturdido > 0)
        {
            aturdimiento.Tiempo = tiempoAturdido;
            CambiarEstado(aturdimiento);
        }
    }
}
