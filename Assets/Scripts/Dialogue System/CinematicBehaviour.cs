using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CinematicBehaviour : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private string fileName;
    [SerializeField] private UnityEvent onCinematicStart;
    [SerializeField] private UnityEvent onCinematicEnd;

    private InteractBehaviour objectThatInteract;

    public void StartCinematic(InteractBehaviour interactBehaviour)
    {
        objectThatInteract = interactBehaviour;
        StartCoroutine(StartCinematicCoroutine());
    }

    IEnumerator StartCinematicCoroutine()
    {
        yield return new WaitUntil(() => SceneController.isLoading == false);
        yield return new WaitUntil(() => objectThatInteract.playerController.IsOnFloor());
        onCinematicStart.Invoke();
        objectThatInteract.playerController.LockPlayer();

        yield return dialogueController.StartDialogue(fileName);
        objectThatInteract.playerController.UnlockPlayer();
        onCinematicEnd.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        objectThatInteract = other.transform.GetChild(1).GetComponent<InteractBehaviour>();
        StartCinematic(objectThatInteract);
    }
}
