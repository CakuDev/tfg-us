using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CinematicItem
{
    public bool isAsync = false;
    public abstract IEnumerator Action();
}
