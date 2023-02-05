using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]

public class Gota : MonoBehaviour
{
    PickUp pickUp;

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

            pickUp.DestroyThiShit(false);

            pickUp.pickUpController.NullPickUp();
        }
    }


}
