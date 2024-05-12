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
    [Header("Movement")]
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask floorLayer;
    [SerializeField][Range(0, 1f)] float raycastDistance;

    [Header("Enemies")]
    [SerializeField] float knockbackImpulse;

    Rigidbody2D rb;
    Damage damage;
    Vector2 input_vector = Vector2.zero;

    float currentMaxSpeed;
    float currentJumpForce;

    bool jumpFlag = false;

    private void Awake()
    {
        currentMaxSpeed = maxSpeed;
        currentJumpForce = jumpForce;
        rb = GetComponent<Rigidbody2D>();
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
        if(isOnFloor() && jumpFlag)
        {
            rb.AddForce(currentJumpForce * Vector3.up, ForceMode2D.Impulse);
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
        Vector2 targetSpeed = input_vector * currentMaxSpeed;
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

    private void ControlarHitbox()
    {
        if (isOnFloor())
        {
            damage.gameObject.SetActive(false);
        }
        else if (rb.velocity.y < 0)
        {
            damage.gameObject.SetActive(true);
        }
    }

    private bool isOnFloor()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, floorLayer);
    }

    private void AddKnockback()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
        rb.AddForce(Vector2.up * knockbackImpulse, ForceMode2D.Impulse);
    }

    public void SpeedPowerUp(float multiplier, float time)
    {
        StopCoroutine(nameof(SpeedPowerUpEnabler));
        multiplier = Mathf.Max(multiplier, 1f);
        currentMaxSpeed = maxSpeed * multiplier;
        StartCoroutine(SpeedPowerUpEnabler(time));
    }

    IEnumerator SpeedPowerUpEnabler(float time)
    {
        yield return new WaitForSeconds(time);
        currentMaxSpeed = maxSpeed;
    }

}
