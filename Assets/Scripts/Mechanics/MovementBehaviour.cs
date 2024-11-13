using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class MovementBehaviour : MonoBehaviour
{
    public float speed = 100f;
    public float maxSpeed = 2f;
    [SerializeField] private bool checkYSpeed = false;
    [SerializeField] private bool rotateWithMovement = true;
    [SerializeField] private Rigidbody rb;

    [HideInInspector] public bool canMove = true;

    public void Move(Vector3 movementDirection)
    {
        if (!canMove) return;

        // If no movement, then stop except jump velocity
        if (movementDirection.magnitude == 0)
        {
            rb.velocity = new(0, rb.velocity.y, 0);
        }
        else
        {
            // Rotate character
            if (rotateWithMovement)
            {
                Quaternion rotation = Quaternion.LookRotation(movementDirection);
                rb.MoveRotation(rotation);
            }

            // Check for max speed
            Vector3 movement = rb.velocity;
            if (!checkYSpeed) movement.y = 0;
            if (movement.magnitude < maxSpeed)
            {
                // Add force to move
                rb.AddForce(speed * movementDirection);
                
            }
            else
            {
                // Limit max speed
                Vector3 newVelocity = movement.normalized * maxSpeed;
                newVelocity.y = rb.velocity.y;
                rb.velocity = newVelocity;
            }
        }
    }
}
