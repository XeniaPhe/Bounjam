using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoSave : MonoBehaviour
{
    public Statue lastStatue;

    public void Continue()
    {
        if (!lastStatue) return;
        else
        {
            Player.Instance.transform.position = lastStatue.transform.position;
            PlayerStats.Instance.health = PlayerStats.Instance.maxHealth;
        }
    }
}
