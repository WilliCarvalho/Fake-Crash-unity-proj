using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private PlayerMovementComponent movementComponent;

    private Transform playerTransform;
    private CharacterController characterController;
    private bool isJumping;
    private bool isMoving;

    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float velocity = 10;
    [SerializeField] private int lives = 1;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion       

        movementComponent = GetComponent<PlayerMovementComponent>();
        playerTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        InputManager.onMove += MovePlayer;
    }


    private void MovePlayer(InputAction.CallbackContext context)
    {
        movementComponent.MovePlayer(context);
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void SetIsMoving(bool isMoving)
    {
        this.isMoving = isMoving;
    }

    public float GetPlayerVelocity()
    {
        return velocity;
    }
    public float GetCurrentVelocity()
    {
        print(characterController.velocity.magnitude);
        return characterController.velocity.magnitude;
    }

    public CharacterController GetCharacterController()
    {
        return characterController;
    }

    public void SetCharacterController(CharacterController characterController)
    {
        this.characterController = characterController;
    }

    public bool GetIsJumping()
    {
        return isJumping;
    }

    private void OnDisable()
    {
        InputManager.onMove -= MovePlayer;
    }

}
