using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DebugScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in Enum.GetValues(typeof(ProgressSave)))
        {
            string value = item.ToString();
            if(PlayerPrefs.HasKey(value)) text.text += value + "\n";
        }
    }
}
