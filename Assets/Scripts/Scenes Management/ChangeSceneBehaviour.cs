using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneBehaviour : MonoBehaviour
{
    [SerializeField] private int nextScene;
    [SerializeField] private SceneController sceneController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) sceneController.ChangeScene(nextScene);
    }
}
