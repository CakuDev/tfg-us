using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Destroyable")) Destroy(other.gameObject);
    }
}
