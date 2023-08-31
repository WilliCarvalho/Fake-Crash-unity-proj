using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovementComponent : MonoBehaviour
{
    private Vector3 currentMovement;
    private Vector2 inputData;
    private float playerVelocity;
    private bool isMoving;
    private CharacterController characterController;

    private void Awake()
    {
        inputData = new Vector2(0, 0);
        PlayerManager.HandleMoveInput += SetMoveInfo;
        characterController = GetComponent<CharacterController>();
    }

    private void SetMoveInfo(InputAction.CallbackContext context, float velocity)
    {
        playerVelocity = velocity;
        inputData = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        MoveHandler(inputData);
    }

    private void MoveHandler(Vector2 inputData)
    {
        currentMovement.x = inputData.x;
        currentMovement.y = 0;
        currentMovement.z = inputData.y;

        isMoving = inputData.x != 0 || inputData.y != 0;
        //currentVelocity = characterController.velocity.magnitude;
        characterController.Move(currentMovement * playerVelocity * Time.deltaTime);
        RotationHandler();
    }


    private void RotationHandler()
    {
        float rotationFactorPerFrame = 10;
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            //currentMovement.y += Mathf.Sqrt(jumpHeight * gravityScale * -1);
        }
    }

    private void OnDisable()
    {
        PlayerManager.HandleMoveInput -= SetMoveInfo;
    }
}
