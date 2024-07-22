using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] protected Estado primerEstado;

    protected Estado estadoActual;
    protected Estado primerEstadoBuffer;
    protected Estado ultimoEstado;

    protected virtual void Start()
    {
        if (!primerEstado)
        {
            primerEstado = GetComponent<Estado>();
        }

        estadoActual = primerEstado;
        primerEstadoBuffer = primerEstado;

        if (estadoActual)
        {
            estadoActual.Entrar(this);
        }
        else
        {
            Debug.LogWarning("El State Machine " + name + "no posee ni encuentra un Estado al que llamar");
        }
    }

    protected virtual void Update()
    {
        if (estadoActual)
        {
            estadoActual.Actualizar();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (estadoActual)
        {
            estadoActual.ActualizarFixed();
        }
    }

    public virtual void CambiarEstado(Estado nuevoEstado)
    {
        estadoActual?.Salir();
        estadoActual = (nuevoEstado) ? nuevoEstado : primerEstado;
        estadoActual?.Entrar(this);
    }

    private void OnEnable()
    {
        if (estadoActual)
        {
            return;
        }
        primerEstado = primerEstadoBuffer;
        CambiarEstado(ultimoEstado);
    }

    private void OnDisable()
    {
        ultimoEstado = estadoActual;
        primerEstadoBuffer = primerEstado;
        primerEstado = null;
        estadoActual?.Salir();
    }

}

public abstract class Estado : MonoBehaviour
{
    protected bool isActive = false;
    protected StateMachine personaje;

    public virtual void Entrar(StateMachine personajeActual)
    {
        personaje = personajeActual;
        isActive = true;
    }
    public virtual void Salir()
    {
        isActive = false;
    }

    public virtual void Actualizar() { }
    public virtual void ActualizarFixed() { }
    public virtual void DañoRecibido() { }

    private void OnDisable()
    {
        if (!personaje) { return; }
        personaje.CambiarEstado(null);
    }

}