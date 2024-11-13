using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<GameObject> playerHeads = new();
    public List<GameObject> playerBodies = new();

    [HideInInspector] public int previousScene = -1;
    public int currentHead = -1;
    public int currentBody = -1;
    public string playerName = "";
    public Gender playerGender;

    public GameObject GetCurrentHead()
    {
        return playerHeads[currentHead];
    }

    public GameObject GetCurrentBody()
    {
        return playerBodies[currentBody];
    }
}
