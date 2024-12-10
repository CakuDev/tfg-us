using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Condition
{
    public ProgressCondition progressCondition;
    public ProgressSave progressSave;
}

public class ShowByProgressBehaviour : MonoBehaviour
{
    [SerializeField] private List<Condition> conditions;

    void Start()
    {
        conditions.ForEach(c => CheckCondition(c));
    }

    private void CheckCondition(Condition condition)
    {
        bool alreadySaved = PlayerPrefs.HasKey(condition.progressSave.ToString());
        if (condition.progressCondition == ProgressCondition.SHOW && alreadySaved) gameObject.SetActive(true);
        if (condition.progressCondition == ProgressCondition.HIDE && alreadySaved) gameObject.SetActive(false);
    }
}
