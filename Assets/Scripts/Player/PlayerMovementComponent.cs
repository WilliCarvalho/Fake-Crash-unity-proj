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
    private bool canMove;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = PlayerManager.instance.GetCharacterController();
        playerVelocity = PlayerManager.instance.GetPlayerVelocity();
    }

    private void Update()
    {
        if (canMove)
        {
            MoveHandler(inputData);
        }
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        inputData = context.ReadValue<Vector2>();
        if(inputData.y != 0 || inputData.x != 0)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    private void MoveHandler(Vector2 inputData)
    {
        currentMovement.x = inputData.x;
        currentMovement.y = 0;
        currentMovement.z = inputData.y;

        bool isMoving = inputData.x != 0 || inputData.y != 0;
        PlayerManager.instance.SetIsMoving(isMoving);

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

        if (PlayerManager.instance.GetIsMoving())
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
}
