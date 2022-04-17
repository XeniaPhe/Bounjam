using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CreditsLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup fader;
    [SerializeField] int fadeFrameCount;
    [SerializeField] int creditsFps;
    [SerializeField] string[] creditsNames;
    string thanks = "Thanks for playing our game!";
    [SerializeField] float timeBetweenNames;

    WaitForSeconds oneOverFps;
    WaitForSeconds waitNewName;

    float delta;
    private void Start()
    {
        fader.gameObject.SetActive(false);
        oneOverFps = new WaitForSeconds(1f / creditsFps);
        waitNewName = new WaitForSeconds(timeBetweenNames);
        delta = 1f / fadeFrameCount;
    }
    public void EndGame()
    {
        fader.gameObject.SetActive(true);
        fader.alpha = 0f;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (int i = 0; i < fadeFrameCount; i++)
        {
            fader.alpha += delta;
            yield return oneOverFps;
        }

        yield return null;
    }

    IEnumerator LoadCredits()
    {
        foreach (var item in creditsNames)
        {

        }

        yield return null;  
    }
}