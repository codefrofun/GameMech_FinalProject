using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionScript : MonoBehaviour
{
    public bool bombExploded = false;

    public float explosionRadius = 0.49f;  // Adjust explosion range as needed

    public AudioClip tickSound; // ticking noise before explosion
    public AudioClip explodeSound; // sound for explosion
    private AudioSource audioSource;

    public float delayTime = 2f;
    public ParticleSystem explosionParticles;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (explosionParticles != null)
        {
            //explosionParticles.gameObject.SetActive(false); // Deactivate the particle system initially
        }
    }

    public void Update()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (bombExploded) return;

            if (audioSource != null && tickSound != null)
            {
                audioSource.PlayOneShot(tickSound); // Play the sound once
                StartCoroutine(WaitForBomb());
            }
        }
    }

    public IEnumerator WaitForBomb()
    {
        yield return new WaitForSeconds(delayTime);
        BombExplode();
    }


    void BombExplode()
    {
        if (bombExploded) return; // Prevent multiple explosions

        bombExploded = true;

        // Stop ticking sound if still playing
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Play explosion sound
        if (audioSource != null && explodeSound != null)
        {
            audioSource.PlayOneShot(explodeSound);
        }

        // Play explosion particles
        if (explosionParticles != null)
        {
            ParticleSystem explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            explosion.Play();
        }

        // Find the player and check if within explosion range
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= explosionRadius)
            {
                Debug.Log("Player is within explosion range! Applying damage.");
                player.TakeDamage(); // Call method on PlayerMovement to apply damage
            }
            else
            {
                Debug.Log("Player is outside explosion range. No damage taken.");
            }
        }

        StartCoroutine(DestroyAfterDelay(2f)); // Delay before bomb is destroyed
    }


    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}