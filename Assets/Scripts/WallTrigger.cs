using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    private PlayerMovementScript playerScript;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript = other.gameObject.GetComponent < PlayerMovementScript>();

            playerScript.PlayerSwitchDirection();
        }
    }
}
