using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorComponent : MonoBehaviour
{
    private Animator animator;
    private int isJumpingHash;
    private int velocityHash;
    private CharacterController characterController;
    float currentVelocity;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("isJumping");
        velocityHash = Animator.StringToHash("velocity");
    }

    private void Update()
    {
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        bool isJumpingAnimation = animator.GetBool(isJumpingHash);
        currentVelocity = characterController.velocity.magnitude;
        animator.SetFloat(velocityHash, currentVelocity);

        //if (isJumping && !isJumpingAnimation)
        //{
        //    animator.SetBool(isJumpingHash, true);
        //}
        //else if (!isJumping && isJumpingAnimation)
        //{
        //    animator.SetBool(isJumpingHash, false);
        //}
    }

    private void GetAnimatorParameters()
    {
        isJumpingHash = Animator.StringToHash("isJumping");
        velocityHash = Animator.StringToHash("velocity");
    }
}
