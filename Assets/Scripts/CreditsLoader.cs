using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class CreditsLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup fader;
    [SerializeField] int fadeFrameCount;
    [SerializeField] int creditsFps;
    [SerializeField] TMP_Text[] texts;
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
        fader.alpha = 0;
        foreach (var item in texts)
            item.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        fader.gameObject.SetActive(true);
        fader.alpha = 0f;
        foreach (var item in texts)
            item.gameObject.SetActive(false);
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

        StartCoroutine(LoadCredits());
    }

    IEnumerator LoadCredits()
    {
        int counter = 0;
        do
        {
            yield return waitNewName;
            texts[counter++].gameObject.SetActive(true);
        } while (counter<texts.Length);

        yield return null;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}