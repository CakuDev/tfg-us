using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CinematicMovement : CinematicItem
{
    const float ERROR_MARGIN = 0.0001f;

    private MovementBehaviour objectToMove;
    private Vector3 positionToMove;

    public CinematicMovement(MovementBehaviour objectToMove, Vector3 positionToMove)
    {
        this.objectToMove = objectToMove;
        this.positionToMove = positionToMove;
    }

    public override IEnumerator Action()
    {
        objectToMove.canMove = true;
        while (!objectToMove.MoveToPosition(positionToMove))
        {
            yield return new WaitForFixedUpdate();
        }
        objectToMove.transform.position = positionToMove;
        objectToMove.canMove = false;
    }
}
