using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoSave : MonoBehaviour
{

    public static PlayerAutoSave Instance;

    public Statue lastStatue;

    private void Awake() => Instance = this;

    public void Continue()
    {
        if (!lastStatue)
        {
            Player.Instance.transform.position = new Vector3(2.540001f, -5.539993f, -30.38763f);
            PlayerStats.Instance.Rebirth();
        }
        else
        {
            Player.Instance.transform.position = lastStatue.transform.position;
            PlayerStats.Instance.Rebirth();
        }
    }
}
