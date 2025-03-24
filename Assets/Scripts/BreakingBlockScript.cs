using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBlockScript : MonoBehaviour
{
    public bool isDestroyed = false;

    //public GameObject breakEffect;      with particle system?

    public AudioClip breakSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BreakBlock();
        }
    }

    void BreakBlock()
    {
        if (isDestroyed) return; // Won't break block if already broken

        isDestroyed = true;

        if (audioSource != null && breakSound != null)
        {
            audioSource.PlayOneShot(breakSound); // Play the sound once
        }

        Destroy(gameObject);

        Debug.Log("Block destroyed!");
    }
}