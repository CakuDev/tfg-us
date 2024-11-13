using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour : MonoBehaviour
{
    public const string GAME_CONTROLLER = "GameController";

    private static Dictionary<string, GameObject> entities = new();
    
    void Awake()
    {
        if (entities.ContainsKey(gameObject.name)) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            entities[gameObject.name] = gameObject;
        }
    }

    public static GameObject GetEntity(string name)
    {
        return entities[name];
    }
}
