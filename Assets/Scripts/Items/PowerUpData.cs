using UnityEngine;

[CreateAssetMenu(fileName = "Power Up Data", menuName = "Scriptable Objects/Power Up Data", order = 1)]
public class PowerUpData : ScriptableObject, IBuff
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
}