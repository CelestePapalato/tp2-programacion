using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, IBuff
{
    [SerializeField] int cura;
    [SerializeField] float multiplicadorVelocidadMaxima;
    [SerializeField] float tiempoPowerUp;

    public void Buff(object o)
    {
        Vida vida = o as Vida;
        if (vida)
        {
            vida.Heal(cura);
        }
        PlayerController playerController = o as PlayerController;
        if (playerController)
        {
            playerController.SpeedPowerUp(multiplicadorVelocidadMaxima, tiempoPowerUp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IBuffable buffable;
        if(collision.TryGetComponent<IBuffable>(out buffable))
        {
            buffable.Accept(this);
            Destroy(gameObject);
        }
    }
}
