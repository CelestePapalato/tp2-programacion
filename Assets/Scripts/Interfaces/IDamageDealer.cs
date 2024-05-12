using UnityEngine;

public interface IDamageDealer
{
    public int DamagePoints { get; }
    public float Impulse { get; }
    public Vector2 Position { get; }

}