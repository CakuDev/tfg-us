using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CinematicDialogue : CinematicItem
{
    private DialogueParser dialogueParser;
    private float defaultSpeed;
    private float skipSpeed;
    private string nameToShow = "";
    private string text = "";
    private GameObject dialogueCanvas;
    private TMP_Text nameText;
    private TMP_Text dialogueText;

    private float currentSpeed;

    public CinematicDialogue(DialogueParser dialogueParser, float defaultSpeed, float skipSpeed, string name, string text, GameObject dialogueCanvas, TMP_Text nameText, TMP_Text dialogueText)
    {
        this.dialogueParser = dialogueParser;
        this.defaultSpeed = defaultSpeed;
        this.skipSpeed = skipSpeed;
        nameToShow = name;
        this.text = text;
        this.dialogueCanvas = dialogueCanvas;
        this.nameText = nameText;
        this.dialogueText = dialogueText;
    }

    public override IEnumerator Action()
    {
        dialogueCanvas.SetActive(true);
        nameText.text = nameToShow;
        currentSpeed = defaultSpeed;
        Coroutine ckeckSkipCoroutine = dialogueParser.StartCoroutine(CheckSkipLine());
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(currentSpeed);
        }

        dialogueParser.StopCoroutine(ckeckSkipCoroutine);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    IEnumerator CheckSkipLine()
    {
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        currentSpeed = skipSpeed;
    }
}
