using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveProgressBehaviour : MonoBehaviour
{
    [SerializeField] private ProgressSave progressSave;

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(progressSave.ToString(), 1);
    }
}
