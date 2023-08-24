using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControl;

    public delegate void OnMove(InputAction.CallbackContext context);
    public static event OnMove onMove;

    private void Awake()
    {
        playerControl = new PlayerControls();

        playerControl.PlayerActions.Move.started += OnMoveInput;
        playerControl.PlayerActions.Move.performed += OnMoveInput;
        playerControl.PlayerActions.Move.canceled += OnMoveInput;

        playerControl.PlayerActions.Jump.started += OnJumpInput;
        playerControl.PlayerActions.Jump.canceled += OnJumpInput;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        onMove.Invoke(context);
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
