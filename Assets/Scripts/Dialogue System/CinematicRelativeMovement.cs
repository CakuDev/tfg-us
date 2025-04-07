using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicRelativeMovement : CinematicItem
{

    private MovementBehaviour objectToMove;
    private Vector3 positionToMove;
    private bool checkX;
    private bool checkY;
    private bool checkZ;

    public CinematicRelativeMovement(MovementBehaviour objectToMove, Vector3 positionToMove, bool checkX, bool checkY, bool checkZ, bool isAsync)
    {
        this.objectToMove = objectToMove;
        this.positionToMove = positionToMove;
        this.checkX = checkX;
        this.checkY = checkY;
        this.checkZ = checkZ;
        base.isAsync = isAsync;
    }

    public override IEnumerator Action()
    {
        positionToMove += objectToMove.transform.position;
        objectToMove.canMove = true;
        while (!objectToMove.MoveToPosition(positionToMove, checkX, checkY, checkZ))
        {
            yield return new WaitForFixedUpdate();
        }
        objectToMove.canMove = false;
    }
}
