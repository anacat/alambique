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
            pickUp.pickUpController.barrel.AddGotas(pickUp.pickUpController.currentGotas);

            pickUp.DestroyThiShit();

            pickUp.pickUpController.NullPickUp();
        }
    }


}
