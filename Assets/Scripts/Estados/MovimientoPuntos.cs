using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuntos : Estado
{
    [SerializeField] float velocidad;
    [SerializeField] float distanciaParaCambiarPunto;
    [SerializeField] Transform[] listaPuntos;
    [Tooltip("Estado al que se cambiará cuando se pase " +
        "por todos los puntos. Si es nulo, se sigue el " +
        "movimiento.")]
    [SerializeField] Estado siguienteEstado;

    List<Vector3> puntos = new List<Vector3>();
    int indicePuntoActual;

    private void Awake()
    {
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

    public override void Actualizar()
    {
        Vector3 posicion = transform.position;
        Vector3 objetivo = puntos[indicePuntoActual];
        siguientePunto();
        Vector3 direccion = objetivo - posicion;
        transform.Translate(direccion.normalized * velocidad * Time.deltaTime);
    }

    private void siguientePunto()
    {
        Vector3 posicion = transform.position;
        Vector3 objetivo = puntos[indicePuntoActual];
        float distanciaAlPunto = Vector3.Distance(posicion, objetivo);
        if (distanciaParaCambiarPunto < 0 || distanciaAlPunto > distanciaParaCambiarPunto)
        {
            return;
        }
        int nuevoIndice = indicePuntoActual + 1;
        if(nuevoIndice >= puntos.Count)
        {
            nuevoIndice = 0;
            if (siguienteEstado)
            {
                personaje.CambiarEstado(siguienteEstado);
            }
        }
        indicePuntoActual = nuevoIndice;
        objetivo = puntos[indicePuntoActual];
    }

}
