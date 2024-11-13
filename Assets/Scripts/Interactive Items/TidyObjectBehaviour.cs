using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TidyObjectBehaviour : InteractiveItem
{
    [SerializeField] float tidySpeed;
    [SerializeField] float tidyPauseTime;
    [SerializeField] AudioSource tidySound;
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject untidyObject;
    [SerializeField] GameObject tidyObject;

    public override void OnInteract(InteractBehaviour objectThatInteract)
    {
        if (tidyObject.activeSelf) return;

        base.OnInteract(objectThatInteract);
        Debug.Log("Limpiado " + gameObject.name + " por: " + objectThatInteract.name);
        StartCoroutine(TidyObject());
    }

    IEnumerator TidyObject()
    {
        objectThatInteract.playerController.LockPlayer();

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

        objectThatInteract.playerController.UnlockPlayer();
    }
}
