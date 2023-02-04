using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float currentSpeed;
    public float rotationSpeed = 10.0f;

    public Transform hands;
    private Transform pickedUpObject;

    private Vector2 _movementVector;

    private void Start()
    {
        currentSpeed = speed;
    }    

    private void OnMove(InputValue movementValue)
    {
       _movementVector = movementValue.Get<Vector2>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(_movementVector.x, 0, _movementVector.y);

        if (movement.magnitude > 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, movement, Time.deltaTime * rotationSpeed);
        }

        transform.position += movement * currentSpeed * Time.deltaTime;
    }
}
