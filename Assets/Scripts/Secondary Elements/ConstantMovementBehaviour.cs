using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovementBehaviour : MonoBehaviour
{
    [SerializeField] private MovementBehaviour movementBehaviour;
    public Vector3 direction;

    void FixedUpdate()
    {
        movementBehaviour.Move(direction.normalized);
    }
}
