using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Instance;

    public AudioSource skillAudioSource;

    public List<AudioClip> skillAudioClips;

    private void Awake() => Instance = this;

    public void PlayAttackVFX()
    {
        skillAudioSource.clip = skillAudioClips[Random.Range(0, skillAudioClips.Count)];
        skillAudioSource.Play();
    }
}
