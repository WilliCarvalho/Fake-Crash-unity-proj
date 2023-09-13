using System;
using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    private Animator animator;

    private int isJumpingHash;
    private int velocityHash;
    private int isDoubleJumpingHash;

    public bool isJumping;
    private void Awake()
    {
        PlayerManager.HandleJumpInput += HandleJumpTrigger;

        animator = GetComponent<Animator>();
        GetAnimatorParameters();
    }

    private void HandleJumpTrigger(bool jumpInputPressed, int numberOfJumps)
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
        CharacterController tempReference = PlayerManager.characterControllerReference?.Invoke();
        float currentVelocity = tempReference.velocity.magnitude;
        animator.SetFloat(velocityHash, currentVelocity);

        if (isJumping && !isJumpingAnimation && tempReference.isGrounded)
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
        isDoubleJumpingHash = Animator.StringToHash("isDoubleJumping");
    }
}
