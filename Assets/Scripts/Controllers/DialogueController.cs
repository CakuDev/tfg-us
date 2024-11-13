using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using TMPro;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float skipSpeed;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    private GameController gameController;
    private float currentSpeed;

    private const string DIALOGUE_FOLDER = "/Dialogues/";
    private const string SEPARATOR = "||";

    private void Start()
    {
        gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
    }

    public Coroutine StartDialogue(string fileName)
    {
        string filePath = Application.dataPath + DIALOGUE_FOLDER + fileName + "_" + gameController.playerGender.ToString().ToLower() + ".txt";
        List<DialogueLine> dialogueLines = ReadDialogueFile(filePath);
        dialogueCanvas.SetActive(true);
        return StartCoroutine(ShowDialogue(dialogueLines));
    }

    IEnumerator ShowDialogue(List<DialogueLine> dialogueLines)
    {
        foreach (DialogueLine line in dialogueLines)
        {
            nameText.text = line.name;
            Coroutine currentLine = StartCoroutine(ShowLine(line.text));
            yield return currentLine;
        }

        nameText.text = "";
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);
    }

    IEnumerator ShowLine(string line)
    {
        currentSpeed = defaultSpeed;
        Coroutine ckeckSkipCoroutine = StartCoroutine(CheckSkipLine());
        dialogueText.text = "";
        foreach(char c in line)
        {
            dialogueText.text += c;

            Debug.Log(currentSpeed);
            yield return new WaitForSeconds(currentSpeed);
        }

        StopCoroutine(ckeckSkipCoroutine);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    IEnumerator CheckSkipLine()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        currentSpeed = skipSpeed;
    }

    private List<DialogueLine> ReadDialogueFile(string filePath)
    {
        List<DialogueLine> dialogueLines = new();

        StreamReader reader = new (filePath);
        
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] splitLine = line.Split(SEPARATOR);
            dialogueLines.Add(new DialogueLine(splitLine[0], splitLine[1]));
        }

        return dialogueLines;
    }
}
