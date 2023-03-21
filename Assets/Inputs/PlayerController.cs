using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float runAcceleration;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;
    [SerializeField] private int totalJumps = 2;
    [SerializeField] private float airControl;

    private float direction = 0f;
    private Vector2 targetVelocity = Vector2.zero;
    private bool isRunning = false;
    private bool isGrounded = true;

    private new Rigidbody2D rigidbody;

    private bool IsMoving => direction != 0f;
    private bool MovingRight => rigidbody.velocity.x > 1;
    private float SpeedLimit => isRunning ? maxSpeed : maxRunSpeed;
    private float Acceleration => isGrounded ? isRunning ? runAcceleration : acceleration : airControl;

    public void Move(CallbackContext context)
    {
        if (context.started)
            direction = context.ReadValue<float>();
        else if (context.canceled)
            direction = 0f;
    }

    public void Jump(CallbackContext context)
    {
        if (context.started)
            print("jump");
    }

    public void Run(CallbackContext context)
    {
        if (context.started)
            isRunning = true;
        else if (context.canceled)
            isRunning = false;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsMoving)
            targetVelocity.x = Mathf.Clamp(rigidbody.velocity.x + (Acceleration * direction), -SpeedLimit, SpeedLimit);
        else if (rigidbody.velocity.x != 0f)
            targetVelocity.x = Mathf.Clamp(rigidbody.velocity.x - (deceleration * (MovingRight ? 1 : -1)), MovingRight ? 0f : -maxSpeed, MovingRight ? maxSpeed : 0f);
    }

    private void FixedUpdate()
    {
        if (!IsMoving && Mathf.Approximately(rigidbody.velocity.x, 0f))
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        else
            rigidbody.velocity = new Vector2(targetVelocity.x, rigidbody.velocity.y);
    }
}
