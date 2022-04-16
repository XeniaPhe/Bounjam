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

    float horizontal,vertical;

    [SerializeField] LayerMask groundLayer;

    bool isGrounded, isBlocked,isJumping, isRunning,_jump;
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

        CheckPosition();
        CheckJumping();
        CheckInteraction();
        UpdateAnimations();
    }

    private void CheckJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded && !isBlocked && !_jump)
            _jump = true;
    }

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

    public void Move()
    {
        if(_jump)
        {
            _jump = false;
            isJumping = true;
            isJumpingAnimated = false;
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }

        isRunning = true;
        if(airControl || isGrounded || !isJumping)
        {
            if (horizontal > 0)
            {
                rigidbody.velocity = new Vector2(runSpeed, rigidbody.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                rigidbody.velocity = new Vector2(-runSpeed, rigidbody.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontal == 0 && rigidbody.velocity.x == 0)
                isRunning = false;
        }
    }

    private void UpdateAnimations()
    {
        if (!animator) return;

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