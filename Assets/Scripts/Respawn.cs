using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public static Respawn instance; // Creates an instance of this code, allowing other code to use this code

    public GameObject player; // Stores the player object
    public GameObject respawnPos; // Stores the respawn position object

    // Awake is called when the game starts
    void Awake()
    {
        instance = this; // Creates an instance of this code, allowing other code to use this code
    }

    // Respawn player is called when the player respawns
    public void RespawnPlayer()
    {
        UIManager.instance.respawning.gameObject.SetActive(true); // Enables the respawning display on the UI
        GameManager.instance.respawnTime = 3; // Sets the respawn time to 3 seconds
        UIManager.instance.respawningText.text = "" + GameManager.instance.respawnTime; // Displays the remaining time until respawn to the player on the UI
        StartCoroutine(RespawnCommand()); // Starts the Respawn coroutine
    } // End of Respawn Player Function

    // Called by the respawn player function
    IEnumerator RespawnCommand()
    {
        while (GameManager.instance.respawnTime > 0) // While the respawn time of GameManager is greater than 0
        {
            player.SetActive(false); // Disables the player
            yield return new WaitForSeconds(1); // Wait for a second
            GameManager.instance.respawnTime -= 1; // Decrease the respawn time by 1
            UIManager.instance.respawningText.text = "" + GameManager.instance.respawnTime; // Display the new respawn time to the player on the UI
        }
        player.transform.position = respawnPos.transform.position; // Set the player's position to the respawn position
        player.SetActive(true); // Enables the player
        PlayerMovement.instance.IncreaseSpeed = true; // Resets the Increase Speed variable to true 
        PlayerMovement.instance.resetSpeed = true; // Resets the Reset Speed variable to true
        UIManager.instance.respawning.gameObject.SetActive(false); // Disables the respawning display on the UI
    } // End of Respawn coroutine
} // End of class

