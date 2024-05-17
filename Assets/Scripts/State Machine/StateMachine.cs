using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] protected Estado primerEstado;

    protected Estado estadoActual;
    protected Estado primerEstadoBuffer;
    protected Estado ultimoEstado;

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
            Debug.LogWarning("El State Machine " + name + "no posee ni encuentra un Estado al que llamar");
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

    public virtual void CambiarEstado(Estado nuevoEstado)
    {
        estadoActual?.Salir();
        estadoActual = (nuevoEstado) ? nuevoEstado : primerEstado;
        estadoActual?.Entrar(this);
    }

    private void OnEnable()
    {
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

    private void OnDestroy()
    {
        primerEstado = null;
        estadoActual?.Salir();
    }
}

public abstract class Estado : MonoBehaviour
{
    protected StateMachine personaje;

    public virtual void Entrar(StateMachine personajeActual)
    {
        personaje = personajeActual;
    }
    public virtual void Salir() { }
    public virtual void Actualizar() { }
    public virtual void ActualizarFixed() { }
    public virtual void Da�oRecibido() { }

    private void OnDisable()
    {
        if (!personaje) { return; }
        personaje.CambiarEstado(null);
    }

    private void OnDestroy()
    {
        if (!personaje) { return; }
        personaje.CambiarEstado(null);
    }
}