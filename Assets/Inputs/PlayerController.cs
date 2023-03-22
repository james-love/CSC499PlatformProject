using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


//TODO Jump Cancel
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float playerHeight;

    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float runAcceleration;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;
    [SerializeField] private int airJumps = 1;
    [SerializeField] private float airControl;
    [SerializeField] private float gravity;

    private float direction = 0f;
    public Vector2 targetVelocity = Vector2.zero;
    private bool isRunning = false;
    public bool isGrounded = true;
    private bool jumping = false;
    private int currentJumps = 0;
    private bool groundedJump;
    private bool jumpCancel;

    private new Rigidbody2D rigidbody;

    private bool IsMoving => direction != 0f;
    private bool MovingRight => rigidbody.velocity.x > 1;
    private float SpeedLimit => isRunning ? maxRunSpeed : maxSpeed;
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
            jumping = true;
        else if (context.canceled)
            jumpCancel = true;
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
        CheckIsGrounded();

        HorizontalMovement();

        JumpCheck();
    }

    private void FixedUpdate()
    {
        if (!IsMoving && Mathf.Approximately(rigidbody.velocity.x, 0f))
            rigidbody.velocity = new Vector2(0f, targetVelocity.y);
        else
            rigidbody.velocity = new Vector2(targetVelocity.x, targetVelocity.y);
    }

    private void CheckIsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, (playerHeight / 2f) + 0.1f, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;

        if (!isGrounded && groundedJump)
            groundedJump = false;
    }

    private void HorizontalMovement()
    {
        if (IsMoving)
            targetVelocity.x = Mathf.Clamp(rigidbody.velocity.x + (Acceleration * direction), -SpeedLimit, SpeedLimit);
        else if (rigidbody.velocity.x != 0f)
            targetVelocity.x = Mathf.Clamp(rigidbody.velocity.x - (deceleration * (MovingRight ? 1 : -1)), MovingRight ? 0f : -maxSpeed, MovingRight ? maxSpeed : 0f);
    }

    private void JumpCheck()
    {
        if (!jumping && !isGrounded)
            targetVelocity.y -= gravity;
        else if (!jumping && isGrounded && !groundedJump)
            targetVelocity.y = 0f;

        if (jumpCancel)
        {
            targetVelocity.y = 0f;
            jumpCancel = false;
        }

        if (jumping && currentJumps < airJumps)
        {
            if (currentJumps == 0 && isGrounded)
                groundedJump = true;
            targetVelocity.y = jumpHeight;
            jumping = false;
            currentJumps++;
        }
        else if (jumping)
            jumping = false;
        else if (isGrounded)
            currentJumps = 0;
    }
}
