using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicHideCanvas : CinematicItem
{
    private GameObject canvas;

    public CinematicHideCanvas(GameObject canvas)
    {
        this.canvas = canvas;
    }

    public override IEnumerator Action()
    {
        canvas.SetActive(false);
        yield return null;
    }
}
