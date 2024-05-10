using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Perseguir : Estado
{
    [SerializeField] Transform objective;
    [SerializeField] float speed;
    [SerializeField] Estado nextState;
    [SerializeField] float distanceForNextState;

    Rigidbody2D rb;

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
        if (!objective || !rb)
        {
            return;
        }
        Vector2 movement = objective.position - transform.position;
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

    public void ActualizarObjetivo(Transform objetivo)
    {
        objective = objetivo;
    }
}
