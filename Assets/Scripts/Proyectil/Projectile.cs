using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;

    Vector2 _direction = Vector2.zero;
    Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value.normalized; }
    }

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity += _direction * speed;
    }
}
