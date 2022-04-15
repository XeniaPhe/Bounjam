using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] float walkVelocity;
    [SerializeField] float climbVelocity;
    [SerializeField] float jumpVelocity;
    [SerializeField] Animator animator;
    bool isJumping;
    bool isWalking;
    bool isClimbing;
    bool isGrounded;
    private void Update()
    {
        CheckGrounded();
        TakeInputs();
        UpdateAnimations();
    }

    public void CheckGrounded()
    {
        isGrounded = true;
    }

    public void TakeInputs()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rigidbody.velocity.Set(0, jumpVelocity);
        }
        else
        {
            isJumping = false;
            rigidbody.velocity.Set(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //climb ladder up
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            //climb ladder down
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //go left
            transform.localScale.Set(transform.localScale.x, -1, transform.localScale.z);
            rigidbody.velocity = Vector2.left*walkVelocity;
            if (isGrounded)
                isWalking = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //go right;
            transform.localScale.Set(transform.localScale.x,1, transform.localScale.z);
            rigidbody.velocity = Vector2.right*walkVelocity;
            if (isGrounded)
                isWalking = true;
        }
    }

    public void UpdateAnimations()
    {
        animator.SetBool("Jumping", isJumping);
        animator.SetBool("Walking", isWalking);
        animator.SetBool("Climbing", isClimbing);
    }
}