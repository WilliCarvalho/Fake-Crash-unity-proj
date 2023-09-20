using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static event Action<InputAction.CallbackContext, float> HandleMoveInput;
    public static event Action<bool, int> HandleJumpInput;
    public static event Action<bool> HandleAttackInput; 

    //Delegate para pegar e passar a refer�ncia do character controller
    //(est� sendo assinada dentro do PlayerMovementComponent)
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
        GameSystem.OnMoveInputContextReceived += HandleMove;
        GameSystem.OnJumpInputContextReceived += HandleJump;
        GameSystem.OnAttackInputContextReceived += HandleAttack;
    }

    private void HandleJump(bool isJumpPressed)
    {
        CharacterController tempController = _characterControllerReference?.Invoke();
        if (tempController.isGrounded == true) numberOfJumps = 0;
        if (isJumpPressed) numberOfJumps++;
        HandleJumpInput?.Invoke(isJumpPressed, numberOfJumps);
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        HandleMoveInput?.Invoke(context, velocity);
    }
    
    private void HandleAttack(bool isAttacking)
    {
        HandleAttackInput?.Invoke(isAttacking);
    }

    private void OnDisable()
    {
        GameSystem.OnMoveInputContextReceived -= HandleMove;
    }
}
