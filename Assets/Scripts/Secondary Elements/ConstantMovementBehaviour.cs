using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovementBehaviour : MonoBehaviour
{
    [SerializeField] private MovementBehaviour movementBehaviour;
    public Vector3 direction;

    public void StartMoving(Vector3 direction, float speed)
    {
        movementBehaviour.maxSpeed = speed;
        this.direction = direction;
    }

    void FixedUpdate()
    {
        movementBehaviour.Move(direction.normalized);
    }
}
