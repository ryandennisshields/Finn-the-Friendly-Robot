using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // OnCollisionEnter is called whenever 
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        { // If the other object has the tag "Player",
            GameManager.instance.KillPlayer(); // Kill the Player
            Destroy(this.gameObject); // Destroy this object
        }
        else
        { // If the other object does not have the tag "Player",
            Destroy(this.gameObject); // Destroy this object
        }
    }
}
