using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelManager : MonoBehaviour
{
    public int capGotas;
    public int currentGotas;

    public GameObject barrelBar;

    public Vector3 zeroScale;
    public Vector3 maxScale;

    // Start is called before the first frame update
    public void AddGotas(int toAdd)
    {
        currentGotas += toAdd;

        Debug.Log(currentGotas);
    }
}
