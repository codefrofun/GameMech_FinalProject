using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : MonoBehaviour
{
    public static bool hasKey = false; // Public state of key
    private bool isFollowing = false; // Public state of location/if player picked up key

    public float followSpeed = 5f; // Follow speed
    public Transform player; // Player position
    public Vector3 offset = new Vector3(-0.3f, 0.2f, 0f); // Defines the position relative to the player ( x a bit behind, y a bit above)

    private AudioSource pickupSound;

    private void Start()
    {
        pickupSound = GetComponent<AudioSource>(); // Get AudioSource attached to this object
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (pickupSound != null)
            {
                pickupSound.Play(); // Play pickup sound
            }

            Debug.Log("Key has  been opicked up, should now be following player");
            hasKey = true;
            player = other.transform; // Key follows player
            isFollowing = true; // key is picked up and following player
            GetComponent<Collider2D>().enabled = false; // Turn off collider so player cannot accidentally pick up key twice
        }
    }

    public void Update()
    {
        if(isFollowing && player != null) // Player exists and has key
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);  // Vector3.Lerp(A, B, t) moves an object from A (start position) to B (target position) gradually, speed determines how fast

        }
    }
}
