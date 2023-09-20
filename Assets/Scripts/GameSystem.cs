using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private InputManager inputManager;

    public static Action<InputAction.CallbackContext> OnMoveInputContextReceived;
    public static Action<bool> OnJumpInputContextReceived;
    public static Action<bool> OnAttackInputContextReceived;

    private void Awake()
    {
        inputManager.OnMove += OnMoveInputReceived;
        inputManager.OnJump += OnJumpInputReceived;
        inputManager.OnAttack += OnAttackInputReceived;
    }

    private void OnAttackInputReceived(bool isAttackPressed)
    {
        OnAttackInputContextReceived?.Invoke(isAttackPressed);
    }

    private void OnJumpInputReceived(bool isJumpPressed)
    {
        OnJumpInputContextReceived?.Invoke(isJumpPressed);
    }

    private void OnMoveInputReceived(InputAction.CallbackContext context)
    {
        OnMoveInputContextReceived?.Invoke(context);
    }
    private void OnDisable()
    {
        inputManager.OnMove -= OnMoveInputReceived;
    }
}
