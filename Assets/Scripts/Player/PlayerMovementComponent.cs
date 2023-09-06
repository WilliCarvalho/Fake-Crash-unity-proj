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
    private Vector3 cameraRelativeMovement;
    private Vector2 inputData;
    private float playerVelocity;
    private bool isMoving;
    private CharacterController characterController;

    private void Awake()
    {
        PlayerManager.HandleMoveInput += SetMoveInfo;
        characterController = GetComponent<CharacterController>();
    }

    private void SetMoveInfo(InputAction.CallbackContext context, float velocity)
    {
        playerVelocity = velocity;
        inputData = context.ReadValue<Vector2>();
        isMoving = inputData.x != 0 || inputData.y != 0;
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

        cameraRelativeMovement = ConvertToCameraSpace(currentMovement);
        characterController.Move(cameraRelativeMovement * playerVelocity * Time.deltaTime);
        RotationHandler();
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

    private void Jump()
    {
        
    }

    private void OnDisable()
    {
        PlayerManager.HandleMoveInput -= SetMoveInfo;
    }
}
