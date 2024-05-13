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
    private Movement movement;
    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            if (!target)
            {
                personaje.CambiarEstado(null);
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        if(target == null)
        {
            personaje.CambiarEstado(null);
        }
    }

    public override void ActualizarFixed()
    {
        if (!target || !rb)
        {
            return;
        }
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;
        if(distance <= distanceForNextState)
        {
            personaje.CambiarEstado(nextState);
            return;
        }
        direction.y = 0;
        movement.Direction = direction;
    }

    public override void DañoRecibido()
    {
        base.DañoRecibido();
    }

}
