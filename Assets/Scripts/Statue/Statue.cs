using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public void OpenUpgradePanel() => PlayerStats.Instance.OpenUpgradePlayerUI();
}
