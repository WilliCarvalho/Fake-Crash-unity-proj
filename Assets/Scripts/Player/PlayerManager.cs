using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static event Action<InputAction.CallbackContext, float> HandleMoveInput;
    public static event Action<bool, int> HandleJumpInput;

    //Delegate para pegar e passar a referência do character controller
    //(está sendo assinada dentro do PlayerMovementComponent)
    public delegate CharacterController CharacterControllerReference();
    public static CharacterControllerReference _characterControllerReference;

    private int numberOfJumps = 0;

    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float velocity = 10;
    [SerializeField] private int lives = 1;

    private void Awake()
    {
        PlayerManagerSetUpListenerns();
    }

    private void PlayerManagerSetUpListenerns()
    {
        GameSystem.OnMoveInputContextReceived += MovePlayer;
        GameSystem.OnJumpInputContextReceived += JumpPlayer;
    }

    private void Update()
    {
        print(numberOfJumps);
    }

    private void JumpPlayer(bool isJumpPressed)
    {
        CharacterController tempController = _characterControllerReference?.Invoke();
        if (tempController.isGrounded == true) numberOfJumps = 0;
        if (isJumpPressed) numberOfJumps++;
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
