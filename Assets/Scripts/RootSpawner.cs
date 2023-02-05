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
    [SerializeField]
    private GameObject _spawnIndicatorPrefab;
    [SerializeField]
    private LayerMask _floorLayer;

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

                var pickupInstance = Instantiate(currentPickup, currentSpawnPoint.transform.position, currentPickup.transform.rotation);
                var spawnIndicatorToSpawn = _spawnIndicatorPrefab;

                var pickupComponent = pickupInstance.GetComponent<PickUp>();
                if(pickupComponent != null)
                {
                    pickupComponent.grabbed.AddListener(() => currentSpawnPoint.inUse = false);
                    pickupComponent.destroyed.AddListener(() => currentSpawnPoint.inUse = false);
                    spawnIndicatorToSpawn = pickupComponent.spawnIndicator;
                }

                currentSpawnPoint.inUse = true;

                var ray = new Ray(currentSpawnPoint.transform.position, -Vector3.up);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 500, _floorLayer))
                {
                    var spawnIndicator = Instantiate(spawnIndicatorToSpawn, hit.point, _spawnIndicatorPrefab.transform.rotation);
                    pickupComponent.hitFloor.AddListener(() => Destroy(spawnIndicator));
                    StartCoroutine(DelayedDestroy(spawnIndicator));
                }

                yield return new WaitForSeconds(_spawnDelay);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator DelayedDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(4);
        Destroy(obj);
    }

}
