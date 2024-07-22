using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuntos : CharacterState
{
    [Tooltip("Indica si el objeto se moverá por Rigidbody o Transform.Translate")]
    [SerializeField] bool useRigidbody;
    [SerializeField] bool useYAxis;
    [SerializeField]
    [Tooltip("Esta velocidad es para cuando el objeto no se mueve con Rigidbody")]
    float velocidad;
    [SerializeField] float distanciaParaCambiarPunto;
    [SerializeField] Transform[] listaPuntos;
    [Tooltip("Estado al que se cambiará cuando se pase " +
        "por todos los puntos. Si es nulo, se sigue el " +
        "movimiento.")]
    [SerializeField] Estado siguienteEstado;

    List<Vector2> puntos = new List<Vector2>();
    int indicePuntoActual;

    FlipSprite flipSprite;

    private void Awake()
    {
        foreach (Transform t in listaPuntos)
        {
            puntos.Add(t.position);
        }

        flipSprite = GetComponentInChildren<FlipSprite>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        indicePuntoActual = 0;
    }

    public override void ActualizarFixed()
    {
        Vector2 direccion = siguientePunto();
        if (!useRigidbody || !movement)
        {
            direccion = direccion.normalized * velocidad * Time.fixedDeltaTime;
            transform.Translate(direccion);
            flipSprite?.UpdateSprite(direccion);
            return;
        }
        movement.Direction = direccion.normalized;
    }

    private Vector2 siguientePunto()
    {
        Vector2 posicion = transform.position;
        Vector2 objetivo = puntos[indicePuntoActual];
        if (!useYAxis)
        {
            objetivo.y = posicion.y;
        }
        float distanciaAlPunto = Vector2.Distance(posicion, objetivo);
        Vector2 direccion = objetivo - posicion;
        if (distanciaParaCambiarPunto < 0 || distanciaAlPunto > distanciaParaCambiarPunto)
        {
            return direccion.normalized;
        }
        int nuevoIndice = indicePuntoActual + 1;
        if (nuevoIndice >= puntos.Count)
        {
            nuevoIndice = 0;
            if (siguienteEstado)
            {
                personaje.CambiarEstado(siguienteEstado);
                return Vector2.zero;
            }
        }
        indicePuntoActual = nuevoIndice;
        objetivo = puntos[indicePuntoActual];
        direccion = objetivo - posicion;
        return direccion;
    }

}
