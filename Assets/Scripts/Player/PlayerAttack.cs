using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 5f;

    public float cooldownEndtime = 1f;

    Transform currentSkillVFXTransform;

    public bool skill1Cooldown, skill2Cooldown;

    public GameObject slash1;
    public GameObject slash2;

    public void SkillAttack1()
    {
        if (skill1Cooldown) return;
        skill1Cooldown = true;
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if (!hitCollider) return;
        GameObject enemyGameObject = hitCollider.gameObject;
        currentSkillVFXTransform = Instantiate(slash1.transform, enemyGameObject.transform.position, slash1.transform.rotation);
        currentSkillVFXTransform.gameObject.SetActive(true);
        enemyGameObject.GetComponent<EnemyAI>().TakeDamage(PlayerStats.Instance.attackDamage);
        StartCoroutine(EndSkill1Cooldown());
    }

    public void SkillAttack2()
    {
        if (skill2Cooldown) return;
        skill2Cooldown = true;
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if (!hitCollider) return;
        currentSkillVFXTransform = Instantiate(slash1.transform, hitCollider.gameObject.transform.position, slash1.transform.rotation);
        currentSkillVFXTransform.gameObject.SetActive(true);
        hitCollider.gameObject.GetComponent<EnemyAI>().TakeDamage(PlayerStats.Instance.attackDamage);
        StartCoroutine(EndSkill2Cooldown());
    }

    public IEnumerator EndSkill1Cooldown()
    {
        yield return new WaitForSeconds(cooldownEndtime);
        skill1Cooldown = false;
    }

    public IEnumerator EndSkill2Cooldown()
    {
        yield return new WaitForSeconds(cooldownEndtime);
        skill2Cooldown = false;
    }
}
