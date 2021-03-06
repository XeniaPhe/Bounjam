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
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if (!hitCollider) return;
        GameObject enemyGameObject = hitCollider.gameObject;
        skill1Cooldown = true;
        currentSkillVFXTransform = Instantiate(slash1.transform, enemyGameObject.transform.position, slash1.transform.rotation);
        currentSkillVFXTransform.gameObject.SetActive(true);
        enemyGameObject.GetComponent<EnemyAI>().TakeDamage(PlayerStats.Instance.attackDamage);
        PlayerAudioManager.Instance.PlayAttackVFX();
        StartCoroutine(EndSkill1Cooldown());
    }

    public void SkillAttack2()
    {
        if (skill2Cooldown) return;
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if (!hitCollider) return;
        currentSkillVFXTransform = Instantiate(slash2.transform, hitCollider.gameObject.transform.GetChild(1).position, slash2.transform.rotation);
        skill2Cooldown = true;
        currentSkillVFXTransform.gameObject.SetActive(true);
        hitCollider.gameObject.GetComponent<EnemyAI>().TakeDamage(PlayerStats.Instance.attackDamage);
        PlayerAudioManager.Instance.PlayAttackVFX();
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