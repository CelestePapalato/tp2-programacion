using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMaths
{
    public static bool Approximately(float a, float b, float tolerance)
    {
        return Mathf.Abs(a - b) < tolerance;
    }
}

public class PlayerController : Estado
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask floorLayer;
    [SerializeField][Range(0, 1f)] float raycastDistance;
    Rigidbody2D rb;
    Vector2 input_vector = Vector2.zero;

    bool jumpFlag = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Entrar(Personaje personajeActual)
    {
        base.Entrar(personajeActual);
        input_vector = Vector2.zero;
    }

    public override void Actualizar()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Vertical") > 0)
        {
            jumpFlag = true;
        }
    }

    public override void ActualizarFixed()
    {
        Saltar();
        ObtenerInputMovimiento();
        Mover();
    }

    private void Saltar()
    {
        if(isOnFloor() && jumpFlag)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode2D.Impulse);
        }
        jumpFlag = false;
    }

    private void ObtenerInputMovimiento()
    {
        input_vector = new Vector2(Input.GetAxis("Horizontal"), 0);
        input_vector = Vector2.ClampMagnitude(input_vector, 1);
    }

    private void Mover()
    {
        Vector2 movementVector = Vector2.zero;
        Vector2 targetSpeed = input_vector * maxSpeed;
        Vector2 currentSpeed = rb.velocity;
        currentSpeed.y = 0;
        float difference = targetSpeed.magnitude - currentSpeed.magnitude;
        float currentAcceleration;
        if (!ExtendedMaths.Approximately(difference, 0, 0.01f))
        {
            if (difference > 0)
            {
                currentAcceleration = Mathf.Min(acceleration * Time.fixedDeltaTime, difference);
            }
            else
            {
                currentAcceleration = Mathf.Max(-decceleration * Time.fixedDeltaTime, difference);
            }
            difference = 1f / difference;
            movementVector = targetSpeed - currentSpeed;
            movementVector *= difference * currentAcceleration;
        }
        rb.velocity += movementVector;
    }

    private bool isOnFloor()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, floorLayer);
    }
}
