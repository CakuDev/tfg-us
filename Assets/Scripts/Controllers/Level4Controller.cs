using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : MonoBehaviour
{
    [SerializeField] private GameObject ironFenceEntrance;
    [SerializeField] private GameObject ironFenceExit;
    [SerializeField] private SpawnObjectController spawnObjectController;
    [SerializeField] private float spawnDuration;

    bool isActive = false;

    public void StartLevel()
    {
        if (isActive) return;

        isActive = true;
        ironFenceEntrance.SetActive(true);
        spawnObjectController.Active();
        StartCoroutine(WaitForEndLevel());
    }

    IEnumerator WaitForEndLevel()
    {
        yield return new WaitForSeconds(spawnDuration);
        spawnObjectController.Deactive();
        ironFenceExit.SetActive(false);
    }
}
