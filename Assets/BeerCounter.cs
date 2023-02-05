using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerCounter : MonoBehaviour
{
    public GameObject[] beers;
    [Range(-1, 10)]
    public float sliderValue;

    void OnValidate()
    {
        int index = Mathf.RoundToInt(sliderValue * (beers.Length)/10);
        for (int i = 0; i < beers.Length; i++)
        {
            beers[i].SetActive(i <= index);
        }
    }
}
