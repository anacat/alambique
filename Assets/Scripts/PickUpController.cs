using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpController : MonoBehaviour
{
    public BarrelManager barrel;
    public Transform hands;
    public Transform ownedBarrel;
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

    private void OnInteract(InputValue interactValue)
    {
        if(interactValue.Get<float>() == 1 && action)
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
        //Debug.Log(interactValue.Get<float>());
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

                    currentGotas += pickUpInHand.value;
                    if (currentGotas > maxGotas)
                        currentGotas = maxGotas;

                    //currentGotas++;

                }
                else if (pickUpInHand == null && (tempPickup.type == PickUp.Type.Buff || tempPickup.type == PickUp.Type.Debuff))
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
        if (other.transform == ownedBarrel)
        {
            canDropGota = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == ownedBarrel)
        {
            canDropGota = false;
        }
    }
}

