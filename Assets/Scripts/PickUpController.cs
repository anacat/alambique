using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public BarrelManager barrel;
    public Transform hands;
    PickUp pickUpInHand;

    public int maxGotas;

    public int currentGotas = 0;

    public float actionCooldown = 0.5f;
    bool action = true;

    [HideInInspector]
    public bool canDropGota = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && action )
        {

            Invoke("ActionCooldown", actionCooldown);
            action = false;

            if (pickUpInHand == null)
            {
                TryPickUp();
                Debug.Log("fire1");
            }
            else
            {
                pickUpInHand.Use();
                Debug.Log("fire2");
            }
        }

    }

    void ActionCooldown()
    {
        action = true;
    }

    void TryPickUp()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("PickUp"))
            {
                PickUp tempPickup = collider.gameObject.GetComponent<PickUp>();

                if (tempPickup.type == PickUp.Type.Gota && currentGotas < maxGotas && (pickUpInHand == null || pickUpInHand.type == PickUp.Type.Gota))
                {
                    pickUpInHand = tempPickup;
                    pickUpInHand.pickUpController = this;

                    pickUpInHand.Grab();

                    pickUpInHand.gameObject.transform.SetParent(hands);
                    pickUpInHand.gameObject.transform.localPosition = Vector3.zero;
                    pickUpInHand.gameObject.transform.rotation = Quaternion.identity;

                    //pickUpInHand.gameObject.transform.localScale = Vector3.zero;
                    currentGotas++;

                }
                else if (pickUpInHand == null && (tempPickup.type == PickUp.Type.Buff || pickUpInHand.type == PickUp.Type.Debuff))
                {
                    pickUpInHand = tempPickup;
                    pickUpInHand.pickUpController = this;

                    pickUpInHand.Grab();

                    pickUpInHand.gameObject.transform.SetParent(hands);
                    pickUpInHand.gameObject.transform.localPosition = Vector3.zero;
                    pickUpInHand.gameObject.transform.rotation = Quaternion.identity;
                }

                break;
            }
        }
    }

    //void Use()
    //{
    //    pickUpInHand.Use();

    //    pickUpInHand.gameObject.transform.SetParent(null);
    //    pickUpInHand = null;
    //}

    public void NullPickUp()
    {
        pickUpInHand.gameObject.transform.SetParent(null);

        pickUpInHand = null;

        currentGotas = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            canDropGota = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            canDropGota = false;
        }
    }
}

