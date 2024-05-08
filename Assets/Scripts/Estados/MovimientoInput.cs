using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoInput : Estado
{
    [SerializeField] float velocidad;

    Vector2 input_vector = Vector2.zero;

    public override void Actualizar()
    {
        ObtenerInput();
        Mover();
    }

    private void ObtenerInput()
    {
        input_vector = new Vector2(Input.GetAxis("Horizontal"), 0);
        input_vector = Vector2.ClampMagnitude(input_vector, 1);
    }

    private void Mover()
    {
        transform.Translate(input_vector * velocidad * Time.deltaTime);
    }
}
