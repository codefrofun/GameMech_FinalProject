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
        if (other.gameObject.CompareTag("Player"))
        {

            gameManager.sceneManager.LoadSceneToSpawnPosition(sceneName);
        }
    }
}
