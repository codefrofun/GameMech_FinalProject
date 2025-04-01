using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;

    [SerializeField] public float playerSpeed = 2f;  // Adjusted speed
    [SerializeField] public float jumpForce = 2.2f;
    [SerializeField] private bool isGrounded;
    private bool isMoving = false;
    public float moveDirection = 1f;

    // For damage + health system
    [SerializeField] public int maxLives = 3;
    public static int currentLives;

    public TextMeshPro playerLivesText;
    public GameObject gameOverPanel;         // Reference to Game Over Panel
    public Button retryButton;               // Reference to Retry Button

    // Audio for jump

    private AudioSource jumpSound;

    private void Start()
    {
        jumpSound = GetComponent<AudioSource>(); // Get AudioSource attached to this object

        currentLives = maxLives; // Setting lives to 3 at start

        playerRigidbody = GetComponent<Rigidbody2D>(); // Get player rigidbody

        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D not found on player object!");
        }

        // GameOverPanel will be inactive at the start
        gameOverPanel.SetActive(false);

        UpdateLivesText();

        //The Retry Button click event
        retryButton.onClick.AddListener(RestartLevel);
    }


    private void Update()
    {
        if (currentLives <= 0)
        {
            HandleDeath();
            return;
        }

        // Handle player horizontal movement
        HandleHorizontalMovement();

        // Handle jumping logic
        HandleJump();

        // Handle level restart
        HandleRestart();
    }

    void HandleHorizontalMovement()
    {
        if (!gameOverPanel.activeSelf)  // Only move if the game over panel is not active
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
    }


    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if(jumpSound != null)
            {
                jumpSound.Play();
            }

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

        if (collision.gameObject.CompareTag("Bomb"))
        {
            StartCoroutine(HandleBombImpact());
        }
    }

    private IEnumerator HandleBombImpact()
    {
        yield return new WaitForSeconds(3f);

        currentLives--;
        UpdateLivesText();
        if (currentLives <= 0)
        {
            HandleDeath();
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
    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reload current scene
    }

    public void UpdateLivesText()
    {
        // Update the TextMeshPro component with the current lives
        if (playerLivesText != null)
        {
            playerLivesText.text = "Lives: " + currentLives.ToString();
        }
    }

    public void HandleDeath()
    {
        // Show Game Over panel
        gameOverPanel.SetActive(true);

        playerRigidbody.velocity = Vector2.zero; // Stop player movement

        Debug.Log("Game over!");
    }
}