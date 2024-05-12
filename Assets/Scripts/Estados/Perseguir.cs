using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Perseguir : Estado, IObjectTracker
{
    [SerializeField] float speed;
    [SerializeField] Estado nextState;
    [SerializeField] float distanceForNextState;

    private Rigidbody2D rb;
    private Transform target;
    public Transform Target { get => target; set => target = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
    }

    public override void ActualizarFixed()
    {
        if (!target || !rb)
        {
            return;
        }
        Vector2 movement = target.position - transform.position;
        float distance = movement.magnitude;
        if(distance <= distanceForNextState)
        {
            personaje.CambiarEstado(nextState);
            return;
        }
        movement.y = 0;
        movement = Vector2.ClampMagnitude(movement, 1);
        rb.velocity += movement * speed;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
    }

    public override void DañoRecibido()
    {
        base.DañoRecibido();
    }

}
