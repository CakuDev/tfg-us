using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] SceneController sceneController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas.activeSelf) Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    public void OnContinue()
    {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void OnExit()
    {
        Time.timeScale = 1f;
        sceneController.ChangeScene(0);
    }
}
