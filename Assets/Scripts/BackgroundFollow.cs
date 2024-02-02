using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{

    public Transform player; // Stores the position of the player

    // Update is called once every frame
    void Update()
    {
        if (player.gameObject.activeSelf == true)
        { // If the player is active,
            transform.position = new Vector3(player.position.x, player.position.y, -5); // Background follows the player depending on the player's current position (-5 on the Z axis keeps the background at the back)
        }
    } // End of update
} // End of class