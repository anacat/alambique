using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnPoint[] _spawnPoints;
    [SerializeField]
    private float _spawnDelay = 1;
    private Coroutine _spawningCoroutine;

    [ContextMenu("Start spawner")]
    public void StartSpawner()
    {
        _spawningCoroutine = StartCoroutine(DoSpawning());
    }

    [ContextMenu("Stop spawner")]
    public void StopSpawner()
    {
        StopCoroutine(_spawningCoroutine);
    }

    private IEnumerator DoSpawning()
    {
        while(true)
        {
            var possibleSpawns = new List<SpawnPoint>();
            for(var i = 0; i < _spawnPoints.Length; i++)
            {
                if(!_spawnPoints[i].inUse)
                {
                    possibleSpawns.Add(_spawnPoints[i]);
                }
            }

            if(possibleSpawns.Count > 0)
            {
                var pointToSpawnFrom = Random.Range(0, possibleSpawns.Count);
                var pickupToSpawn = Random.Range(0, possibleSpawns[pointToSpawnFrom].SpawnablePickups.Length);

                var currentSpawnPoint = possibleSpawns[pointToSpawnFrom];
                var currentPickup = currentSpawnPoint.SpawnablePickups[pickupToSpawn];

                Instantiate(currentPickup, currentSpawnPoint.transform.position, currentPickup.transform.rotation);

                currentSpawnPoint.inUse = true;

                yield return new WaitForSeconds(_spawnDelay);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

}
