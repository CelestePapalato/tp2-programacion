using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterState
{
    [Header("Enemies")]
    [SerializeField] float knockbackImpulse;

    Vector2 input_vector = Vector2.zero;

    bool jumpFlag = false;

    public override void Entrar(StateMachine personajeActual)
    {
        base.Entrar(personajeActual);
        input_vector = Vector2.zero;
        damage.DamageDealed += AddKnockback;
        damage.gameObject.SetActive(false);
    }

    public override void Salir()
    {
        damage.DamageDealed -= AddKnockback;
        damage.gameObject.SetActive(false);
        base.Salir();
    }

    private void OnEnable()
    {
        if(damage)
        {
            damage.DamageDealed += AddKnockback;
        }
    }

    private void OnDisable()
    {
        if (damage)
        {
            damage.DamageDealed -= AddKnockback;
        }
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
        ControlarHitbox();
    }

    private void Saltar()
    {
        if(movement.OnFloor && jumpFlag)
        {
            movement.Jump();
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
        movement.Direction = input_vector;
    }

    private void ControlarHitbox()
    {
        if (movement.OnFloor)
        {
            damage.gameObject.SetActive(false);
        }
        else if (movement.RigidbodyComponent.velocity.y < 0) // añadir que se active cuando la caída ya haya tomado cierta velocidad
        {
            damage.gameObject.SetActive(true);
        }
    }

    // La forma en la que se mueve el jugador en el eje x hace que necesite más impulso para ser influenciado. 
    // Esto en el eje Y no sucede, resultando en una reacción impar entre los ejes.
    // Que el impulso sea tratado por Movement y no añadido de forma directa al rigidbody.
    // Que acumule las fuerzas de impulso en un Vector diferente a Velocity
    // sumarlas al rigidbody después de limitar el movimiento en X

    private void AddKnockback()
    {
        Vector2 velocity = movement.RigidbodyComponent.velocity;
        velocity.y = 0;
        movement.RigidbodyComponent.velocity = velocity;
        movement.RigidbodyComponent.AddForce(Vector2.up * knockbackImpulse, ForceMode2D.Impulse);
    }
}
