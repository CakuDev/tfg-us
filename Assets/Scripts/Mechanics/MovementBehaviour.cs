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
    [SerializeField] private List<Animator> animators;

    [HideInInspector] public bool canMove = true;

    public void Move(Vector3 movementDirection)
    {
        if (!canMove) 
        {
            animators.ForEach(animator => animator.SetBool("is_walking", false));
            rb.velocity = new(0f, rb.velocity.y, 0f);
            return;
        }

        animators.ForEach(animator => animator.SetBool("is_walking", movementDirection.magnitude != 0));

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

    public bool MoveToPosition(Vector3 objective)
    {
        if (!canMove) return true;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, objective, maxSpeed * Time.fixedDeltaTime);
        transform.position = newPosition;

        // Rotate character
        if (rotateWithMovement && !Vector3.zero.Equals(objective - transform.position))
        {
            Quaternion rotation = Quaternion.LookRotation(objective - transform.position);
            rb.MoveRotation(rotation);
        }

        bool objectiveReached = newPosition == objective;
        animators.ForEach(animator => animator.SetBool("is_walking", !objectiveReached));
        return objectiveReached;
    }

    public void SetAnimators(List<Animator> animators)
    {
        this.animators = new(animators);
    }
}
