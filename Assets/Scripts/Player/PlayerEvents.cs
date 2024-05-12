using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    PlayerController controller;
    Vida vida;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        vida = GetComponent<Vida>();
        if (!vida)
        {
            vida = GetComponentInChildren<Vida>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IBuff buff;
        if(TryGetComponent<IBuff>(out buff))
        {
            buff.Buff(vida);
            buff.Buff(controller);
        }
    }
}
