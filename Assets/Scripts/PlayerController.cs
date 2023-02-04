using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float currentSpeed;
    public float rotationSpeed = 100.0f;



    private void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        if (movement.magnitude > 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, movement, Time.deltaTime * rotationSpeed);
        }

        transform.position += movement * currentSpeed * Time.deltaTime;


    }



}
