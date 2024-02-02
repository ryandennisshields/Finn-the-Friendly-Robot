using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    // OnCollisionEnter is called when this object collides with another object
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        { // If the other object has the tag Player,
            GameManager.instance.KillPlayer(); // Kill the player
        }
        else
        { // If the object does not have the tag Player,
            Destroy(other.gameObject); // Destroy the other object
        }
    } // End of OnCollisionEnter
} // End of class
