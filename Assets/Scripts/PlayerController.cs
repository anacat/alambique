using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public Transform hands;
    private Transform pickedUpObject;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        if (movement.magnitude > 0.1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, movement, Time.deltaTime * rotationSpeed);
        }

        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            TryPickUp();
            Debug.Log("fire1");
        }
        else if (Input.GetButtonDown("Fire2") && pickedUpObject != null)
        {
            Drop();
            Debug.Log("fire2");
        }
    }

    void TryPickUp()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("PickUp"))
            {
                pickedUpObject = collider.transform;
                pickedUpObject.SetParent(hands);
                pickedUpObject.localPosition = Vector3.zero;
                pickedUpObject.rotation = Quaternion.identity;
                break;
            }
        }
    }

    void Drop()
    {
        pickedUpObject.SetParent(null);
        pickedUpObject = null;
    }

}
