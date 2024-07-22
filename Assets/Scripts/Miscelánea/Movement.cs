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

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour // (*) Hacer que el buff a la velocidad y salto sea un multiplicador en vez de una reasignación
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask floorLayer;
    [SerializeField][Range(0, 1f)] float raycastDistance;


    [Header("Debug")]
    [SerializeField] float speedMultiplier = 1f;
    [SerializeField] float jumpMultiplier = 1f;
    [SerializeField] float currentMaxSpeed;
    [SerializeField] float currentAcceleration;
    [SerializeField] float currentDecceleration;
    [SerializeField] float currentJumpForce;

    // (*) y que se multipliquen en estos los parámetros
    /*
    public float MaxSpeed { get => maxSpeed; set { currentMaxSpeed = Mathf.Max(maxSpeed, value); } }
    public float Acceleration { get => acceleration; set { currentAcceleration = Mathf.Max(maxSpeed, value); } }
    public float JumpForce { get => jumpForce; set { currentJumpForce = Mathf.Max(maxSpeed, value); } }
    */

    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = value; }
    public float JumpMultiplier { get => jumpMultiplier; set => jumpMultiplier = value; }

    public float MaxSpeed
    {
        get => maxSpeed * speedMultiplier;
        set
        {
            if (value > 0)
            {
                float diff = maxSpeed / value;
                maxSpeed = value;
                acceleration *= diff;
                decceleration *= diff;
            }
            if (value == 0)
            {
                maxSpeed = 0;
            }
        }
    }
    public float Acceleration
    {
        get => acceleration * speedMultiplier; private set { acceleration = (value > 0) ? value : acceleration; }
    }
    public float Decceleration
    {
        get => decceleration * speedMultiplier; private set { decceleration = (value > 0) ? value : decceleration; }
    }

    public float JumpForce { get => jumpForce * jumpMultiplier;}

    Vector2 currentDirection = Vector2.zero;
    public Vector2 Direction
    {
        get => currentDirection;

        set => currentDirection = Vector2.ClampMagnitude(value, 1);

    }

    public bool OnFloor { get; private set; }

    Rigidbody2D rb;

    Vector2 velocity = Vector2.zero;
    public Vector2 Velocity {  get => velocity; private set { velocity = value; } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isOnFloor();
        move();
    }

    public void isOnFloor()
    {
        OnFloor = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, floorLayer);
    }

    private void move()
    {
        Vector2 new_velocity = rb.velocity;
        float accel = Acceleration * Time.fixedDeltaTime;
        if (currentDirection.x > 0)
        {
            new_velocity.x = Mathf.Min(new_velocity.x + accel, MaxSpeed * currentDirection.x);
        }
        if(currentDirection.x < 0)
        {
            new_velocity.x = Mathf.Max(new_velocity.x - accel, MaxSpeed * currentDirection.x);
        }
        if(currentDirection.x == 0)
        {
            new_velocity.x = Mathf.Lerp(new_velocity.x, 0, Decceleration * Time.deltaTime);
        }
        rb.velocity = new_velocity;
    }
    public void Jump()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
        rb.AddForce(JumpForce * Vector3.up, ForceMode2D.Impulse);
    }
}
