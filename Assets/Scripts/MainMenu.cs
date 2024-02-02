using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private string loadlevel1 = "Level 1"; // Stores the first level

    public GameObject howtoplayScreen; // Stores the how to play screen

    // Function to start the game
    public void StartGame()
    {
        SceneManager.LoadScene(loadlevel1); // Loads the first level

    } // End of startgame

    // Function to quit the game
    public void QuitGame()
    {
        Application.Quit(); // Closes the game

    } // End of quitgame

    // Function to access the how to play
    public void HowToPlay()
    {
        howtoplayScreen.SetActive(true); // Displays the how to play information/screen
    } // End of howtoplay

    // Function to go back to the main menu
    public void Back()
    {
        howtoplayScreen.SetActive(false); // Hides the how to play screen so the player can access the main menu
    } // End of back

} // End of class
