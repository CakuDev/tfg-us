using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleBehaviour : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    void Start()
    {
        toggle.isOn = Screen.fullScreen;
    }

    public void OnToggleChange(bool value)
    {
        Screen.fullScreen = value;
    }
}
