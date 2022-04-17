using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Stats")]
    public float attackDamage = 1;
    public float health = 25;
    public float healthRegenerationCooldown = 0.5f;
    public float maxHealth = 100;
    public int trebleClef = 0;
    public float speed = 1;
    [Header("UI")]
    public PlayerHealthBar playerHealthBar;
    public TrebleClef playerTrebleClef;
    public UpgradePlayer upgradePlayer;

    private void Awake() => Instance = this;

    private void Start() => CoroutineStarter();

    public void CoroutineStarter() => StartCoroutine(Regeneration());

    public void OpenUpgradePlayerUI() => upgradePlayer.ToggleUpgradePanel();

    public void IncreaseAttack()
    {
        if (trebleClef < 2) return;
        DecreaseTrebleClef(2);
        attackDamage += 0.1f;
    }

    public void IncreaseHealth()
    {
        if (trebleClef < 1) return;
        DecreaseTrebleClef(1);
        maxHealth += 1;
    }

    public void IncreaseSpeed()
    {
        if (trebleClef < 1) return;
        DecreaseTrebleClef(1);
        speed += 0.01f;
    }

    public void IncreaseTrebleClef()
    {
        trebleClef += 1;
        playerTrebleClef.OnTrebleClefChange();
    }

    public void DecreaseTrebleClef(int value)
    {
        trebleClef -= value;
        playerTrebleClef.OnTrebleClefChange();
    }

    public void TakeDamage(float damage)
    {
        if ((health -= damage) <= 0) Die();
        playerHealthBar.OnHealthBarChange();
    }

    public void Die() => Destroy(gameObject);

    public IEnumerator Regeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(healthRegenerationCooldown);
            if(health < maxHealth) health += 1;
            playerHealthBar.OnHealthBarChange();
        }
    }
}
