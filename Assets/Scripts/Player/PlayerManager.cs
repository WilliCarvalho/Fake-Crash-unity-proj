using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static event Action<InputAction.CallbackContext, float> HandleMoveInput;
    public static event Action<bool, int> HandleJumpInput;

    public delegate CharacterController GetCharacterController();
    public static GetCharacterController characterControllerReference;

    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float velocity = 10;
    [SerializeField] private int lives = 1;

    private int numberOfJumps = 0;

    private void Awake()
    {
        PlayerManagerSetUpListenerns();
    }

    private void PlayerManagerSetUpListenerns()
    {
        GameSystem.OnMoveInputContextReceived += MovePlayer;
        GameSystem.OnJumpInputContextReceived += JumpPlayer;
    }

    private void JumpPlayer(bool isJumpPressed)
    {
        if (characterControllerReference?.Invoke().isGrounded == true) numberOfJumps = 0;
        if(isJumpPressed) numberOfJumps++;

        HandleJumpInput?.Invoke(isJumpPressed, numberOfJumps);
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        HandleMoveInput?.Invoke(context, velocity);
    }

    private void OnDisable()
    {
        GameSystem.OnMoveInputContextReceived -= MovePlayer;
    }

}
