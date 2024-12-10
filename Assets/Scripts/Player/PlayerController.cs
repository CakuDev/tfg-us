using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementBehaviour movementBehaviour;
    [SerializeField] private JumpBehaviour jumpBehaviour;
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private Transform models;

    private bool movementBlocked = false;

    private void Start()
    {
        GameController gameController = SingletonBehaviour.GetEntity(SingletonBehaviour.GAME_CONTROLLER).GetComponent<GameController>();
        Instantiate(gameController.GetCurrentHead(), models);
        Instantiate(gameController.GetCurrentBody(), models);
        InitAnimators();
        interactBehaviour.playerController = this;
    }

    void FixedUpdate()
    {
        if(!movementBlocked) ManageMovement();
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
        if(!jumpBehaviour.canJump) movementBehaviour.Move(movement);
    }

    private void ManageJump()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        jumpBehaviour.Jump();
    }

    private void ManageInteract()
    {
        if (Input.GetKeyDown(KeyCode.E)) interactBehaviour.Interact();
    }

    public void LockPlayer()
    {
        movementBehaviour.canMove = false;
        interactBehaviour.canInteract = false;
        movementBlocked = true;
        movementBehaviour.Stop();
    }

    public void UnlockPlayer()
    {
        movementBehaviour.canMove = true;
        interactBehaviour.canInteract = true;
        movementBlocked = false;
    }

    public void InitAnimators()
    {
        List<Animator> animators = new ();
        foreach (Transform child in models)
        {
            animators.Add(child.gameObject.GetComponent<Animator>());
        }

        movementBehaviour.SetAnimators(animators);
        jumpBehaviour.SetAnimators(animators);
    }

    public bool IsOnFloor()
    {
        return jumpBehaviour.isOnFloor;
    }
}
