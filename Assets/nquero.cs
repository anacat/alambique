using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nquero : MonoBehaviour
{
    public PickUp pickUp;

    public void Kill()
    {
        if(!pickUp.IsPicked)
            pickUp.DestroyThiShit(true);
    }
}
