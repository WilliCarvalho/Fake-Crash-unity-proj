using System;
using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    private Animator animator;
    private int isJumpingHash;
    private int velocityHash;
    public bool isJumping;
    private CharacterController characterController;
    float currentVelocity;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        PlayerManager.HandleJumpInput += HandleJumpTrigger;

        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("isJumping");
        velocityHash = Animator.StringToHash("velocity");
    }

    private void HandleJumpTrigger(bool jumpInputPressed)
    {
        isJumping = jumpInputPressed;
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

        if (isJumping && !isJumpingAnimation && characterController.isGrounded)
        {
            animator.SetBool(isJumpingHash, true);
        }
        else if (!isJumping && isJumpingAnimation)
        {
            animator.SetBool(isJumpingHash, false);
        }
    }

    private void GetAnimatorParameters()
    {
        isJumpingHash = Animator.StringToHash("isJumping");
        velocityHash = Animator.StringToHash("velocity");
    }
}
