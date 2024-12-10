using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using TMPro;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private float defaultTextSpeed;
    [SerializeField] private float skipTextSpeed;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private DialogueParser dialogueParser;

    private GameController gameController;
    private const string DIALOGUE_FOLDER = "/Dialogues/";

    private void Start()
    {
        gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
    }

    public Coroutine StartDialogue(string fileName)
    {
        string filePath = Application.dataPath + DIALOGUE_FOLDER + fileName + "/" + gameController.playerGender.ToString().ToLower() + ".txt";
        List<CinematicItem> dialogueLines = ReadDialogueFile(filePath);
        dialogueCanvas.SetActive(true);
        return StartCoroutine(ShowDialogue(dialogueLines));
    }

    IEnumerator ShowDialogue(List<CinematicItem> dialogueLines)
    {
        foreach (CinematicItem cinematicItem in dialogueLines)
        {
            yield return StartCoroutine(cinematicItem.Action());
        }
        dialogueCanvas.SetActive(false);
    }

    private List<CinematicItem> ReadDialogueFile(string filePath)
    {
        List<string> dialogueLines = new();

        StreamReader reader = new (filePath);
        
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            dialogueLines.Add(line);
        }

        return dialogueParser.ParseFileLines(dialogueLines, dialogueCanvas, nameText, dialogueText, defaultTextSpeed, skipTextSpeed, gameController);
    }
}
