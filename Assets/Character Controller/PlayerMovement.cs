using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public float moveSpeed = 2.0f;
    public Rigidbody2D playerRigidbody; // Reference to the CharacterController component for movement control
    public Vector2 moveVector;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the CharacterController component attached to the GameObject
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        // Subscribe to the MovePlayerEvent to update movement direction
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

            InputActions.MovePlayerEvent += UpdateMoveVector;
    }

    // Method to update the movement direction based on the input from InputManager
    private void UpdateMoveVector(Vector2 InputVector)
    {
        moveVector = InputVector;
    }

    void HandlePlayerMovement(Vector2 moveVector)
    {
        // Move the character based on the input direction and moveSpeed
        playerRigidbody.MovePosition(playerRigidbody.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }
    void FixedUpdate()
    {
        // Handle the player movement using the current direction
        HandlePlayerMovement(moveVector);
    }
    void OnDisable()
    {
        // Unsubscribe from the MovePlayerEvent
        InputActions.MovePlayerEvent -= UpdateMoveVector;
    }
}