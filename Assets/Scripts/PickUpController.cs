using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Transform hands;
    PickUp pickUpInHand;

    public int maxGotas;
    int currentGotas = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUpInHand == null)
        {
            TryPickUp();
        }
        else if (Input.GetButtonDown("Fire2") && pickUpInHand != null)
        {
            Use();
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
                PickUp tempPickup = collider.gameObject.GetComponent<PickUp>();

                if (tempPickup.type == PickUp.Type.Gota && currentGotas < maxGotas && (pickUpInHand.type == PickUp.Type.Gota || pickUpInHand == null))
                {
                    pickUpInHand = tempPickup;
                    pickUpInHand.pickUpController = this;

                    pickUpInHand.Grab();

                    pickUpInHand.gameObject.transform.SetParent(hands);
                    pickUpInHand.gameObject.transform.localPosition = Vector3.zero;
                    pickUpInHand.gameObject.transform.rotation = Quaternion.identity;

                    pickUpInHand.gameObject.transform.localScale = Vector3.zero;
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

    void Use()
    {


        pickUpInHand.Use();
        pickUpInHand.gameObject.transform.SetParent(null);
        pickUpInHand = null;
    }

}
