using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script referenced from previous assignment, game states manager
public class GameStateManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameManagerScript gameManager;
    public enum GameState
    {
        MainMenu_State, Gameplay_State
    }
    public GameState currentState { get; private set; }

    [SerializeField] private string currentStateDebug;
    [SerializeField] private string lastStateDebug;

    public void ChangeState(GameState newState)
    {
        lastStateDebug = currentState.ToString();

        currentState = newState;

        HandleStateChange(newState);

        currentStateDebug = currentState.ToString();
    }

    public void InitializePlayer()
    {
        // Check if player already exists in the scene
        if (playerPrefab != null && GameObject.Find("Player") == null)
        {
            Instantiate(playerPrefab); // Instantiate player in the new scene
            playerPrefab.name = "Player";
        }
        else
        {
            Debug.Log("Player already exists in this scene (no second instantiation) ");
        }
    }

    private void Update()
    {
        Debug.Log(currentState);
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentState == GameState.MainMenu_State)
            {
                ChangeState(GameState.Gameplay_State);
                SceneManager.LoadScene("Level_1");
            }
        }
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Gameplay_State:
            InitializePlayer();
            Time.timeScale = 1f;
            gameManager.uiManager.EnableGameplay();
            Debug.Log("Switched to Gameplay screen");
            break;
        }
    }

    public void ChangeStateToGameplay()
    {
        ChangeState(GameState.Gameplay_State);
    }
}