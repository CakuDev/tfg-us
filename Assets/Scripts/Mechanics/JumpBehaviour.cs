using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Vector3 jumpDirection;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<Animator> animators;
    [SerializeField] private AudioSource jumpSfx;

    public bool canJump = false;
    [HideInInspector] public bool isOnFloor = true;

    public void Jump()
    {
        if (!isOnFloor || !canJump) return;
        
        jumpSfx.Play();
        animators.ForEach(animator => animator.SetBool("is_jumping", true));
        rb.velocity = Vector3.zero;
        rb.AddForce(jumpForce * jumpDirection.normalized,ForceMode.Impulse);
        isOnFloor = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isOnFloor) return;
        if (!collision.transform.CompareTag("Floor") && !collision.transform.CompareTag("Destroyable")) return;
        
        animators.ForEach(animator => animator.SetBool("is_jumping", false));
        isOnFloor = true;
        rb.velocity = Vector3.zero;
    }

    public void SetAnimators(List<Animator> animators)
    {
        this.animators = new(animators);
    }
}
