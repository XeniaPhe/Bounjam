using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public AudioSource rewardAudioSource;

    public void GetReward()
    {
        PlayerStats.Instance.IncreaseTrebleClef();
        rewardAudioSource.Play();
        gameObject.SetActive(false);
    }
}
