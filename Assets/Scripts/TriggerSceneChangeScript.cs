using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSceneChangeScript : MonoBehaviour
{
    // script borrowed from previous assignment.
    [SerializeField] private string sceneName;
    [SerializeField] private GameManagerScript gameManager;

    public float delayTime = 1f;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && KeyPickupScript.hasKey)
        {

            gameManager.sceneManager.LoadSceneToSpawnPosition(sceneName);
        }
        else
        {
            Debug.Log("You need a key to proceed!"); // Feedback for player
        }
    }
}
