using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<Animator> animators;

    public bool canJump = true;
    [HideInInspector] public bool isOnFloor = true;

    public void Jump()
    {
        if (!canJump) return;

        animators.ForEach(animator => animator.SetBool("is_jumping", true));
        rb.AddForce(jumpForce * Vector3.up,ForceMode.Impulse);
        canJump = false;
        isOnFloor = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            animators.ForEach(animator => animator.SetBool("is_jumping", false));
            canJump = true;
            isOnFloor = true;
        }
    }

    public void SetAnimators(List<Animator> animators)
    {
        this.animators = new(animators);
    }
}
