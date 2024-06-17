using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esperar : Estado
{
    [SerializeField] float tiempo;
    [Tooltip("Opcional. Si Personaje no recibe un Estado no nulo, simplemente activa su primer estado.")]
    [SerializeField] Estado siguienteEstado;

    Movement movement;

    public float Tiempo {  get { return tiempo; } set {  tiempo = value; } }

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        StopAllCoroutines();
        StartCoroutine(EsperarYCambiarEstado());
    }

    IEnumerator EsperarYCambiarEstado()
    {
        if (movement)
        {
            movement.Direction = Vector2.zero;
            movement.enabled = false;
        }
        yield return new WaitForSeconds(tiempo);
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
