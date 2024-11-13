using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody rb;

    [HideInInspector] public bool canJump = true;


    public void Jump()
    {
        if (!canJump) return;

        rb.AddForce(jumpForce * Vector3.up,ForceMode.Impulse);
        canJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Floor")) canJump = true;
    }
}
