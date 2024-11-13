using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    private const string isToggleAnimVariable = "is_toggle";

    [SerializeField] private Animator buttonAnimator;
    [SerializeField] UnityEvent onToggleOn;
    [SerializeField] UnityEvent onToggleOff;

    public void SetIsToggleAnimation(bool value)
    {
        buttonAnimator.SetBool(isToggleAnimVariable, value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) onToggleOn.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) onToggleOff.Invoke();
    }
}
