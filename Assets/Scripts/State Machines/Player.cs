using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : StateMachine, IBuffable // Mover los buffs de movimiento a Movement en vez de que estén en Player?
{
    [SerializeField] float tiempoAturdido;

    PlayerController controller;
    Vida vida;
    Movement movement;
    Esperar aturdimiento;

    public static UnityAction OnDead;


    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<Movement>();
        controller = GetComponent<PlayerController>();
        aturdimiento = GetComponent<Esperar>();
        vida = GetComponent<Vida>();
        if (!vida)
        {
            vida = GetComponentInChildren<Vida>();
        }
    }

    private void OnEnable()
    {
        vida.NoHealth += Dead;
        vida.Damaged += OnDamageReceived;
    }

    private void OnDisable()
    {
        vida.NoHealth -= Dead;
        vida.Damaged -= OnDamageReceived;
    }

    public void Accept(IBuff buff)
    {
        if (buff == null) return;
        buff.Buff(vida);
        buff.Buff(this);
    }

    private void Dead()
    {
        movement.Direction = Vector2.zero;
        OnDead?.Invoke();
        this.enabled = false;
    }

    public void SpeedPowerUp(float multiplier, float time)
    {
        StopCoroutine(nameof(SpeedPowerUpEnabler));
        multiplier = Mathf.Max(multiplier, 1f);
        if(multiplier == 1)
        {
            return;
        }
        StartCoroutine(SpeedPowerUpEnabler(multiplier, time));
    }

    IEnumerator SpeedPowerUpEnabler(float multiplier, float time)
    {
        movement.SpeedMultiplier = multiplier;
        yield return new WaitForSeconds(time);
        movement.SpeedMultiplier = 1;
    }

    public void JumpPowerUp(float multiplier, float time)
    {
        StopCoroutine(nameof(JumpPowerUp));
        multiplier = Mathf.Max(multiplier, 1f);
        if (multiplier == 1)
        {
            return;
        }
        StartCoroutine(JumpPowerUpEnabler(multiplier, time));
    }

    IEnumerator JumpPowerUpEnabler(float multiplier, float time)
    {
        movement.JumpMultiplier = multiplier;
        yield return new WaitForSeconds(time);
        movement.JumpMultiplier = 1;
    }

    private void OnDamageReceived()
    {
        estadoActual?.DañoRecibido();
        if(aturdimiento)
        {
            aturdimiento.Tiempo = tiempoAturdido;
            CambiarEstado(aturdimiento);
        }
    }
}
