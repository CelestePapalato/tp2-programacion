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
    [SerializeField] float invincibilityTime;
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Healed;
    public UnityEvent Dead;

    int health;
    bool invincibility = false;
    Collider2D col;

    private void Awake()
    {
        health = maxHealth;
        col = GetComponent<Collider2D>();
    }

    public void Heal(int healPoints)
    {
        health = Mathf.Clamp(health + healPoints, 0, maxHealth);
        Healed.Invoke(health);
    }

    public void Damage(int damagePoints)
    {
        if(invincibility)
        {
            return;
        }
        Debug.Log(name + " " + health);
        health = Mathf.Clamp(health - damagePoints, 0, maxHealth);
        Damaged.Invoke(health);
        invincibilityEnabler();
        if(health <= 0)
        {
            Dead.Invoke();
        }
    }

    IEnumerator invincibilityEnabler()
    {
        invincibility = true;
        col.enabled = false;
        yield return new WaitForSeconds(invincibilityTime);
        invincibility = false;
        col.enabled = true;
    }
}
