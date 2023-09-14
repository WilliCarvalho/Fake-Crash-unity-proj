using System;
using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    private Animator animator;

#region Varaibles: AnimationsHash
    private int isJumpingHash;
    private int velocityHash;
    private int numberOfJumpsHash;
    #endregion

    private int numberOfJumps;
    public bool isJumping;

    float currentVelocity = 0;
    private void Awake()
    {
        PlayerManager.HandleJumpInput += HandleJumpTrigger;

        animator = GetComponent<Animator>();
        GetAnimatorParameters();
    }

    private void HandleJumpTrigger(bool jumpInputPressed, int numberOfJumps)
    {
        print(numberOfJumps);
        if (jumpInputPressed) this.numberOfJumps = numberOfJumps;
        isJumping = jumpInputPressed;
    }

    private void Update()
    {
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        bool isJumpingAnimation = animator.GetBool(isJumpingHash);
        CharacterController tempController = PlayerManager._characterControllerReference?.Invoke();
        currentVelocity = tempController.velocity.magnitude;

        animator.SetFloat(velocityHash, currentVelocity);
        animator.SetInteger(numberOfJumpsHash, numberOfJumps);

        if (tempController.isGrounded || animator.GetInteger(numberOfJumpsHash) > 2)
        {
            animator.SetInteger(numberOfJumpsHash, 0);            
        }

        if (isJumping && !isJumpingAnimation && tempController.isGrounded)
        {
            animator.SetBool(isJumpingHash, true);
        }
        else if (isJumpingAnimation || tempController.isGrounded)
        {
            animator.SetBool(isJumpingHash, false);
        }
    }

    private void GetAnimatorParameters()
    {
        isJumpingHash = Animator.StringToHash("isJumping");
        velocityHash = Animator.StringToHash("velocity");
        numberOfJumpsHash = Animator.StringToHash("numberOfJumps");
    }
}
