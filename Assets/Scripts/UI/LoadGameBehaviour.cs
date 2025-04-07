using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameBehaviour : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    private GameController gameController;

    private void Start()
    {
        //if (!PlayerPrefs.HasKey(PersistentDataIndex.SAVE_GAME)) Destroy(gameObject);

        gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
    }

    public void LoadGame()
    {
        gameController.previousScene = PlayerPrefs.GetString(PersistentDataIndex.PREVIOUS_SCENE);
        sceneController.ChangeScene(PlayerPrefs.GetString(PersistentDataIndex.SCENE_TO_LOAD));
    }
}
