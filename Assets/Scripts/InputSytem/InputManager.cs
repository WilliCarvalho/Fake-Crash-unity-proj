using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControl;

    public event Action<InputAction.CallbackContext> OnMove;
    public event Action<bool> OnJump;
    public event Action<bool> OnAttack;

    private void Awake()
    {
        playerControl = new PlayerControls();

        playerControl.PlayerActions.Move.started += OnMoveInput;
        playerControl.PlayerActions.Move.performed += OnMoveInput;
        playerControl.PlayerActions.Move.canceled += OnMoveInput;

        playerControl.PlayerActions.Jump.started += OnJumpInput;
        playerControl.PlayerActions.Jump.canceled += OnJumpInput;

        playerControl.Combat.Attack.started += OnAttackInput;
        playerControl.Combat.Attack.canceled += OnAttackInput;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //? = Somente será invocado caso existe um listener no event
        OnMove?.Invoke(context);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(context.ReadValueAsButton());
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke(context.ReadValueAsButton());
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
