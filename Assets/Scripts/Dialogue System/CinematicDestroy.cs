using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicDestroy : CinematicItem
{
    private GameObject objectToDestroy;
    private Action<GameObject> DestroyAction;

    public CinematicDestroy(GameObject objectToDestroy, Action<GameObject> destroyAction)
    {
        this.objectToDestroy = objectToDestroy;
        DestroyAction = destroyAction;
    }

    public override IEnumerator Action()
    {
        DestroyAction(objectToDestroy);
        yield return null;
    }
}
