using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Perseguir : CharacterState, IObjectTracker
{
    [Header("Transform")]
    [SerializeField] bool UseTransform = false;
    [SerializeField] float speed = 1f;
    [Header("Direction")]
    [SerializeField] bool UseYAxis = true;
    [Header("State Change")]
    [SerializeField] Estado nextState;
    [SerializeField] float distanceForNextState;

    private Transform target;
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
        if (!target)
        {
            return;
        }
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;
        if (distance <= distanceForNextState && nextState)
        {
            personaje.CambiarEstado(nextState);
            return;
        }
        if (!UseYAxis)
        {
            direction.y = 0;
        }
        if (UseTransform || !movement)
        {
            transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
        }
        else
        {
            movement.Direction = direction;
        }
    }

    public override void DañoRecibido()
    {
        base.DañoRecibido();
    }

}
