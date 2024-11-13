using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct SpawnPoint
{
    public int previousScene;
    public Transform position;
}

public class SceneController : MonoBehaviour
{
    [SerializeField] private bool saveSceneDataOnLoad = true;
    [SerializeField] private float changeSceneSpeed = 2.5f;
    public PlayerController player;
    public SpawnPoint[] spawnPoints;
    [SerializeField] Image changeSceneImage;

    private GameController gameController;
    private Dictionary<int, Transform> spawnPointsDict = new();

    private void Awake()
    {
        // Load spawn points dictionary
        foreach (var item in spawnPoints)
        {
            spawnPointsDict[item.previousScene] = item.position;
        }
    }

    void Start()
    {
        gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();

        // If there is a previous scene, load its corresponding player position
        if ( player != null && gameController.previousScene != -1 && spawnPointsDict.ContainsKey(gameController.previousScene))
        {
            player.transform.position = spawnPointsDict[gameController.previousScene].position;
        }

        if(saveSceneDataOnLoad)
        {
            PlayerPrefs.SetInt(PersistentDataIndex.PREVIOUS_SCENE, gameController.previousScene);
            PlayerPrefs.SetInt(PersistentDataIndex.SCENE_TO_LOAD, SceneManager.GetActiveScene().buildIndex);
        }

        StartCoroutine(OnLoadScene());
    }

    public void ChangeScene(int nextScene)
    {
        gameController.previousScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(OnChangeScene(nextScene));
    }

    IEnumerator OnChangeScene(int nextScene)
    {
        if (player != null) player.LockPlayer();

        changeSceneImage.gameObject.SetActive(true);
        Color imageColor = changeSceneImage.color;
        while (imageColor.a < 1f)
        {
            imageColor.a += changeSceneSpeed * Time.deltaTime;
            changeSceneImage.color = imageColor;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator OnLoadScene()
    {
        if(player != null) player.LockPlayer();

        changeSceneImage.gameObject.SetActive(true);
        Color imageColor = changeSceneImage.color;
        while (imageColor.a > 0f)
        {
            imageColor.a -= changeSceneSpeed * Time.deltaTime;
            changeSceneImage.color = imageColor;
            yield return new WaitForEndOfFrame();
        }

        if (player != null) player.UnlockPlayer();
        changeSceneImage.gameObject.SetActive(false);
    }
}
