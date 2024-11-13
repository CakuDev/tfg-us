using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    public PlayerController playerController;

    [HideInInspector] public bool canInteract = true;
    private InteractiveItem itemToInteract;

    public void Interact()
    {
        if (!canInteract || itemToInteract == null) return;

        itemToInteract.OnInteract(this);
    }

    public void EndInteraction()
    {
        Debug.Log("Can interact again");
        canInteract = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InteractiveItem interactiveItem)) itemToInteract = interactiveItem;
    }

    private void OnTriggerExit(Collider other)
    {
        if (itemToInteract != null && other.gameObject == itemToInteract.gameObject) itemToInteract = null;
    }
}
