using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedWalk;
    public float speedRun;

    public float rotationSpeed;
    public float rotationVelocity;

    public float moveSpeed;
    public float moveVelocity;
    public float currentSpeed;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speedWalk = 2;
        speedRun = 9;
        rotationSpeed = .25f;
        moveSpeed = .15f;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 Direction = new Vector2(horizontal, vertical).normalized;

        if (Direction != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSpeed);

            //transform.eulerAngles = Vector3.up *
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = (running ? speedRun : speedWalk) * Direction.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref moveVelocity, moveSpeed);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = (running ? 2f : 1f) * Direction.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, moveSpeed, Time.deltaTime);

    }
}
