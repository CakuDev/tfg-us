using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPointBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> onSpawnEvents;

    public void OnSpawn(GameObject spawnObject)
    {
        onSpawnEvents.Invoke(spawnObject);
    }
}
