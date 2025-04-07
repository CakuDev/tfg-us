using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CinematicMovement : CinematicItem
{
    private MovementBehaviour objectToMove;
    private Vector3 positionToMove;
    private bool checkX;
    private bool checkY;
    private bool checkZ;

    public CinematicMovement(MovementBehaviour objectToMove, Vector3 positionToMove, bool checkX, bool checkY, bool checkZ)
    {
        this.objectToMove = objectToMove;
        this.positionToMove = positionToMove;
        this.checkX = checkX;
        this.checkY = checkY;
        this.checkZ = checkZ;
    }

    public override IEnumerator Action()
    {
        objectToMove.canMove = true;
        while (!objectToMove.MoveToPosition(positionToMove, checkX, checkY, checkZ))
        {
            yield return new WaitForFixedUpdate();
        }
        objectToMove.canMove = false;
    }
}
