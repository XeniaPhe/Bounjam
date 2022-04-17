using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);  //Next scene
    }
    public void Quit()
    {
        Application.Quit();
    }
}