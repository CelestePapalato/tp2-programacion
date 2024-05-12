using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distance;

    Vector2 _direction = Vector2.zero;
    public Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value.normalized; }
    }
    float currentDistance = 0;
    Vector2 previousPosition = Vector2.zero;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        previousPosition = rb.position;
        rb.velocity += _direction * speed;
    }

    private void FixedUpdate()
    {
        currentDistance += (rb.position - previousPosition).magnitude;
        previousPosition = rb.position;
        if (currentDistance >= distance)
        {
            Destroy(gameObject);
        }
    }
}
