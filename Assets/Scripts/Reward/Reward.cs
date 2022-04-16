using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public void GetReward()
    {
        PlayerStats.Instance.IncreaseTrebleClef();
        gameObject.SetActive(false);
    }
}
