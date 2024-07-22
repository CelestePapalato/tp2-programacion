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
    protected Animator animator;
    protected Damage damage;

    protected List<IObjectTracker> trackers = new List<IObjectTracker>();

    public Movement MovementComponent { get => movement; }
    public Vida VidaComponent { get => vida; }
    public Animator AnimatorComponent { get => animator; }
    public Damage DamageComponent { get => damage; }

    protected override void Start()
    {
        IObjectTracker[] _trackers = GetComponents<IObjectTracker>();
        trackers = _trackers.ToList();
        movement = GetComponent<Movement>();
        aturdimiento = GetComponent<Esperar>();
        animator = GetComponentInChildren<Animator>();
        damage = GetComponentInChildren<Damage>();
        vida = GetComponentInChildren<Vida>();
        base.Start();
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

    protected override void Update()
    {
        base.Update();
        if (animator)
        {
            Vector2 speed = movement.RigidbodyComponent.velocity;
            animator.SetFloat("Speed X", Mathf.Abs(speed.x));
            animator.SetFloat("Speed Y", speed.y);
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
        animator?.SetTrigger("Damage");
        estadoActual?.DañoRecibido();
        if (aturdimiento && tiempoAturdido > 0)
        {
            aturdimiento.Tiempo = tiempoAturdido;
            CambiarEstado(aturdimiento);
        }
    }
}

public class CharacterState : Estado
{
    protected Movement movement;
    protected Vida vida;
    protected Animator animator;
    protected Damage damage;

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        Character character = personaje as Character;
        if (character != null)
        {
            movement = character.MovementComponent;
            vida = character.VidaComponent;
            animator = character.AnimatorComponent;
            damage = character.DamageComponent;
        }
    }

    public override void Salir()
    {
        base.Salir();
        movement = null;
        vida = null;
        animator = null;
        damage = null;
    }
}
