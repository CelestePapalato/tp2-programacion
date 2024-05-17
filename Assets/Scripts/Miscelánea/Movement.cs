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

public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask floorLayer;
    [SerializeField][Range(0, 1f)] float raycastDistance;


    [Header("Debug")]
    [SerializeField] float currentMaxSpeed;
    [SerializeField] float currentAcceleration;
    [SerializeField] float currentDecceleration;
    [SerializeField] float currentJumpForce;

    public float MaxSpeed { get => maxSpeed; set { currentMaxSpeed = Mathf.Max(maxSpeed, value); } }
    public float Acceleration { get => acceleration; set { currentAcceleration = Mathf.Max(maxSpeed, value); } }
    public float JumpForce { get => jumpForce; set { currentJumpForce = Mathf.Max(maxSpeed, value); } }


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
        currentMaxSpeed = maxSpeed;
        currentAcceleration = acceleration;
        currentDecceleration = decceleration;
        currentJumpForce = jumpForce;
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
        float accel = currentAcceleration * Time.fixedDeltaTime;
        if (currentDirection.x > 0)
        {
            new_velocity.x = Mathf.Min(new_velocity.x + accel, currentMaxSpeed * currentDirection.x);
        }
        if(currentDirection.x < 0)
        {
            new_velocity.x = Mathf.Max(new_velocity.x - accel, currentMaxSpeed * currentDirection.x);
        }
        if(currentDirection.x == 0)
        {
            new_velocity.x = Mathf.Lerp(new_velocity.x, 0, decceleration * Time.deltaTime);
        }
        rb.velocity = new_velocity;
    }
    public void Jump()
    {
        Vector2 velocity = rb.velocity;
        velocity.y = 0;
        rb.velocity = velocity;
        rb.AddForce(currentJumpForce * Vector3.up, ForceMode2D.Impulse);
    }
}
