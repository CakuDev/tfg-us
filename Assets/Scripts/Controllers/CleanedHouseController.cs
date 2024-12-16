using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanedHouseController : MonoBehaviour
{
    [SerializeField] private InteractBehaviour interactBehaviour;
    [SerializeField] private CinematicBehaviour cinematicBehaviour;

    private bool alreadyStarted = false;

    void Update()
    {
        if (!alreadyStarted && IsCleanedHouse())
        {
            alreadyStarted = true;
            cinematicBehaviour.StartCinematic(interactBehaviour);
        }
    }

    private bool IsCleanedHouse()
    {
        return PlayerPrefs.HasKey(TidyObjectBehaviour.SAVE_PREFIX + "Interactive Bookshelf") &&
            PlayerPrefs.HasKey(TidyObjectBehaviour.SAVE_PREFIX + "Interactive Table") &&
            PlayerPrefs.HasKey(TidyObjectBehaviour.SAVE_PREFIX + "Interactive Cabinet Living Room") &&
            PlayerPrefs.HasKey(TidyObjectBehaviour.SAVE_PREFIX + "Interactive Bed") &&
            PlayerPrefs.HasKey(TidyObjectBehaviour.SAVE_PREFIX + "Interactive Cabinet") && 
            PlayerPrefs.HasKey(TidyObjectBehaviour.SAVE_PREFIX + "Interactive Desk");
    }
}
