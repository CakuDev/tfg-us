using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectController : MonoBehaviour
{
    [SerializeField] private Vector2 objectSpeedBoundaries;
    [SerializeField] private Vector2 spawnFrequencyBoundaries;
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private GameObject objectToSpawn;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        Active();
    }

    public void Active()
    {
        spawnCoroutine = StartCoroutine(SpawnObjectCoroutine());
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
            SpawnObject(objectSpeed);

            float timeForNextSpawn = Random.Range(spawnFrequencyBoundaries.x, spawnFrequencyBoundaries.y);
            yield return new WaitForSeconds(timeForNextSpawn);
        }
    }

    private void SpawnObject(float objectSpeed)
    {
        GameObject spawnedObject = Instantiate(objectToSpawn);
        spawnedObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        spawnedObject.GetComponent<MovementBehaviour>().maxSpeed = objectSpeed;
    }
}
