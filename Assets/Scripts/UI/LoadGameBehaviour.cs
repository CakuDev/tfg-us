using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameBehaviour : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    private GameController gameController;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(PersistentDataIndex.SAVE_GAME)) Destroy(gameObject);

        gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
    }

    public void LoadGame()
    {
        gameController.previousScene = PlayerPrefs.GetInt(PersistentDataIndex.PREVIOUS_SCENE);
        sceneController.ChangeScene(PlayerPrefs.GetInt(PersistentDataIndex.SCENE_TO_LOAD));
    }
}
