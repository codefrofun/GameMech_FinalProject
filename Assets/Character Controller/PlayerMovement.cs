using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] public float jumpForce = 5f;
    [SerializeField] private bool isGrounded;

    public float slowWallBounce = 0.5f;
    public float moveDirection = 1f;
    public float moveSpeed = 2.0f;
    public Rigidbody2D playerRigidbody; // Reference to the CharacterController component for movement control
    public Vector2 moveVector;
    private bool isMoving = false;

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

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isGrounded && isMoving)
            {
                Debug.Log("Player Jumped!");
                playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Applies upward force
                isGrounded = false;
            }
        }
    }

    public void PlayerSwitchDirection()
    {
        Debug.Log("Player hit a wall, switching direction");

        //Slows down player after wall collision
        moveSpeed *= slowWallBounce;

        // Reverse the movement direction by changing the sign of moveSpeed
        moveSpeed = -moveSpeed;

        // Update the player's velocity based on the new moveSpeed
        playerRigidbody.velocity = new Vector2(moveSpeed, playerRigidbody.velocity.y);
    }


    /// <summary>
    /// Collision with walls so that player automatically turns, 
    /// does not go through walls, follows path. 
    /// (EVENTUALLY CODE THE APPEARING WALLS AND STONE WALLS THAT BREAK)
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayerSwitchDirection();
            Debug.Log("Player has collided with wall, will now switch directions");
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Checks if player is on the ground, giving the ability to jump without jumping on air
            Debug.Log("Player is grounded!");
        }
    }

    // R to restart level after player death
    void HandleRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    // Logic for level reset
    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reload current scene
    }
}