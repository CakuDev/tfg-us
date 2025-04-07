using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class CharacterCreationController : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private ToggleGroup genderToggleGroup;
    [SerializeField] private Transform headContainer;
    [SerializeField] private Transform bodyContainer;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private GameObject requiredText;

    private int selectedHead = 0;
    private int selectedBody = 0;
    private Gender selectedGender;
    private GameController gameController;

    private void Start()
    {
        gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
        foreach(var head in gameController.playerHeads)
        {
            Instantiate(head, headContainer).SetActive(false);
        }
        foreach (var body in gameController.playerBodies)
        {
            Instantiate(body, bodyContainer).SetActive(false);
        }

        headContainer.GetChild(selectedHead).gameObject.SetActive(true);
        bodyContainer.GetChild(selectedBody).gameObject.SetActive(true);
    }

    public void OnHeadChanged(int variation)
    {
        headContainer.GetChild(selectedHead).gameObject.SetActive(false);
        selectedHead = (selectedHead + variation) % gameController.playerHeads.Count;
        if (selectedHead == -1) selectedHead = gameController.playerHeads.Count - 1;
        headContainer.GetChild(selectedHead).gameObject.SetActive(true);
    }

    public void OnBodyChanged(int variation)
    {
        bodyContainer.GetChild(selectedBody).gameObject.SetActive(false);
        selectedBody = (selectedBody + variation) % gameController.playerBodies.Count;
        if (selectedBody == -1) selectedBody = gameController.playerBodies.Count - 1;
        bodyContainer.GetChild(selectedBody).gameObject.SetActive(true);
    }

    public void OnHeCheckSelected()
    {
        selectedGender = Gender.HE;
    }

    public void OnSheCheckSelected()
    {
        selectedGender = Gender.SHE;
    }

    public void OnTheyCheckSelected()
    {
        selectedGender = Gender.THEY;
    }

    public void CreateCharacter()
    {
        if (nameInput.text == null || nameInput.text.Length == 0 || !genderToggleGroup.AnyTogglesOn())
        {
            requiredText.SetActive(true);
            return;
        }
        gameController.playerName = nameInput.text;
        gameController.playerGender = selectedGender;
        gameController.currentHead = selectedHead;
        gameController.currentBody = selectedBody;

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(ProgressSave.INIT_GAME.ToString(), 1);

        sceneController.ChangeScene(nextScene);
    }
}
