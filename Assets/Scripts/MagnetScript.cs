using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public GameObject magnet;
    private Rigidbody2D playerRigidbody;
    private Collider2D magnetCollider;

    private bool isFrozen = false;

    private void Start()
    {
        playerRigidbody = magnet.GetComponent<Rigidbody2D>();
        magnetCollider = magnet.GetComponent<Collider2D>();
        magnetCollider.enabled = false;
        magnet.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleFreeze();
        }
    }


    void ToggleFreeze()
    {
        // Freeze and unfreeze player
        if (isFrozen)
        {
            UnfreezePlayer();
        }
        else
        {
            FreezePlayer();
        }
    }

    void FreezePlayer()
    {
        isFrozen = true;
        playerRigidbody.velocity = Vector2.zero; // Set movement to 0
        playerRigidbody.isKinematic = true;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        magnetCollider.enabled = true;

        Debug.Log("Player is frozen, magnet set to magnet layer");
    }

    void UnfreezePlayer()
    {
        isFrozen = false;
        playerRigidbody.isKinematic = false;
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        magnetCollider.enabled = false;

        magnet.GetComponent<Rigidbody2D>().gravityScale = 0;

        Debug.Log("Player is unfrozen");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFrozen)
        {
            Debug.Log("Colliding with player");
        }
    }
}
