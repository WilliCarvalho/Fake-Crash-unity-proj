using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSystem : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private InputManager inputManager;

    [Header("Triggers")]
    [SerializeField] private BossFightTrigger BossTrigger;

    public static Action<InputAction.CallbackContext> OnMoveInputContextReceived;
    public static Action<bool> OnJumpInputContextReceived;
    public static Action<bool> OnAttackInputContextReceived;
    public static Action OnBossTriggered;

    private void Awake()
    {
        inputManager.OnMove += OnMoveInputReceived;
        inputManager.OnJump += OnJumpInputReceived;
        inputManager.OnAttack += OnAttackInputReceived;

        BossTrigger.OnEnterBossArea += HandleStartBossFight;
    }

    private void HandleStartBossFight()
    {
        OnBossTriggered?.Invoke();
    }

    private void OnJumpInputReceived(bool isJumpPressed)
    {
        OnJumpInputContextReceived?.Invoke(isJumpPressed);
    }

    private void OnMoveInputReceived(InputAction.CallbackContext context)
    {
        OnMoveInputContextReceived?.Invoke(context);
    }

    private void OnAttackInputReceived(bool isAttacking)
    {
        OnAttackInputContextReceived?.Invoke(isAttacking);
    }

    private void OnDisable()
    {
        inputManager.OnMove -= OnMoveInputReceived;
    }
}
