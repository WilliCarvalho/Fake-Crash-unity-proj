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

    private void Awake()
    {
        playerControl = new PlayerControls();

        playerControl.PlayerActions.Move.started += OnMoveInput;
        playerControl.PlayerActions.Move.performed += OnMoveInput;
        playerControl.PlayerActions.Move.canceled += OnMoveInput;

        playerControl.PlayerActions.Jump.started += OnJumpInput;
        playerControl.PlayerActions.Jump.performed += OnJumpInput;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //? = Somente será invocado caso existe um listener no event
        OnMove?.Invoke(context);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        //isJumping = context.ReadValueAsButton();
        //Jump();
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
