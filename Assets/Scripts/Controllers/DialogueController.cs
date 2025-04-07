using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private float defaultTextSpeed;
    [SerializeField] private float skipTextSpeed;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private DialogueParser dialogueParser;

    private GameController gameController;
    private const string DIALOGUE_FOLDER = "Dialogues/";

    private void Start()
    {
        gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
    }

    public Coroutine StartDialogue(string fileName)
    {
        string filePath = DIALOGUE_FOLDER + fileName + "/" + gameController.playerGender.ToString().ToLower();
        List<CinematicItem> dialogueLines = ReadDialogueFile(filePath);
        dialogueCanvas.SetActive(true);
        return StartCoroutine(ShowDialogue(dialogueLines));
    }

    IEnumerator ShowDialogue(List<CinematicItem> dialogueLines)
    {
        foreach (CinematicItem cinematicItem in dialogueLines)
        {
            if(cinematicItem == null) continue;

            if (cinematicItem.isAsync)
            {
                StartCoroutine(cinematicItem.Action());
            }
            else
            {
                yield return StartCoroutine(cinematicItem.Action());
            }
        }
        dialogueCanvas.SetActive(false);
    }

    private List<CinematicItem> ReadDialogueFile(string filePath)
    {
        TextAsset file = Resources.Load<TextAsset>(filePath);
        List<string> dialogueLines = new();
        string[] lines = file.text.Split("\r\n");
        foreach (var line in lines)
        {
            dialogueLines.Add(line);
        }

        return dialogueParser.ParseFileLines(dialogueLines, dialogueCanvas, nameText, dialogueText, defaultTextSpeed, skipTextSpeed, gameController);
    }
}
