using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemigo : Character
{
    public static new UnityAction<int> OnDead;

    [Header("Game Manager")]
    [SerializeField] int puntos;

    protected override void Dead()
    {
        base.Dead();
        OnDead?.Invoke(puntos);
        Destroy(gameObject);
    }
}
