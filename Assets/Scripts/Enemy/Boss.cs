using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyAI
{
    public override void TakeDamage(float damage)
    {
        health -= damage;
        enemyHealthBar.OnHealthBarChange();
        if (health <= 0)
        {
            AudioManager.Instance.PlayAmbianceMusic();
            Destroy(transform.parent.gameObject);
            Destroy(enemyHealthBar.transform.parent.gameObject);
        }
    }
}
