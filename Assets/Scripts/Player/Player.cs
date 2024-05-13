using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : StateMachine, IBuffable
{
    float ogMaxSpeed;
    float ogAcceleration;
    float ogDecceleration;
    float ogJumpForce;

    PlayerController controller;
    Vida vida;
    Movement movement;

    public UnityEvent OnDead;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        getOGMovementParameters();
        controller = GetComponent<PlayerController>();
        vida = GetComponent<Vida>();
        if (!vida)
        {
            vida = GetComponentInChildren<Vida>();
        }
        vida.Dead += Dead;
    }

    public void Accept(IBuff buff)
    {
        if (buff == null) return;
        buff.Buff(vida);
        buff.Buff(this);
    }

    private void Dead()
    {
        this.enabled = false;
        OnDead.Invoke();
    }

    public void SpeedPowerUp(float multiplier, float time)
    {
        StopCoroutine(nameof(SpeedPowerUpEnabler));
        multiplier = Mathf.Max(multiplier, 1f);
        modifySpeed(multiplier);
        StartCoroutine(SpeedPowerUpEnabler(time));
    }

    private void getOGMovementParameters()
    {
        ogMaxSpeed = movement.MaxSpeed;
        ogAcceleration = movement.Acceleration;
        ogDecceleration = movement.Decceleration;
        ogJumpForce = movement.JumpForce;
    }

    private void resetMovementParameters()
    {
        movement.MaxSpeed = ogMaxSpeed;
        movement.Acceleration = ogAcceleration;
        movement.Decceleration = ogDecceleration;
    }

    private void modifySpeed(float multiplier)
    {
        movement.MaxSpeed = movement.MaxSpeed * multiplier;
        movement.Acceleration = movement.Acceleration * multiplier;
        movement.Decceleration = movement.Decceleration * multiplier;
    }

    IEnumerator SpeedPowerUpEnabler(float time)
    {
        yield return new WaitForSeconds(time);
        resetMovementParameters();
    }

}
