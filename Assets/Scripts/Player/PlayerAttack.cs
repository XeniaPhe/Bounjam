using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 5f;

    Transform currentSkillVFXTransform;

    public GameObject slash1;
    public GameObject slash2;
    public GameObject slash3;

    public void SkillAttack1()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if (!hitCollider) return;
        currentSkillVFXTransform = Instantiate(slash1.transform, hitCollider.gameObject.transform.position, slash1.transform.rotation);
        currentSkillVFXTransform.gameObject.SetActive(true);
    }

    public void SkillAttack2()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if (!hitCollider) return;
        currentSkillVFXTransform = Instantiate(slash1.transform, hitCollider.gameObject.transform.position, slash1.transform.rotation);
        currentSkillVFXTransform.gameObject.SetActive(true);
    }
}
