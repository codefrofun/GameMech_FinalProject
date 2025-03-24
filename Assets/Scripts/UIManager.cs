using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Referenced from a previous assignemnt.
public class UIManager : MonoBehaviour
{
    public GameManagerScript gameManager;

    public GameObject mainMenuUI;
    public GameObject gameplayUI;

    public void EnableMainMenu()
    {
        DisableAll();
        mainMenuUI.SetActive(true);
    }

    public void EnableGameplay()
    {
        DisableAll();
        gameplayUI.SetActive(true);
    }

    public void DisableAll()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(false);
    }
}