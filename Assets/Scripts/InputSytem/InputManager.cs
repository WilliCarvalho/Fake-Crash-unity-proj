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

        playerControl.PlayerActions.Attack.started += OnAttackInput;
        playerControl.PlayerActions.Attack.canceled += OnAttackInput;
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke(context.ReadValueAsButton());
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //? = Somente ser� invocado caso exista um listener no evento
        OnMove?.Invoke(context);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(context.ReadValueAsButton());
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
