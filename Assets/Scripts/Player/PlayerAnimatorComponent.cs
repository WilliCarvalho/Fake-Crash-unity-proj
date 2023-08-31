using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorComponent : MonoBehaviour
{
    private Animator animator;

    private int isJumpingHash;
    private int velocityHash;

    private float playerCurrentVelocity;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("isJumping");
        velocityHash = Animator.StringToHash("velocity");
    }

    private void Update()
    {
        //playerCurrentVelocity = PlayerManager.instance.GetCurrentVelocity();
        AnimationHandler();
    }

    private void AnimationHandler()
    {
        bool isJumpingAnimation = animator.GetBool(isJumpingHash);
        //bool isJumping = PlayerManager.instance.GetIsJumping();
        animator.SetFloat(velocityHash, playerCurrentVelocity);

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
