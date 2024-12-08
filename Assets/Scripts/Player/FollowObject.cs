using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private bool moveOnX = false;
    [SerializeField] private bool moveOnY = false;
    [SerializeField] private bool moveOnZ = false;
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        if(objectToFollow == null) return;

        Vector3 position = transform.position;
        if (moveOnX) position.x = objectToFollow.position.x + offset.x;
        if (moveOnY) position.y = objectToFollow.position.y + offset.y;
        if (moveOnZ) position.z = objectToFollow.position.z + offset.z;
        transform.position = position;
    }
}
