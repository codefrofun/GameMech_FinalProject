using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; set; }
    public UIManager uiManager;

    // Create instance of player

    public SceneManagerScript sceneManager;
    /*
    private void Awake()
    {
        // Ensures only one instance of PlayerManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make the PlayerManager persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy the duplicate PlayerManager
        }
    }
    */
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        
    }
}
