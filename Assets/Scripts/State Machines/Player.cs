using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : StateMachine, IBuffable
{
    [SerializeField] float tiempoAturdido;

    float ogMaxSpeed;
    float ogAcceleration;
    float ogJumpForce;

    PlayerController controller;
    Vida vida;
    Movement movement;
    Esperar aturdimiento;

    public UnityEvent OnDead;


    private void Awake()
    {
        movement = GetComponent<Movement>();
        getOGMovementParameters();
        controller = GetComponent<PlayerController>();
        aturdimiento = GetComponent<Esperar>();
        vida = GetComponent<Vida>();
        if (!vida)
        {
            vida = GetComponentInChildren<Vida>();
        }
        vida.NoHealth += Dead;
        vida.Damaged += OnDamageReceived;
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
        OnDead.Invoke();
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
        modifySpeed(multiplier);
        StartCoroutine(SpeedPowerUpEnabler(time));
    }

    private void getOGMovementParameters()
    {
        ogMaxSpeed = movement.MaxSpeed;
        ogAcceleration = movement.Acceleration;
        ogJumpForce = movement.JumpForce;
    }

    private void resetMovementParameters()
    {
        movement.MaxSpeed = ogMaxSpeed;
        movement.Acceleration = ogAcceleration;
    }

    private void modifySpeed(float multiplier)
    {
        movement.MaxSpeed = ogMaxSpeed * multiplier;
        movement.Acceleration = ogAcceleration * multiplier;
    }

    IEnumerator SpeedPowerUpEnabler(float time)
    {
        yield return new WaitForSeconds(time);
        resetMovementParameters();
    }

    public void JumpPowerUp(float multiplier, float time)
    {
        StopCoroutine(nameof(JumpPowerUp));
        multiplier = Mathf.Max(multiplier, 1f);
        if (multiplier == 1)
        {
            return;
        }
        movement.JumpForce = ogJumpForce * multiplier;
        StartCoroutine(JumpPowerUpEnabler(time));
    }

    IEnumerator JumpPowerUpEnabler(float time)
    {
        yield return new WaitForSeconds(time);
        movement.JumpForce = ogJumpForce;
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
