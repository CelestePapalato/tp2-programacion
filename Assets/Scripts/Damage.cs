using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damagePoints;
    [SerializeField] float impulse;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(">:)");
        damage(other.gameObject);
    }

    private void damage(GameObject gameObject)
    {
        IDamageable enemy;
        Rigidbody2D enemyRigidbody;
        if (!gameObject.TryGetComponent<IDamageable>(out enemy))
        {
            return;
        }
        enemy.Damage(damagePoints);
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (!enemyRigidbody)
        {
            enemyRigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
        }
        if (enemyRigidbody)
        {
            Vector2 position = transform.position;
            Vector2 impulseVector = enemyRigidbody.position - position;
            impulseVector.Normalize();
            enemyRigidbody.AddForce(impulseVector * impulse, ForceMode2D.Impulse);
        }

    }
}
