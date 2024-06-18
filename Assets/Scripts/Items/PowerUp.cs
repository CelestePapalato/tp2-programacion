using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    PowerUpData powerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IBuffable buffable;
        if(collision.TryGetComponent<IBuffable>(out buffable))
        {
            buffable.Accept(powerUp);
            Destroy(gameObject);
        }
    }
}
