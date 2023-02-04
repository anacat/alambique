using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gota : MonoBehaviour
{
    public PickUp pickUp;

    public void DropInBarrel()
    {
        if (pickUp.pickUpController.canDropGota)
        {
            pickUp.gameObject.transform.SetParent(null);

            pickUp.pickUpController.NullPickUp();

            pickUp.pickUpController.barrel.AddGotas(pickUp.pickUpController.currentGotas);

            pickUp.DestroyThiShit();
        }
    }
}
