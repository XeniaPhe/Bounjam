using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float sightRange = 5f;
    public float attackRange = 2f;

    public float attackCooldown = 1f;

    bool isCooldown;

    public AIPath aiPath;

    public bool CheckPlayerInMoveRange() => Physics2D.OverlapCircle(transform.position, sightRange, LayerMask.GetMask("Player")) ? true : false;

    public bool CheckPlayerInAttackRange() => Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player")) ? true : false;

    private void Update()
    {
        if (!CheckPlayerInMoveRange())
        {
            aiPath.enabled = false;
            return;
        }
        if (CheckPlayerInAttackRange() && !isCooldown)
        {
            isCooldown = true;
            PlayerStats.Instance.TakeDamage(10);
            StartCoroutine(EndCooldown());
        }
        aiPath.enabled = true;
        if(aiPath.desiredVelocity.x >= 0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else if(aiPath.desiredVelocity.x <= -0.01f) transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isCooldown = false;
    }
}
