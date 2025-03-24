using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;

    [SerializeField] public float boxPlayer = 1.6f;
    [SerializeField] public float playerSpeed = 1.5f;
    [SerializeField] public float jumpForce = 5f;
    [SerializeField] private bool isGrounded;
    private bool isMoving = false;
    public float moveDirection = 1f;

    // Add audio logic

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D not found on player object!");
        }
        //playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

    }

    private void Update()
    {
        if (!isMoving)
        {
            MovePlayerOnX();
        }
        else
        {
            playerRigidbody.velocity = new Vector2(playerSpeed, playerRigidbody.velocity.y);
        }
            
        HandleJump();
        HandleRestart();
    }


    // X for moving forward + jump
    void MovePlayerOnX()
    {
        if(Input.GetKeyUp(KeyCode.X)) // Use get key not getkeydown for a continuation of movement
        {
            Debug.Log("Player has started moving!");
            isMoving = true;
            playerRigidbody.velocity = new Vector2(playerSpeed, playerRigidbody.velocity.y);
        }
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
        Debug.Log("I hit a wall");
        
        if (playerSpeed == -playerSpeed)
        {
            playerSpeed = +playerSpeed;
        }
        else
        {
            playerSpeed = -playerSpeed;
        }
        playerRigidbody.velocity = new Vector2(playerSpeed, playerRigidbody.velocity.y);
        //playerRigidbody.velocity = new Vector2(moveDirection * playerSpeed, playerRigidbody.velocity.y);
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
            moveDirection = -moveDirection; // switches direction
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
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    // Logic for level reset - CREATE SCENE MANAGER SCRIPT
    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reload current scene CHANGE NAME ONCE APPLICABLE
    }
}
