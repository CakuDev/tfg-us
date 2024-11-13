using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMovementBehaviour : MonoBehaviour
{
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private List<Animator> animators = new();


    // Update is called once per frame
    void FixedUpdate()
    {
        ManageMovement();
    }

    private void ManageMovement()
    {
        Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement.Normalize();
        movementBehaviour.Move(movement);
        SetBooleanAnimatorValue("is_walking", movement.magnitude != 0);
    }

    private void SetBooleanAnimatorValue(string parameterName, bool value)
    {
        animators.ForEach(animator => animator.SetBool(parameterName, value));
    }
}
