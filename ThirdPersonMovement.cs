using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    // Rotation
    public float turnTime = 0.2f;
    public float turnVelocity;

    // Movement
    public float speed = 50f;
    public float gravity = -9.81f;

    // Gravity
    public Transform groundCheck;
    public float groundDistance = 0.7f;
    public LayerMask groundMask;

    public Vector3 velocity;
    public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        HandleMovementInput(Input.GetKey(KeyCode.LeftShift));
        HandleGravity();

    }
    void HandleGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * speed * Time.deltaTime);
    }

    void HandleMovementInput(bool shift)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (shift)
            {
                controller.Move(direction * speed * 2f * Time.deltaTime);
            }
            else
            {
                controller.Move(direction * speed * Time.deltaTime);
            }
        }
    }
}
