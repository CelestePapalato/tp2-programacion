using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, IBuff
{
    [SerializeField] int cura;
    [SerializeField] float multiplicadorVelocidad;
    [SerializeField] float tiempoPowerUp;

    public void Buff(object o)
    {
        Vida vida = (Vida)o;
        if (vida)
        {
            vida.Heal(cura);
        }
        PlayerController playerController = (PlayerController)o;
        if (playerController)
        {
            playerController.SpeedPowerUp(multiplicadorVelocidad, tiempoPowerUp);
        }
    }
}
