using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaScript : MonoBehaviour
{
    public float delayTime = 3f;

    public PlayerMovement playerMoveScript;

    public void Start()
    {
        playerMoveScript = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(HandleLava()); // Start damage sequence
        }
    }

    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); 
    }


    private IEnumerator HandleLava()
    {
        yield return new WaitForSeconds(0.5f);

        // Reduce player lives
        PlayerMovement.currentLives--;
        playerMoveScript.UpdateLivesText();

        RestartLevel();

        if (PlayerMovement.currentLives <= 0)
        {
            playerMoveScript.HandleDeath();
        }
    }
}
