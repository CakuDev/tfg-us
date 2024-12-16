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

    private const string SEPARATOR = "||";
    private const string POSITION_SEPARATOR = ";";
    private const string SAME_CORD = "_";
    private const string DIALOGUE_LINE_ID = "dialogue";
    private const string MOVEMENT_LINE_ID = "move";
    private const string HIDE_CANVAS_LINE_ID = "hide_canvas";
    private const string PLAYER_NAME_IN_DIALOGUE = "player_name";

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
            _ => null,
        };
    }

    private CinematicDialogue ParseCinematicDialogue(string[] lineSplit)
    {
        string name = lineSplit[1] == PLAYER_NAME_IN_DIALOGUE ? gameController.playerName : lineSplit[1];
        string text = lineSplit[2];
        text = text.Replace(PLAYER_NAME_IN_DIALOGUE, gameController.playerName);
        return new(this, defaultTextSpeed, skipTextSpeed, name, text, dialogueCanvas, nameText, dialogueText);
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

    private CinematicHideCanvas ParseCinematicHideCanvas()
    {
        return new(dialogueCanvas);
    }
}
