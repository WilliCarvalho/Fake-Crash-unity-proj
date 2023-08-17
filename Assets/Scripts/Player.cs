using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const float gravityScale = -9.81f;

    private PlayerControls playerControl;
    private CharacterController characterController;
    private Animator animator;
    private Transform playerTransform;
    private Vector3 currentMovement;
    private Vector3 cameraRelativeMovement;
    private bool isJumping;
    private bool isMoving;
    private float currentVelocity;

    private int isJumpingHash;
    private int velocityHash;

    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float velocity = 10;

    private void Awake()
    {
        
        playerControl = new PlayerControls();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        GetAnimatorParameters();

        playerControl.PlayerActions.Move.started += OnMoveInput;
        playerControl.PlayerActions.Move.performed += OnMoveInput;
        playerControl.PlayerActions.Move.canceled += OnMoveInput;

        playerControl.PlayerActions.Jump.started += OnJumpInput;
        playerControl.PlayerActions.Jump.canceled += OnJumpInput;
    }

    void FixedUpdate()
    {
        MoveHandler();
        AnimationHandler();
        RotationHandler();
        GravityHandler();
        print(characterController.isGrounded);
    }

    private void GravityHandler()
    {        
        currentMovement.y += gravityScale * Time.deltaTime;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 inputData = context.ReadValue<Vector2>();
        currentMovement.x = inputData.x;
        currentMovement.y = 0;
        currentMovement.z = inputData.y;
        isMoving = inputData.x != 0 || inputData.y != 0;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
        Jump();
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y += Mathf.Sqrt(jumpHeight * gravityScale * -1);
        }
    }

    private void MoveHandler()
    {
        currentVelocity = characterController.velocity.magnitude;
        cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
        characterController.Move(cameraRelativeMovement * velocity * Time.deltaTime);
    }

    private void AnimationHandler()
    {
        bool isJumpingAnimation = animator.GetBool(isJumpingHash);
        animator.SetFloat(velocityHash, currentVelocity);

        if(isJumping && !isJumpingAnimation)
        {
            animator.SetBool(isJumpingHash, true);
        }
        else if(!isJumping && isJumpingAnimation)
        {
            animator.SetBool(isJumpingHash, false);
        }
    }

    private Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 cameraForwardZproduct = cameraForward * vectorToRotate.z;
        Vector3 cameraRightXProduct = cameraRight * vectorToRotate.x;

        Vector3 directionToMovePlayer = cameraForwardZproduct + cameraRightXProduct;
        directionToMovePlayer.y = currentYValue;
        return directionToMovePlayer;
    }


    private void RotationHandler()
    {
        float rotationFactorPerFrame = 10;
        Vector3 positionToLookAt;
        positionToLookAt.x = cameraRelativeMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = cameraRelativeMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void GetAnimatorParameters()
    {
        isJumpingHash = Animator.StringToHash("isJumping");
        velocityHash = Animator.StringToHash("velocity");
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }
}
