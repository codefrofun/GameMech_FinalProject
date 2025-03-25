using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

public class InputManager : MonoBehaviour, GameInput.IPlayerActions
{

    public GameInput gameInput;

    void Awake()
    {
        // Initialize the gameInput and enable the input actions
        gameInput = new GameInput();
        gameInput.Player.Enable();
        // Set up the input callback methods for player actions
        gameInput.Player.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            // Debugging the move input
            // Debug.Log("Move input detected: " + context.ReadValue<Vector2>());
            Vector2 moveInput = context.ReadValue<Vector2>();
            InputActions.MovePlayerEvent?.Invoke(moveInput);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            if (Keyboard.current.wKey.isPressed) // Check if "W" is pressed
            {
                // Trigger the Jump event
                Debug.Log("W key detected for jump!");
                InputActions.Jump?.Invoke(); // Trigger the jump action without arguments
            }
        }
    }

    // Disable the input actions when the script is disabled
    void OnDisable()
    {
        gameInput.Player.Disable();
    }
}

public static class InputActions
{
    // Event to move the player based on input
    public static Action <Vector2> MovePlayerEvent;

    public static Action Jump;
}