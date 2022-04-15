using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;
    [SerializeField] float walkVelocity;

    [SerializeField] Animator animator;

    float horizontal;
    float vertical;
    bool jump;
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetKeyDown(KeyCode.Space);
    }
    private void FixedUpdate()
    {
        characterController.Move(horizontal*walkVelocity, false, jump);
    }
}