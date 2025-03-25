using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;

    [SerializeField] public float playerSpeed = 2f;  // Adjusted speed
    [SerializeField] public float jumpForce = 2.2f;
    [SerializeField] private bool isGrounded;
    private bool isMoving = false;
    public float moveDirection = 1f;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D not found on player object!");
        }
    }


    private void Update()
    {
        // Handle player horizontal movement
        HandleHorizontalMovement();

        // Handle jumping logic
        HandleJump();

        // Handle level restart
        HandleRestart();
    }

    void HandleHorizontalMovement()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Gets input for left/right movement (A/D or Left/Right arrow)
        if (moveInput != 0)  // If there's movement input
        {
            playerRigidbody.velocity = new Vector2(moveInput * playerSpeed, playerRigidbody.velocity.y); // Apply movement
            isMoving = true; // Player is moving
        }
        else
        {
            isMoving = false; // Player is not moving
        }
    }


    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Player Jumped!");
            playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Apply upward force
            isGrounded = false; // Player is no longer grounded after jumping
        }
    }

    public void PlayerSwitchDirection()
    {
        Debug.Log("I hit a wall");

        // Reverse player movement direction if they hit a wall
        moveDirection = -moveDirection;
        playerRigidbody.velocity = new Vector2(moveDirection * playerSpeed, playerRigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveDirection = -moveDirection; // Switch direction when hitting a wall
            Debug.Log("Player has collided with wall, will now switch directions");
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Player is on the ground, can jump again
            Debug.Log("Player is grounded!");
        }
    }

    void HandleRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    // Restart the level by reloading the current scene
    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reload current scene
    }
}