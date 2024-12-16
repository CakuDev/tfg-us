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

    public bool canMove = true;

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

    public bool MoveToPosition(Vector3 objective, bool checkX, bool checkY, bool checkZ)
    {
        if (!canMove) return true;

        if (!checkX) objective.x = transform.position.x;
        if (!checkY) objective.y = transform.position.y;
        if (!checkZ) objective.z = transform.position.z;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, objective, maxSpeed * Time.fixedDeltaTime);
        transform.position = newPosition;

        // Rotate character
        if (rotateWithMovement && !Vector3.zero.Equals(objective - transform.position))
        {
            Vector3 rotationVector = objective - transform.position;
            if (!checkYSpeed) rotationVector.y = 0;
            Quaternion rotation = Quaternion.LookRotation(rotationVector);
            rb.MoveRotation(rotation);
        }
        Debug.Log(newPosition);
        Debug.Log(objective);
        bool objectiveReached = transform.position == objective;
        animators.ForEach(animator => animator.SetBool("is_walking", !objectiveReached));
        return objectiveReached;
    }

    public void SetAnimators(List<Animator> animators)
    {
        this.animators = new(animators);
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }
}
