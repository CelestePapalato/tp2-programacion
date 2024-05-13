using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour, IBuffable
{
    PlayerController controller;
    Vida vida;

    public UnityAction OnDead;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        vida = GetComponent<Vida>();
        if (!vida)
        {
            vida = GetComponentInChildren<Vida>();
        }
        vida.Dead += OnDead;
    }

    public void Accept(IBuff buff)
    {
        if (buff == null) return;
        buff.Buff(vida);
        buff.Buff(controller);
    }

}
