using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicScale : CinematicItem
{
    private Transform objectToScale;
    private float speed;
    private Vector3 scaleObjective;

    public CinematicScale(Transform objectToScale, float speed, Vector3 scaleObjective)
    {
        this.objectToScale = objectToScale;
        this.speed = speed;
        this.scaleObjective = scaleObjective;
    }

    public override IEnumerator Action()
    {
        int sign = objectToScale.localScale.x > scaleObjective.x ? 
            -1 
            : 1;
        Vector3 frameVariation = sign * new Vector3(speed, speed, speed);
        while(Vector3.Distance(objectToScale.localScale, scaleObjective) >= speed)
        {
            objectToScale.localScale += frameVariation;
            yield return new WaitForFixedUpdate();
        }

        objectToScale.localScale = scaleObjective;
    }
}
