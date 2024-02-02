using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelManager : MonoBehaviour
{
    public static EndLevelManager instance; // Creates an instance to allow other scripts to pull from this one

    public int score; // Stores the score
    public Text scoreText; // Stores the text of the score
    public Text lifeBonus; // Stores the text of the life bonus
    public Text timeBonus; // Stores the text of the time bonus
    private string mainmenu = "Main Menu"; // Stores the name of the main menu scene

    // Awake is called when the program loads
    void Awake()
    {
        instance = this; // Creates an instance of this code, allowing other scripts to use this code
    } // End of Awake

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.currentLives *= 2000; // Takes the current lives from gamemanager and times it by 200
        score = GameManager.instance.currentScore + GameManager.instance.currentLives + GameManager.instance.timebonusScore; // Adds together the score, current lives and time bonus to create the player's total score
        scoreText.text = "SCORE: " + score; // Displays the score
        lifeBonus.text = "LIFE BONUS: +" + GameManager.instance.currentLives; // Displays the extra score gained from the remaining lives
        timeBonus.text = "TIME BONUS: +" + GameManager.instance.timebonusScore; // Displays the extra score gained from the time bonus

    } // End of start

    // Called when the player wants to return to the main menu
    public void ReturntoMainMenu()
    {
        SceneManager.LoadScene(mainmenu); // Loads the main menu scene

    } // End of returntomainmenu

    // Called when the player wants to start the next level
    public void NextLevel()
    {
        // Next level code

    } // End of nextlevel
}
