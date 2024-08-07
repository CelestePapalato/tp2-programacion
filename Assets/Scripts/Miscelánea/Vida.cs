using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vida : MonoBehaviour, IDamageable, IHittable
{
    [SerializeField] int maxHealth;
    [SerializeField] float invincibilityTime;
    public UnityAction<int, int> OnHealthUpdate;
    public UnityAction OnNoHealth;
    public UnityAction OnDamaged;
    public UnityAction OnHealed;

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

    private void Start()
    {
        OnHealthUpdate?.Invoke(health, maxHealth);
    }

    public void Heal(int healPoints)
    {
        health = Mathf.Clamp(health + healPoints, 0, maxHealth);
        if(OnHealed != null) { OnHealed(); }
        OnHealthUpdate?.Invoke(health, maxHealth);
    }

    public void Damage(IDamageDealer damageDealer)
    {
        if(invincibility)
        {
            return;
        }
        health = Mathf.Clamp(health - damageDealer.DamagePoints, 0, maxHealth);
        OnDamaged?.Invoke();
        OnHealthUpdate?.Invoke(health, maxHealth);
        StartCoroutine(invincibilityEnabler());
        if (health <= 0 && OnNoHealth != null)
        {
            OnNoHealth();
        }
    }

    public void Hit(IDamageDealer damageDealer)
    {
        Vector2 position = transform.position;
        Vector2 impulseVector = position - damageDealer.Position;
        impulseVector.Normalize();
        rb?.AddForce(impulseVector * damageDealer.Impulse, ForceMode2D.Impulse);
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
