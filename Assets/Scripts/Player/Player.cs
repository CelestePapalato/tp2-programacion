using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : StateMachine, IBuffable
{
    PlayerController controller;
    Vida vida;

    public UnityEvent OnDead;

    private void Awake()
    {
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
        buff.Buff(controller);
    }

    private void Dead()
    {
        this.enabled = false;
        OnDead.Invoke();
    }

}
