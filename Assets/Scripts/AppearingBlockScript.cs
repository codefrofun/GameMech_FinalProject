using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingBlockScript : MonoBehaviour
{
    public GameObject block;
    public GameObject newBlock;
    public float delayTime = 1f;
    private bool isDestroyed = false;

    private void Start()
    {
        block.SetActive(true);
        newBlock.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is found");
            StartCoroutine(ShowBlockAfterDelay());
        }
    }

    void BreakBlock()
    {
        isDestroyed = true;

        Destroy(gameObject);

        Debug.Log("Block destroyed!");

        if (newBlock != null)
        {
            newBlock.SetActive(true);
        }
    }

    private IEnumerator ShowBlockAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        block.SetActive(false);
        BreakBlock();
    }
}
