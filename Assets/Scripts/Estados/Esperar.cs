using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esperar : Estado
{
    [SerializeField] float tiempo;
    [Tooltip("Opcional. Si Personaje no recibe un Estado no nulo, simplemente activa su primer estado.")]
    [SerializeField] Estado siguienteEstado;

    public override void Entrar(Personaje personajeActual)
    {
        base.Entrar(personajeActual);
        StartCoroutine(EsperarYCambiarEstado());
    }

    IEnumerator EsperarYCambiarEstado()
    {
        yield return new WaitForSeconds(tiempo);
        if (personaje)
        {
            personaje.CambiarEstado(siguienteEstado);
        }
    }
}
