using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private JumpBehaviour jumpBehaviour;
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private Transform models;

    private List<Animator> animators = new ();

    private void Start()
    {
        GameController gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
        Instantiate(gameController.GetCurrentHead(), models);
        Instantiate(gameController.GetCurrentBody(), models);
        InitAnimators();
    }

    void FixedUpdate()
    {
        ManageMovement();
    }

    private void Update()
    {
        ManageJump();
        ManageInteract();
    }

    private void ManageMovement()
    {
        Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement.Normalize();
        movementBehaviour.Move(movement);
        SetBooleanAnimatorValue("is_walking", movement.magnitude != 0);
    }

    private void ManageJump()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        jumpBehaviour.Jump();
        SetBooleanAnimatorValue("is_jumping", true);
    }

    private void ManageInteract()
    {
        if (Input.GetKeyDown(KeyCode.E)) interactBehaviour.Interact();
    }

    private void SetBooleanAnimatorValue(string parameterName, bool value)
    {
        animators.ForEach(animator => animator.SetBool(parameterName, value));
    }

    public void LockPlayer()
    {
        movementBehaviour.canMove = false;
        jumpBehaviour.canJump = false;
        interactBehaviour.canInteract = false;
    }

    public void UnlockPlayer()
    {
        movementBehaviour.canMove = true;
        jumpBehaviour.canJump = true;
        interactBehaviour.canInteract = true;
    }

    public void InitAnimators()
    {
        foreach (Transform child in models)
        {
            animators.Add(child.gameObject.GetComponent<Animator>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor")) SetBooleanAnimatorValue("is_jumping", false);
    }
}
