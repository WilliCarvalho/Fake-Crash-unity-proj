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
        transform.Translate(currentMovement * velocity * Time.deltaTime);

        
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
