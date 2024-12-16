using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CinematicBehaviour : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private string fileName;
    [SerializeField] private UnityEvent onCinematicEnd;

    private InteractBehaviour objectThatInteract;

    public void StartCinematic(InteractBehaviour interactBehaviour)
    {
        objectThatInteract = interactBehaviour;
        StartCoroutine(StartCinematicCoroutine());
    }

    IEnumerator StartCinematicCoroutine()
    {
        yield return new WaitUntil(() => objectThatInteract.playerController.IsOnFloor());
        objectThatInteract.playerController.LockPlayer();

        Debug.Log("Movement Blocked!");
        yield return dialogueController.StartDialogue(fileName);
        Debug.Log("Dialogue ended!");
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
