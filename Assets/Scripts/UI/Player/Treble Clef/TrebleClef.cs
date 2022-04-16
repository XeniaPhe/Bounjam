using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrebleClef : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void OnTrebleClefChange() => text.text = PlayerStats.Instance.trebleClef.ToString();
}
