using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

public class InputManager : MonoBehaviour, GameInput.IPlayerActions
{

    public GameInput gameInput;
    // Static event to trigger car movement and stop actions
    public static Action MoveCar;
    public static event Action StopCar;

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
           // Debug.Log("Move has been activated/pressed" + context.ReadValue<Vector2>());
            InputActions.MovePlayerEvent?.Invoke(context.ReadValue<Vector2>());
        }

        // Always invoke MovePlayerEvent with the updated movement vector
        Vector2 moveInput = context.ReadValue<Vector2>();
        InputActions.MovePlayerEvent?.Invoke(moveInput);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            InputActions.MoveCar?.Invoke();   // Trigger interaction when spacebar is pressed
        }
    }

    public void OnCarWorkerSound(InputAction.CallbackContext context)
    {
        Debug.Log("Space was pressed!");
        if (context.started)  // Detect when Space is pressed
        {
            MoveCar?.Invoke(); // Trigger the car movement
        }

        if (context.canceled)  // Detect when Space is released
        {
            StopCar?.Invoke(); // Trigger the car stop
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

    // Events to control car movement (start/stop)
    public static Action MoveCar;
}