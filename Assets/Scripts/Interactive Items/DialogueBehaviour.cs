using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehaviour : InteractiveItem
{
    [SerializeField] private string fileName;
    [SerializeField] private DialogueController dialogueController;

    public override void OnInteract(InteractBehaviour objectThatInteract)
    {
        base.OnInteract(objectThatInteract);
        objectThatInteract.playerController.LockPlayer();
        Coroutine dialogueCoroutine = dialogueController.StartDialogue(fileName);
        StartCoroutine(WaitToUnlockPlayer(dialogueCoroutine));
    }

    IEnumerator WaitToUnlockPlayer(Coroutine dialogueCoroutine)
    {
        yield return dialogueCoroutine;
        objectThatInteract.playerController.UnlockPlayer();
    }
}
