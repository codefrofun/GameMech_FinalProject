using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;

public class LavaScript : MonoBehaviour
{
    public float delayTime = 3f;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            RestartLevel();
            ReturnAfterDelay();
        }
    }

    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); 
    }

    private IEnumerator ReturnAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
    }
}
