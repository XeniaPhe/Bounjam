using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    new Rigidbody2D rigidbody;
    Animator animator;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] bool airControl = true;
    [SerializeField] Transform head;
    [SerializeField] float checkRadius = 0.05f;
    [SerializeField] Transform feet;
    [SerializeField] PlayerAttack playerAttack;

    float horizontal=0,vertical=0;
    bool start = true;

    [SerializeField] LayerMask groundLayer;

    bool isGrounded, isBlocked, jumpRequest, isJumping, isRunning,isAttacking,isLanding;
    bool isJumpingAnimated,isJumpingFallAnimated;

    private void Awake() => Instance = this;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (start) return;
        CheckPosition();
        CheckAttack();
        CheckJumping();
        CheckInteraction();
        UpdateAnimations();
    }

    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerAttack.SkillAttack1();
            isAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAttack.SkillAttack2();
            isAttacking = true;
        }
    }

    public void ResetAttack() => isAttacking = false;

    private void CheckInteraction()
    {
        bool isReward = false, isStatue = false;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Reward"));
        if (hitCollider) isReward = true;
        if(!isReward) hitCollider = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Statue"));
        if (hitCollider && !isReward) isStatue = true;
        if (!(isStatue || isReward)) return;
        Reward reward = null;
        Statue statue = null;
        if(isReward) hitCollider.gameObject.TryGetComponent(out reward);
        if(reward != null) reward.GetReward();
        if(isStatue) hitCollider.gameObject.TryGetComponent(out statue);
        if (statue != null) statue.OpenUpgradePanel();
    }

    private void FixedUpdate() => Move();

    public void CheckPosition()
    {
        if (Physics2D.OverlapCircle(head.position, checkRadius, groundLayer.value))
        {
            isBlocked = true;
            isJumping = false;
        }
        else
            isBlocked = false;

        if (Physics2D.OverlapCircle(feet.position, checkRadius, groundLayer.value))
        {
            isGrounded = true;
            isJumping = false;
        }
        else
            isGrounded = false;
    }

    private void CheckJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded && !isBlocked)
            jumpRequest = true;
        if(isJumping && !isLanding && rigidbody.velocity.y < 0)
        {
             isJumping = false;
             isLanding = true;
        }
    }

    public void Move()
    {
        if(jumpRequest && !isAttacking)
        {
            isJumping = true;
            isJumpingAnimated = false;
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
        jumpRequest=false;

        if ((airControl || isGrounded || !isJumping) && !isAttacking)
        {
            if (horizontal > 0)
            {
                rigidbody.velocity = new Vector2(runSpeed, rigidbody.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
                isRunning = true;
            }
            else if (horizontal < 0)
            {
                rigidbody.velocity = new Vector2(-runSpeed, rigidbody.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                isRunning = true;
            }
            else if (horizontal == 0)
            {
                rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
                isRunning = false;
            }
        }
        else
            isRunning = false;
    }

    private void UpdateAnimations()
    {
        if (!animator) return;

        if (isAttacking)
            animator.SetBool("Attacking", true);
        else
            animator.SetBool("Attacking", false);

        if (isJumping && !isJumpingAnimated)
        {
            animator.SetBool("Jumping", true);
            isJumpingAnimated = true;
        }
        else if (isJumping && isJumpingAnimated && !isJumpingFallAnimated && rigidbody.velocity.y < 0)
        {
            animator.SetBool("JumpingFall", true);
            isJumpingFallAnimated = true;
        }
        else if(isJumpingAnimated && isJumpingFallAnimated && !isJumping && isGrounded)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("JumpingFall", false);
        }

        if (!isJumping && isGrounded && isRunning)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);
    }
}