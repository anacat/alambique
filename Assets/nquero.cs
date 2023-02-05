using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nquero : MonoBehaviour
{
    public PickUp pickUp;

    public void Kill()
    {
        pickUp.DestroyThiShit(true);
    }
}
