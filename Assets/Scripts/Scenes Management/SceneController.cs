using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct SpawnPoint
{
    public string previousScene;
    public Transform position;
}

public class SceneController : MonoBehaviour
{
    public static bool isLoading = true;

    [SerializeField] private bool saveSceneDataOnLoad = true;
    [SerializeField] private float changeSceneSpeed = 2.5f;
    public PlayerController player;
    public SpawnPoint[] spawnPoints;
    [SerializeField] Image changeSceneImage;
    [SerializeField] AudioSource changeSceneSfx;

    private GameController gameController;
    private Dictionary<string, Transform> spawnPointsDict = new();

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
        if ( player != null && gameController.previousScene != "" && spawnPointsDict.ContainsKey(gameController.previousScene))
        {
            player.transform.position = spawnPointsDict[gameController.previousScene].position;
            player.transform.rotation = spawnPointsDict[gameController.previousScene].rotation;
        }

        if(saveSceneDataOnLoad)
        {
            PlayerPrefs.SetString(PersistentDataIndex.PREVIOUS_SCENE, gameController.previousScene);
            PlayerPrefs.SetString(PersistentDataIndex.SCENE_TO_LOAD, SceneManager.GetActiveScene().name);
        }

        StartCoroutine(OnLoadScene());
    }

    public void ChangeScene(string nextScene)
    {
        gameController.previousScene = SceneManager.GetActiveScene().name;
        changeSceneSfx.Play();
        StartCoroutine(OnChangeScene(nextScene));
    }

    IEnumerator OnChangeScene(string nextScene)
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
        isLoading = true;
        if (player != null) player.LockPlayer();

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
        isLoading = false;
    }
}
