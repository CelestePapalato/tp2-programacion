using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esperar : CharacterState
{
    [SerializeField] float tiempo;
    [Tooltip("Opcional. Si Personaje recibe un Estado nulo, simplemente activa su primer estado.")]
    [SerializeField] Estado siguienteEstado;

    public float Tiempo {  get { return tiempo; } set {  tiempo = value; } }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        StopAllCoroutines();
        StartCoroutine(EsperarYCambiarEstado());
    }

    public override void Salir()
    {
        base.Salir();
    }

    IEnumerator EsperarYCambiarEstado()
    {
        if (movement)
        {
            movement.Direction = Vector2.zero;
            movement.enabled = false;
        }
        yield return new WaitForSeconds(tiempo);
        if(!isActive)
        {
            yield break;
        }
        if(movement)
        {
            movement.enabled = true;
        }
        if (personaje)
        {
            personaje.CambiarEstado(siguienteEstado);
        }
    }
}
