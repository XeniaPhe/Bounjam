using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Animator animator;
    public EnemyHealthBar enemyHealthBar;

    public float sightRange = 5f;
    public float attackRange = 2f;

    public float attackCooldown = 1f;

    public float health = 3f;
    public float maxHealth = 3f;
    public float moveSpeed = 3f;

    bool isCooldown, isRunning, isAttacking;

    public bool CheckPlayerInMoveRange() => Physics2D.OverlapCircle(transform.position, sightRange, LayerMask.GetMask("Player")) ? true : false;

    public bool CheckPlayerInAttackRange() => Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player")) ? true : false;

    public void MoveTowardsPlayer() => transform.position = Vector2.MoveTowards(transform.position, Player.Instance.transform.position, moveSpeed * Time.deltaTime);

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        enemyHealthBar.OnHealthBarChange();
        if (health <= 0)
        {
            Destroy(transform.parent.gameObject);
            Destroy(enemyHealthBar.transform.parent.gameObject);
        }
    }

    public virtual void Update()
    {
        if (!CheckPlayerInMoveRange())
        {
            isAttacking = false;
            isRunning = false;
            return;
        }
        else if (CheckPlayerInAttackRange())
        {
            if (isCooldown) return;
            isCooldown = true;
            isAttacking = true;
            PlayerStats.Instance.TakeDamage(10);
            StartCoroutine(EndCooldown());
        }
        else MoveTowardsPlayer();

        if(!isAttacking)
            isRunning = true;

        UpdateAnimations();
    }

    public virtual IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isCooldown = false;
    }

    public void UpdateAnimations()
    {
        return;
        animator.SetBool("Running", isRunning);
        animator.SetBool("Attacking", isAttacking);
    }
}