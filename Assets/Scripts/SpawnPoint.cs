using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] SpawnablePickups { get => _spawnablePickups; }

    // Change to Pickup when we have that
    [SerializeField]
    private GameObject[] _spawnablePickups;

    public bool inUse;
}
