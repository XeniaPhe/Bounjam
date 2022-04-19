using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootTrack;

public class Player : MonoBehaviour
{
    public static Player Instance;

    new Rigidbody2D rigidbody;
    Animator animator;

    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float exertForceAfter = 0.2f;
    [SerializeField] bool airControl = true;
    [SerializeField] Transform head;
    [SerializeField] float checkRadius = 0.05f;
    [SerializeField] Transform feet;
    [SerializeField] PlayerAttack playerAttack;

    float horizontal, vertical;

    [SerializeField] LayerMask groundLayer;

    bool isGrounded, isBlocked, jumpRequest, isJumping, isRunning, isAttacking, isLanding;

    private void Awake() => Instance = this;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
       // if (StartCinematic.isCinematicActive || EndCinematic.isCinematicActive) return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        CheckPosition();
        CheckAttack();
        CheckJumping();
        CheckInteraction();
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

    private void CheckJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded && !isBlocked)
            jumpRequest = true;

        if (isJumping && !isLanding && rigidbody.velocity.y < 0)
        {
            isJumping = false;
            isLanding = true;
        }
    }

    private void CheckInteraction()
    {
        if(!Input.GetKeyDown(KeyCode.E)) return;

        LayerMask rewardMask = LayerMask.GetMask("Reward");
        LayerMask statueMask = LayerMask.GetMask("Statue");
        LayerMask instrumentMask = LayerMask.GetMask("Instrument");
        Collider2D coll;

        if ((coll = Physics2D.OverlapCircle(transform.position, 2f, rewardMask)))
        {
            Reward reward;
            if(coll.gameObject.TryGetComponent<Reward>(out reward))
                reward.GetReward();
        }
        if((coll = Physics2D.OverlapCircle(transform.position,2f,instrumentMask)))
        {
            ItemWrapper instrument;
            if (coll.gameObject.TryGetComponent<ItemWrapper>(out instrument))
            {
                ItemTracker.Instance.UpdateItem(instrument.Item);
                Destroy(instrument.gameObject);
            }
        }
        if ((coll = Physics2D.OverlapCircle(transform.position, 2f, statueMask)))
        {
            Statue statue;
            if (coll.gameObject.TryGetComponent<Statue>(out statue))
                statue.OpenUpgradePanel();
        }
    }

    private void FixedUpdate()
    {
        if (!(StartCinematic.isCinematicActive || EndCinematic.isCinematicActive))
        {
            Move();
            UpdateAnimations();
        }
        else rigidbody.velocity = new Vector2(0f, 0f);
    }

    public void CheckPosition()
    {
        if (Physics2D.OverlapCircle(head.position, checkRadius, groundLayer.value))
            isBlocked = true;
        else
            isBlocked = false;

        if (Physics2D.OverlapCircle(feet.position, checkRadius, groundLayer.value))
        {
            isGrounded = true;
            isJumping = false;
            isLanding = false;
        }
        else
            isGrounded = false;
    }

    void ExertForce() => rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);

    public void Move()
    {
        if (jumpRequest && !isAttacking)
        {
            isJumping = true;
            isRunning = false;
            isLanding = false;
            Invoke("ExertForce", exertForceAfter);
        }
        jumpRequest = false;

        if ((airControl || isGrounded) && !isAttacking)
        {
            if (horizontal > 0)
            {
                rigidbody.velocity = new Vector2(runSpeed, rigidbody.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
                if (!isJumping && !isLanding)
                    isRunning = true;
            }
            else if (horizontal < 0)
            {
                rigidbody.velocity = new Vector2(-runSpeed, rigidbody.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
                if (!isJumping && !isLanding)
                    isRunning = true;
            }
            else if (horizontal == 0)
            {
                rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
                isRunning = false;
            }
        }
        else
        {
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
            isRunning = false;
        }
    }

    private void UpdateAnimations()
    {
        if (!animator) return;

        if (isAttacking)
            animator.SetBool("Attacking", true);
        else
            animator.SetBool("Attacking", false);

        if (isJumping)
            animator.SetBool("Jumping", true);
        else if (isLanding)
        {
            animator.SetBool("Landing", true);
            animator.SetBool("Jumping", false);
        }
        else
        {
            animator.SetBool("Landing", false);
            animator.SetBool("Jumping", false);
        }

        if (isRunning)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);
    }
}