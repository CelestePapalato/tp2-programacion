using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, IBuff
{
    [SerializeField] int cura;
    [SerializeField] float multiplicadorVelocidadMaxima;
    [SerializeField] float multiplicadorSalto;
    [SerializeField] float tiempoPowerUp;

    public void Buff(object o)
    {
        Vida vida = o as Vida;
        if (vida)
        {
            vida.Heal(cura);
        }
        Player player = o as Player;
        if (player)
        {
            player.SpeedPowerUp(multiplicadorVelocidadMaxima, tiempoPowerUp);
            player.JumpPowerUp(multiplicadorSalto, tiempoPowerUp);
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
