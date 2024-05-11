using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    public void Damage(int damagePoints);
}

public class Vida : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Healed;
    public UnityEvent Dead;

    int health;


    private void Awake()
    {
        health = maxHealth;
    }

    public void Heal(int healPoints)
    {
        health = Mathf.Clamp(health + healPoints, 0, maxHealth);
        Healed.Invoke(health);
    }

    public void Damage(int damagePoints)
    {
        health = Mathf.Clamp(health - damagePoints, 0, maxHealth);
        Damaged.Invoke(health);
        if(health <= 0)
        {
            Dead.Invoke();
        }
    }
}
