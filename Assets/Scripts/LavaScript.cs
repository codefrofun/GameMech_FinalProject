using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaScript : MonoBehaviour
{
    public float delayTime = 3f;
    public Vector3 respawnPoint = new Vector3(-1.7f, -0.6f, 0);

    public PlayerMovement playerMoveScript;

    public void Start()
    {
        playerMoveScript = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the lava");
            StartCoroutine(HandleLava()); // Start damage sequence
        }
    }


    private IEnumerator HandleLava()
    {
        yield return new WaitForSeconds(0.5f);

        // Reduce player lives
        PlayerMovement.currentLives--;
        playerMoveScript.UpdateLivesText();
        Debug.Log("Player lives has been updated");

        RespawnPlayer();

        if (PlayerMovement.currentLives <= 0)
        {
            playerMoveScript.HandleDeath();
        } 
    }


    public void RespawnPlayer()
    {
        if (playerMoveScript != null)
        {
            Debug.Log("Player respawned at: " + respawnPoint);

            // Move the PLAYER to respawn point
            playerMoveScript.transform.position = respawnPoint;

            // Reset player's velocity to prevent unintended movement
            if (playerMoveScript.playerRigidbody != null)
            {
                playerMoveScript.playerRigidbody.velocity = Vector2.zero;
                playerMoveScript.playerRigidbody.angularVelocity = 0f;
            }
        }
        else
        {
            Debug.LogError("PlayerMovement script not found!");
        }
    }
}
