using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuntos : Estado
{
    [Tooltip("Indica si el objeto se moverá por Rigidbody o Transform.Translate")]
    [SerializeField] bool useRigidbody;
    [SerializeField] bool useYAxis;
    [SerializeField] float velocidad;
    [SerializeField] float distanciaParaCambiarPunto;
    [SerializeField] Transform[] listaPuntos;
    [Tooltip("Estado al que se cambiará cuando se pase " +
        "por todos los puntos. Si es nulo, se sigue el " +
        "movimiento.")]
    [SerializeField] Estado siguienteEstado;

    List<Vector2> puntos = new List<Vector2>();
    int indicePuntoActual;

    Rigidbody2D rb;

    private void Awake()
    {
        if (useRigidbody)
        {
            useRigidbody = TryGetComponent<Rigidbody2D>(out rb);
        }

        foreach (Transform t in listaPuntos)
        {
            puntos.Add(t.position);
        }
    }

    public override void Entrar(Personaje personajeActual)
    {
        base.Entrar(personajeActual);
        indicePuntoActual = 0;
    }

    public override void ActualizarFixed()
    {
        if (useRigidbody)
        {
            return;
        }
        Vector2 direccion = siguientePunto();
        if (!useYAxis)
        {
            direccion.y = 0;
        }
        rb.AddForce(direccion * velocidad * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    public override void Actualizar()
    {
        if (!useRigidbody)
        {
            return;
        }
        Vector2 direccion = siguientePunto();
        if (!useYAxis)
        {
            direccion.y = 0;
        }
        transform.Translate(direccion * velocidad * Time.deltaTime);

    }

    private Vector2 siguientePunto()
    {
        Vector2 posicion = transform.position;
        Vector2 objetivo = puntos[indicePuntoActual];
        float distanciaAlPunto = Vector2.Distance(posicion, objetivo);
        Vector2 direccion = objetivo - posicion;
        if (distanciaParaCambiarPunto < 0 || distanciaAlPunto > distanciaParaCambiarPunto)
        {
            return direccion.normalized;
        }
        int nuevoIndice = indicePuntoActual + 1;
        Debug.Log(nuevoIndice);
        if (nuevoIndice >= puntos.Count)
        {
            nuevoIndice = 0;
            if (siguienteEstado)
            {
                personaje.CambiarEstado(siguienteEstado);
                return Vector2.zero;
            }
        }
        Debug.Log(nuevoIndice);
        indicePuntoActual = nuevoIndice;
        objetivo = puntos[indicePuntoActual];
        direccion = objetivo - posicion;
        return direccion.normalized;
    }

}
