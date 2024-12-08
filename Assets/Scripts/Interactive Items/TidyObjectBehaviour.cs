using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TidyObjectBehaviour : InteractiveItem
{
    public const string SAVE_PREFIX = "TIDY_OBJECT.";

    [SerializeField] float tidySpeed;
    [SerializeField] float tidyPauseTime;
    [SerializeField] AudioSource tidySound;
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject untidyObject;
    [SerializeField] GameObject tidyObject;

    private void Start()
    {
        if(PlayerPrefs.HasKey(SAVE_PREFIX + gameObject.name))
        {
            untidyObject.SetActive(false);
            tidyObject.SetActive(true);
        }
    }

    public override void OnInteract(InteractBehaviour objectThatInteract)
    {
        if (tidyObject.activeSelf) return;

        base.OnInteract(objectThatInteract);
        PlayerPrefs.SetInt(SAVE_PREFIX + gameObject.name, 1);
        StartCoroutine(TidyObject());
    }

    IEnumerator TidyObject()
    {
        objectThatInteract.playerController.LockPlayer();

        blackScreen.gameObject.SetActive(true);
        Color imageColor = blackScreen.color;
        
        while (imageColor.a < 1f)
        {
            imageColor.a += tidySpeed * Time.deltaTime;
            blackScreen.color = imageColor;
            yield return new WaitForEndOfFrame();
        }

        untidyObject.SetActive(false);
        tidyObject.SetActive(true);
        tidySound.Play();

        yield return new WaitForSeconds(tidyPauseTime);

        while (imageColor.a > 0f)
        {
            imageColor.a -= tidySpeed * Time.deltaTime;
            blackScreen.color = imageColor;
            yield return new WaitForEndOfFrame();
        }

        blackScreen.gameObject.SetActive(false);
        objectThatInteract.playerController.UnlockPlayer();
    }
}
