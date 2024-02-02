using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance; // Allows other scripts to use UImanager's code

    public GameObject gameOverscreen; // Stores the game over screen

    public GameObject pauseScreen; // Stores the pause screen

    public Text livesText; // Stores the life text

    public GameObject respawning; // Stores the respawning object

    public Text respawningText; // Stores the respawning text

    public Text timeSpentText; // Stores the time left text

    public Text scoreText; // Stores the score text

    public Text coinDisplay; // Stores the coin display text

    public Image bigCoin1; // Stores the big coin 1 image
    public Image bigCoin2; // Stores the big coin 1 image
    public Image bigCoin3; // Stores the big coin 1 image

    public string mainMenuName = "Main Menu"; // Stores the main menu name, and names it Main Menu

    // Awake is called before the first frame update
    void Awake()
    {
        instance = this; // Creates an instance of this script, allowing other scripts to use UImanager's code

    } // End of Awake

    // Called when a scene needs to restart
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restarts the current scene

    } // End of restart

    // Called when the user wants to quit to main menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName); // Loads the main menu screen

    } // End of quittomainmenu

    // Called when the player un-pauses
    public void UnPause()
    {
        Time.timeScale = 1; // Sets the time scale to 1 (plays the game at normal speed)
        PlayerMovement.instance.enabled = true; // Enables the player
        pauseScreen.SetActive(false); // Disables the pause screen
    } // End of unpause
} // End of class