using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControls playerControl;
    private Rigidbody rigidbody;
    private Vector3 currentMovement;
    private bool isJumping;
    private bool canJump;

    [SerializeField] private float jumpForce = 100;
    [SerializeField] private float velocity = 10;

    private void Awake()
    {
        playerControl = new PlayerControls();
        rigidbody = GetComponent<Rigidbody>();

        playerControl.PlayerActions.Move.started += OnMoveInput;
        playerControl.PlayerActions.Move.performed += OnMoveInput;
        playerControl.PlayerActions.Move.canceled += OnMoveInput;

        playerControl.PlayerActions.Jump.started += OnJumpInput;
        playerControl.PlayerActions.Jump.canceled += OnJumpInput;

        canJump = true;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 inputData = context.ReadValue<Vector2>();
        currentMovement.x = inputData.x;
        currentMovement.z = inputData.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
        Jump();
    }

    private void Jump()
    {
        if (canJump)
        {
            rigidbody.AddForce(Vector3.up * jumpForce);
            canJump = false;
        }
    }

    private void MovePlayer()
    {
        Vector3 cameraRelativeMovement = ConverToCameraSpace(currentMovement);
        transform.Translate(cameraRelativeMovement * velocity * Time.deltaTime);        
    }

    private Vector3 ConverToCameraSpace(Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0; 
        cameraRight.y = 0;

        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 cameraForwardZproduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        Vector3 vectorRotatedToCameraSpace = cameraForwardZproduct + cameraRightXProduct;
        vectorRotatedToCameraSpace.y = currentYValue;
        return vectorRotatedToCameraSpace;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            canJump = true;
        }
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
