using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCinematic : MonoBehaviour
{
    public float startCinematicEndTime = 70f;
    public float speed = 0.05f;
    public Transform mainCamera;

    public static bool isCinematicActive;

    public AudioSource audioSource;
    public AudioSource generalAudioSource1;
    public AudioSource generalAudioSource2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            mainCamera.transform.GetChild(0).gameObject.SetActive(false);
            isCinematicActive = true;
            generalAudioSource1.Stop();
            generalAudioSource2.Stop();
            audioSource.Play();
            StartCoroutine(EndEndCinematic());
        }
    }

    public IEnumerator EndEndCinematic()
    {
        yield return new WaitForSeconds(startCinematicEndTime);
        audioSource.Stop();
        Application.Quit();
        //Playere update!!!
    }

    private void Update()
    {
        if (isCinematicActive) mainCamera.transform.position += new Vector3(speed * Time.timeScale, 0f, 0f);
    }
}
