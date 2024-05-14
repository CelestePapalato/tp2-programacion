using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Estado
{
    [Header("Enemies")]
    [SerializeField] float knockbackImpulse;

    Rigidbody2D rb;
    Damage damage;
    Movement movement;
    Vector2 input_vector = Vector2.zero;



    bool jumpFlag = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
        damage = GetComponent<Damage>();
        if (!damage)
        {
            damage = GetComponentInChildren<Damage>();
        }
        damage.DamageDealed += AddKnockback;
        damage.gameObject.SetActive(false);
    }

    public override void Entrar(StateMachine personajeActual)
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
        else if (rb.velocity.y < 0)
        {
            damage.gameObject.SetActive(true);
        }
    }
    private void AddKnockback()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
        rb.AddForce(Vector2.up * knockbackImpulse, ForceMode2D.Impulse);
    }

}
