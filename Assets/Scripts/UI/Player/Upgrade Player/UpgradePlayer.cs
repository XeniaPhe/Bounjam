using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePlayer : MonoBehaviour
{
    public TextMeshProUGUI attackLevelText;
    public TextMeshProUGUI healthLevelText;
    public TextMeshProUGUI speedLevelText;

    public void ToggleUpgradePanel() => gameObject.SetActive(!gameObject.activeSelf);

    public void IncreaseAttack()
    {
        PlayerStats.Instance.IncreaseAttack();
        attackLevelText.text = "Damage " + PlayerStats.Instance.attackDamage.ToString();
    }

    public void IncreaseHealth()
    {
        PlayerStats.Instance.IncreaseHealth();
        healthLevelText.text = "Max " + PlayerStats.Instance.maxHealth.ToString();
    }

    public void IncreaseSpeed()
    {
        PlayerStats.Instance.IncreaseSpeed();
        speedLevelText.text = "Speed " + PlayerStats.Instance.speed.ToString();
    }
}
