using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
public class DoubleTapRoot : MonoBehaviour
{
    private PickUp _pickUp;

    private void Start()
    {
        _pickUp = GetComponent<PickUp>();
    }

    public void Consume()
    {
        var effectManager = _pickUp.pickUpController.transform.GetComponent<EffectManager>();
        if(effectManager != null)
        {
            effectManager.ApplySpeedUp();
        }
        _pickUp.pickUpController.NullPickUp();
        Destroy(gameObject);
    }
}
