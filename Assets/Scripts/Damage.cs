using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable enemy;
        if(TryGetComponent<IDamageable>(out enemy))
        {
            enemy.Damage(damage);
        }
    }
}
