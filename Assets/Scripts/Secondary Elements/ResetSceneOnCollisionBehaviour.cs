using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneOnCollisionBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false) return;

        GameObject.Find("SceneController").GetComponent<SceneController>()
            .ChangeScene(SceneManager.GetActiveScene().name);
    }
}
