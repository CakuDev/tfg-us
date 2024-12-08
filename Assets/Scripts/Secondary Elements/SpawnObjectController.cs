using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectController : MonoBehaviour
{
    [SerializeField] private Vector3 directionToMove;
    [SerializeField] private Vector2 objectSpeedBoundaries;
    [SerializeField] private Vector2 spawnFrequencyBoundaries;
    [SerializeField] private Vector2Int amountToSpawn;
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private bool activeOnStart;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        if(activeOnStart) Active();
    }

    public void Active()
    {
        if(spawnCoroutine == null) spawnCoroutine = StartCoroutine(SpawnObjectCoroutine());
    }

    public void Deactive()
    {
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        while (true) {
            float objectSpeed = Random.Range(objectSpeedBoundaries.x, objectSpeedBoundaries.y);

            List<Transform> availableSpawnPoints = new(spawnPoints);
            int currentAmount = Random.Range(amountToSpawn.x, amountToSpawn.y);
            for (int i = 0; i < currentAmount; i++)
            {
                Transform usedSpawnPoint = SpawnObject(objectSpeed, availableSpawnPoints);
                availableSpawnPoints.Remove(usedSpawnPoint);
            }

            float timeForNextSpawn = Random.Range(spawnFrequencyBoundaries.x, spawnFrequencyBoundaries.y);
            yield return new WaitForSeconds(timeForNextSpawn);
        }
    }

    private Transform SpawnObject(float objectSpeed, List<Transform> availableSpawnPoints)
    {
        GameObject spawnedObject = Instantiate(objectToSpawn);
        int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform usedSpawnPoint = availableSpawnPoints[spawnIndex];
        spawnedObject.transform.position = usedSpawnPoint.position;
        spawnedObject.transform.position = usedSpawnPoint.position;
        spawnedObject.GetComponent<ConstantMovementBehaviour>().StartMoving(directionToMove, objectSpeed);
        return usedSpawnPoint;
    }
}
