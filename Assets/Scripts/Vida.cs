using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vida : MonoBehaviour, IDamageable, IHittable
{
    [SerializeField] int maxHealth;
    [SerializeField] float invincibilityTime;
    public UnityEvent<int> Damaged;
    public UnityEvent<int> Healed;
    public UnityEvent Dead;

    int health;
    bool invincibility = false;
    Collider2D col;
    Rigidbody2D rb;

    private void Awake()
    {
        health = maxHealth;
        col = GetComponent<Collider2D>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Heal(int healPoints)
    {
        health = Mathf.Clamp(health + healPoints, 0, maxHealth);
        Debug.Log(name + " " + health);
        Healed.Invoke(health);
    }

    public void Damage(IDamageDealer damageDealer)
    {
        if(invincibility)
        {
            return;
        }
        health = Mathf.Clamp(health - damageDealer.DamagePoints, 0, maxHealth);
        Damaged.Invoke(health);
        StartCoroutine(invincibilityEnabler());
        Debug.Log(name + " " + health);
        if (health <= 0)
        {
            Dead.Invoke();
        }
    }

    public void Hit(IDamageDealer damageDealer)
    {
        Vector2 position = transform.position;
        Vector2 impulseVector = position - damageDealer.Position;
        impulseVector.Normalize();
        rb.AddForce(impulseVector * damageDealer.Impulse, ForceMode2D.Impulse);
    }

    IEnumerator invincibilityEnabler()
    {
        invincibility = true;
        col.enabled = false;
        Debug.Log(col.enabled);
        yield return new WaitForSeconds(invincibilityTime);
        invincibility = false;
        col.enabled = true;
        Debug.Log(col.enabled);
    }
}
