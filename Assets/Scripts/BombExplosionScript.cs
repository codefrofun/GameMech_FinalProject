using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionScript : MonoBehaviour
{
    public bool bombExploded = false;

    public AudioClip tickSound; // for while countdown is happening, have bomb make a ticking noise
    public AudioClip explodeSound; // for when the bomb explodes
    private AudioSource audioSource;

    public float delayTime = 2f;
    public float delay = 2f;
    public ParticleSystem explosionParticles;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (explosionParticles != null)
        {
            //explosionParticles.gameObject.SetActive(false); // Deactivate the particle system initially
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (bombExploded) return;

            if (audioSource != null && tickSound != null)
            {
                Debug.Log("Coroutine has started! Audio should play");   // Not colliding ???

                audioSource.PlayOneShot(tickSound); // Play the sound once
                StartCoroutine(WaitForBomb());
                //Logic to kill the player
            }
        }
    }

    private IEnumerator WaitForBomb()
    {
        yield return new WaitForSeconds(delayTime);
        BombExplode();
    }


    void BombExplode()
    {
        if (bombExploded) return; // Won't explode bomb if already broken

        bombExploded = true;

        // Stop any ticking sound that may still be playing
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop any sound currently playing
        }

        if (audioSource != null && explodeSound != null)
        {
            audioSource.PlayOneShot(explodeSound); // Play the sound once
        }

        if (explosionParticles != null)
        {
            ParticleSystem explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            if (explosion != null)
            {
                explosion.Play(); // Play the explosion effect
            }
        }

        StartCoroutine(DestroyAfterDelay(2f)); // Delay before bomb destruction
    }


    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}