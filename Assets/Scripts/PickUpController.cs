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

    private PlayerController _player;

   /// [HideInInspector]
    public bool canDropGota = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();

        _player.uiController.SetSapCounter(0, maxGotas);
    }

    private void OnInteract(InputValue interactValue)
    {
        if(_player.CanMove && interactValue.Get<float>() == 1 && action)
        {
            Invoke("ActionCooldown", actionCooldown);
            action = false;

            if (pickUpInHand == null)
            {
                TryPickUp();
                Debug.Log("fire1", gameObject);
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

                    currentGotas += pickUpInHand.value;

                    if (currentGotas > maxGotas)
                    {
                        currentGotas = maxGotas;
                    }

                    _player.uiController.SetSapCounter(currentGotas, maxGotas);

                    //currentGotas++;

                }
                else if (pickUpInHand == null && (tempPickup.type != PickUp.Type.Gota))
                {
                    pickUpInHand = tempPickup;
                    pickUpInHand.pickUpController = this;

                    pickUpInHand.Grab();

                    pickUpInHand.gameObject.transform.SetParent(hands);
                    pickUpInHand.gameObject.transform.localPosition = Vector3.zero;
                    pickUpInHand.gameObject.transform.rotation = Quaternion.identity;

                    switch(tempPickup.type)
                    {
                        case PickUp.Type.Confuse:
                            _player.uiController.SetItem(UIController.ItemType.confushroom);
                            break;
                        case PickUp.Type.Slow:
                            _player.uiController.SetItem(UIController.ItemType.roottenBeer);
                            break;
                        case PickUp.Type.Speed:
                            _player.uiController.SetItem(UIController.ItemType.doubleTap);
                            break;
                        case PickUp.Type.Strength:
                            _player.uiController.SetItem(UIController.ItemType.strongAle);
                            break;
                    }

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
        if (other.CompareTag("Barrel") && other.transform == ownedBarrel)
        {
            canDropGota = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Barrel") && other.transform == ownedBarrel)
        {
            canDropGota = false;
        }
    }
}

