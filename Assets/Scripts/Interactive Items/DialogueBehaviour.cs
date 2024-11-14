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
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitUntil(() => objectThatInteract.playerController.IsOnFloor());
        objectThatInteract.playerController.LockPlayer();
        yield return dialogueController.StartDialogue(fileName);
        objectThatInteract.playerController.UnlockPlayer();
    }
}
