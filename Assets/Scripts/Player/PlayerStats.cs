using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Stats")]
    public int attackDamage = 1;
    public int health = 25;
    public float healthRegenerationCooldown = 0.5f;
    public int maxHealth = 100;
    public int trebleClef = 0;
    [Header("UI")]
    public PlayerHealthBar playerHealthBar;
    public TrebleClef playerTrebleClef;
    public UpgradePlayer upgradePlayer;

    private void Awake() => Instance = this;

    private void Start() => CoroutineStarter();

    public void CoroutineStarter() => StartCoroutine(Regeneration());

    public void OpenUpgradePlayerUI()
    {
        upgradePlayer.ToggleUpgradePanel();
    }

    public void IncreaseTrebleClef()
    {
        trebleClef += 1;
        playerTrebleClef.OnTrebleClefChange();
    }

    public void TakeDamage(int damage)
    {
        if ((health -= damage) <= 0) Die();
        playerHealthBar.OnHealthBarChange(health, maxHealth);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public IEnumerator Regeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(healthRegenerationCooldown);
            if(health < 100) health += 1;
            playerHealthBar.OnHealthBarChange(health, maxHealth);
        }
    }
}
