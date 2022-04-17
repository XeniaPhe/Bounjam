using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCinematic : MonoBehaviour
{
    bool isCinematicActive = true;

    public AudioSource audioSource;

    public Transform mainCamera;

    public float startCinematicEndTime = 62f;

    public float speed = 0.05f;

    void Start()
    {
        audioSource.Play();
        StartCoroutine(EndStartCinematic());
    }

    private void Update()
    {
        if(isCinematicActive) mainCamera.transform.position += new Vector3(speed * Time.timeScale, 0f, 0f);
    }

    public IEnumerator EndStartCinematic()
    {
        yield return new WaitForSeconds(startCinematicEndTime);
        mainCamera.transform.GetChild(0).gameObject.SetActive(true);
        audioSource.Stop();
        isCinematicActive = false;
        //Playere update!!!
    }
}
