using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] Estado primerEstado;

    Estado estadoActual;

    void Start()
    {
        if (!primerEstado)
        {
            primerEstado = GetComponent<Estado>();
        }

        estadoActual = primerEstado;

        if (estadoActual)
        {
            estadoActual.Entrar(this);
        }
        else
        {
            Debug.LogWarning("El Personaje " + name + "no posee ni encuentra un Estado al que llamar");
        }
    }

    void Update()
    {
        if (estadoActual)
        {
            estadoActual.Actualizar();
        }
    }

    void FixedUpdate()
    {
        if (estadoActual)
        {
            estadoActual.ActualizarFixed();
        }
    }

    public void CambiarEstado(Estado nuevoEstado)
    {
        if (estadoActual)
        {
            estadoActual.Salir();
        }
        if (nuevoEstado)
        {
            estadoActual = nuevoEstado;
        }
        else
        {
            estadoActual = primerEstado;
        }
        estadoActual.Entrar(this);
    }
}

public class Estado : MonoBehaviour
{
    protected Personaje personaje;

    public virtual void Entrar(Personaje personajeActual)
    {
        personaje = personajeActual;
    }
    public virtual void Salir() { }
    public virtual void Actualizar() { }
    public virtual void ActualizarFixed() { }
    public virtual void DañoRecibido() { }
}