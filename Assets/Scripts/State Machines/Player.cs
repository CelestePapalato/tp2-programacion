using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Esperar))]
[RequireComponent(typeof(PlayerController))]
public class Player : Character, IBuffable // Mover los buffs de movimiento a Movement en vez de que estén en Player?
{
    PlayerController controller;

    public static new UnityAction OnDead;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
        primerEstado = controller;
    }
    protected override void Dead()
    {
        base.Dead();
        OnDead?.Invoke();
        this.enabled = false;
    }

    public void Accept(IBuff buff)
    {
        if (buff == null) return;
        buff.Buff(vida);
        buff.Buff(this);
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
        Debug.Log(multiplier + " " + time);
        StartCoroutine(JumpPowerUpEnabler(multiplier, time));
    }

    IEnumerator JumpPowerUpEnabler(float multiplier, float time)
    {
        movement.JumpMultiplier = multiplier;
        yield return new WaitForSeconds(time);
        movement.JumpMultiplier = 1;
    }
}
