using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PickUp))]

public class Gota : MonoBehaviour
{
    PickUp pickUp;
    public UnityEvent pouredInBarrel;

    private void Start()
    {
        pickUp = GetComponent<PickUp>();
    }

    public void DropInBarrel()
    {
        if (pickUp.pickUpController.canDropGota)
        {

            var playerController = pickUp.pickUpController.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.moleAnimator.SetTrigger("pour");
            }
            pickUp.pickUpController.barrel.AddGotas(pickUp.pickUpController.currentGotas);
            Debug.Log("Do we get here?");
            pouredInBarrel.Invoke();
            Debug.Log("We get here!");
            pickUp.DestroyThiShit(false);

            pickUp.pickUpController.NullPickUp();
        }
    }


}
