using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTransform;

    public float movementSpeed;
    public float movementTime;

    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            HandleMovementInput();
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            newPosition += (transform.right * movementSpeed * Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            newPosition += (transform.forward * movementSpeed * Input.GetAxis("Vertical"));
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, movementTime * Time.deltaTime);
    }
}
