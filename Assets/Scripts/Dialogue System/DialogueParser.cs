using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    private GameObject dialogueCanvas;
    private TMP_Text nameText;
    private TMP_Text dialogueText;
    private float defaultTextSpeed;
    private float skipTextSpeed;
    private GameController gameController;
    [SerializeField] AudioSource textSfx;

    private const string SEPARATOR = "||";
    private const string POSITION_SEPARATOR = ";";
    private const string SAME_CORD = "_";
    private const string DIALOGUE_LINE_ID = "dialogue";
    private const string MOVEMENT_LINE_ID = "move";
    private const string MOVEMENT_RELATIVE_LINE_ID = "move_relative";
    private const string HIDE_CANVAS_LINE_ID = "hide_canvas";
    private const string PLAYER_NAME_IN_DIALOGUE = "player_name";
    private const string ASYNC = "async";
    private const string SCALE_ID = "scale";
    private const string DESTROY_ID = "destroy";

    public List<CinematicItem> ParseFileLines(List<string> fileLines, GameObject dialogueCanvas, TMP_Text nameText, TMP_Text dialogueText, float defaultTextSpeed, float skipTextSpeed, GameController gameController)
    {
        this.dialogueCanvas = dialogueCanvas;
        this.nameText = nameText;
        this.dialogueText = dialogueText;
        this.defaultTextSpeed = defaultTextSpeed;
        this.skipTextSpeed = skipTextSpeed;
        this.gameController = gameController;

        List<CinematicItem> result = new();
        foreach (string line in fileLines)
        {
            result.Add(ParseFileLine(line));
        }

        return result;
    }

    private CinematicItem ParseFileLine(string line)
    {
        string[] lineSplit = line.Split(SEPARATOR);
        
        return lineSplit[0] switch
        {
            DIALOGUE_LINE_ID => ParseCinematicDialogue(lineSplit),
            MOVEMENT_LINE_ID => ParseCinematicMovement(lineSplit),
            HIDE_CANVAS_LINE_ID => ParseCinematicHideCanvas(),
            MOVEMENT_RELATIVE_LINE_ID => ParseCinematicMovementRelative(lineSplit),
            SCALE_ID => ParseCinematicScale(lineSplit),
            DESTROY_ID => ParseCinematicDestroy(lineSplit),
            _ => DebugError(line, lineSplit),
        };
    }

    private CinematicItem DebugError(string line, string[] lineSplit)
    {
        Debug.Log("ERROR PARSEANDO LA SIGUIENTE LÍNEA: " + line);
        Debug.Log(lineSplit[0]);
        Debug.Log(lineSplit[0] == HIDE_CANVAS_LINE_ID);
        return null;
    }

    private CinematicDialogue ParseCinematicDialogue(string[] lineSplit)
    {
        string name = lineSplit[1] == PLAYER_NAME_IN_DIALOGUE ? gameController.playerName : lineSplit[1];
        string text = lineSplit[2];
        text = text.Replace(PLAYER_NAME_IN_DIALOGUE, gameController.playerName);
        return new(this, defaultTextSpeed, skipTextSpeed, name, text, dialogueCanvas, nameText, dialogueText, textSfx);
    }

    private CinematicMovement ParseCinematicMovement(string[] lineSplit)
    {

        MovementBehaviour movementBehaviour = GameObject.Find(lineSplit[1]).GetComponent<MovementBehaviour>();
        string[] positionString = lineSplit[2].Split(POSITION_SEPARATOR);
        bool checkX = positionString[0] != SAME_CORD;
        bool checkY = positionString[1] != SAME_CORD;
        bool checkZ = positionString[2] != SAME_CORD;
        float xCord = checkX ? float.Parse(positionString[0]) : movementBehaviour.transform.position.x;
        float yCord = checkY ? float.Parse(positionString[1]) : movementBehaviour.transform.position.y;
        float zCord = checkZ ? float.Parse(positionString[2]) : movementBehaviour.transform.position.z;
        Vector3 positionToMove = new(xCord, yCord, zCord);
        return new(movementBehaviour, positionToMove, checkX, checkY, checkZ);
    }

    private CinematicRelativeMovement ParseCinematicMovementRelative(string[] lineSplit)
    {
        MovementBehaviour movementBehaviour = GameObject.Find(lineSplit[1]).GetComponent<MovementBehaviour>();
        string[] positionString = lineSplit[2].Split(POSITION_SEPARATOR);
        bool checkX = positionString[0] != SAME_CORD;
        bool checkY = positionString[1] != SAME_CORD;
        bool checkZ = positionString[2] != SAME_CORD;
        float xCord = checkX ? float.Parse(positionString[0]) : 0;
        float yCord = checkY ? float.Parse(positionString[1]) : 0;
        float zCord = checkZ ? float.Parse(positionString[2]) : 0;
        Vector3 positionToMove = new(xCord, yCord, zCord);
        bool isAsync = lineSplit.Length == 4 && lineSplit[3] == ASYNC;
        return new(movementBehaviour, positionToMove, checkX, checkY, checkZ, isAsync);
    }

    private CinematicHideCanvas ParseCinematicHideCanvas()
    {
        return new(dialogueCanvas);
    }

    private CinematicScale ParseCinematicScale(string[] lineSplit)
    {

        Transform objectToScale = GameObject.Find(lineSplit[1]).transform;
        float speed = float.Parse(lineSplit[2]);
        string[] positionString = lineSplit[3].Split(POSITION_SEPARATOR);
        float xCord = float.Parse(positionString[0]);
        float yCord = float.Parse(positionString[1]);
        float zCord = float.Parse(positionString[2]);
        Vector3 scaleObjective = new(xCord, yCord, zCord);
        return new(objectToScale, speed, scaleObjective);
    }

    private CinematicDestroy ParseCinematicDestroy(string[] lineSplit)
    {

        GameObject objectToDestroy = GameObject.Find(lineSplit[1]);
        return new(objectToDestroy, DestroyObject);
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
