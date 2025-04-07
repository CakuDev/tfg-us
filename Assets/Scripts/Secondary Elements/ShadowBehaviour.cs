using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBehaviour : MonoBehaviour
{
    [SerializeField] private float minScale = 0.5f;

    private float maxScale;
    private GameObject objectShadow;
    private float initialObjectDistance;

    private void Awake()
    {
        maxScale = transform.localScale.x;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (objectShadow == null) return;

        float t = Vector3.Distance(transform.position, objectShadow.transform.position) / initialObjectDistance;
        float currentScaleValue = Mathf.Lerp(maxScale, minScale, t);
        Vector3 currentScale = new(currentScaleValue, currentScaleValue, maxScale);
        transform.localScale = currentScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
        objectShadow = null;
    }

    public void SetObjectShadow(GameObject objectShadow)
    {
        gameObject.SetActive(true);
        this.objectShadow = objectShadow;
        initialObjectDistance = Vector3.Distance(transform.position, objectShadow.transform.position);
    }
}
