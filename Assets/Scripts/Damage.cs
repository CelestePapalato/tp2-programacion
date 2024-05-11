using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageDealer
{
    public int DamagePoints { get; }
    public float Impulse { get; }
    public Vector2 Position { get; }

}

public class Damage : MonoBehaviour, IDamageDealer
{
    [SerializeField] int damagePoints;
    [SerializeField] float impulse;

    public int DamagePoints { get { return damagePoints; } }
    public float Impulse { get {  return impulse; } }
    public Vector2 Position { get { return transform.position; } }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable enemyDamageable;
        IHittable enemyHittable;
        if (other.TryGetComponent<IDamageable>(out enemyDamageable))
        {
            enemyDamageable.Damage(this);
        }
        if (other.TryGetComponent<IHittable>(out enemyHittable))
        {
            enemyHittable.Hit(this);
        }
    }

}
