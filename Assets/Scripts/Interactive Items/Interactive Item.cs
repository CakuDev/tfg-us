using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    internal InteractBehaviour objectThatInteract;

    public virtual void OnInteract(InteractBehaviour itemThatInteract)
    {
        objectThatInteract = itemThatInteract;
    }
}
