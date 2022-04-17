using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip ambianceAudioClip;
    public AudioClip battleAudioClip;

    public void PlayBattleMusic()
    {
        audioSource.Stop();
        audioSource.clip = battleAudioClip;
        audioSource.Play();
    }

    public void PlayAmbianceMusic()
    {
        audioSource.Stop();
        audioSource.clip = ambianceAudioClip;
        audioSource.Play();
    }
}
