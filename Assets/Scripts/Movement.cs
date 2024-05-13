using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask floorLayer;
    [SerializeField][Range(0, 1f)] float raycastDistance;


    public float CurrentMaxSpeed { get => CurrentMaxSpeed; set { CurrentMaxSpeed = Mathf.Max(maxSpeed, value); } }
    public float CurrentAcceleration { get => CurrentAcceleration; set { CurrentAcceleration = Mathf.Max(maxSpeed, value); } }
    public float CurrentDecceleration { get => CurrentDecceleration; set { CurrentDecceleration = Mathf.Max(maxSpeed, value); } }
    public float CurrentJumpForce { get => CurrentJumpForce; set { CurrentJumpForce = Mathf.Max(maxSpeed, value); } }


    Vector2 currentDirection = Vector2.zero;
    public Vector2 Direction
    {
        get => currentDirection;

        set => currentDirection = Vector2.ClampMagnitude(value, 1);

    }

    public bool OnFloor { get; private set; }

    Rigidbody2D rb;

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
        Vector2 movementVector = Vector2.zero;
        Vector2 targetSpeed = currentDirection * CurrentMaxSpeed;
        Vector2 currentSpeed = rb.velocity;
        currentSpeed.y = 0;
        float difference = targetSpeed.magnitude - currentSpeed.magnitude;
        float _acceleration;
        if (!ExtendedMaths.Approximately(difference, 0, 0.01f))
        {
            if (difference > 0)
            {
                _acceleration = Mathf.Min(CurrentAcceleration * Time.fixedDeltaTime, difference);
            }
            else
            {
                _acceleration = Mathf.Max(-CurrentDecceleration * Time.fixedDeltaTime, difference);
            }
            difference = 1f / difference;
            movementVector = targetSpeed - currentSpeed;
            movementVector *= difference * _acceleration;
        }
        rb.velocity += movementVector;
    }

    public void Jump()
    {
        rb.AddForce(CurrentJumpForce * Vector3.up, ForceMode2D.Impulse);
    }
}
