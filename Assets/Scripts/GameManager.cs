using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance; // Allows other scripts to use code from this script

    public int currentLives = 3; // Stores the current lives

    public int respawnTime = 3; // Stores the amount of time before respawning the player

    public int currentScore = 0; // Stores the current score

    public int coinsGained = 0; // Stores the amount of coins gained

    public int bigcoinsGained = 0; // Stores the amount of big coins gained

    public int timeSpent = 0; // Stores the amount of time spent

    public int timebonusScore = 15000; // Stores the maximum bonus score for time spent

    public AudioSource playerDeathSound; // Stores the player death sound

    // Awake is called when the program loads
    void Awake()
    {
        instance = this; // Creates an instance of gamemanager, allowing other scripts to use it
    } // End of Awake

    // Start is called on the first frame
    void Start()
    {
        StartCoroutine(TimeLeft()); // Start the Time Left coroutine
        Time.timeScale = 1; // Sets the time scale to 1 (plays the game at normal speed)
        UIManager.instance.livesText.text = "X " + currentLives; // Displays the current lives from the UI manager
        UIManager.instance.scoreText.text = "" + currentScore; // Displays the current score from the UI manager
        UIManager.instance.respawningText.text = "" + respawnTime; // Displays the amount of time until the player respawns from the UI manager
        UIManager.instance.coinDisplay.text = "X " + coinsGained; // Displays the amount of coins gained from the UI manager
    } // End of Start

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        { // If the player presses the ESC key,
            if (currentLives > 0)
            { // If current lives are greater than 0,
                PauseGame(); // Run the Pause Game function
            }
        }
    } // End of update

    // Called whenever the player presses the ESC key
    public void PauseGame()
    {
        Time.timeScale = 0; // Sets the time scale to 0 (freezes the game)
        PlayerMovement.instance.enabled = false; // Disables the player, making them unable to do anything
        UIManager.instance.pauseScreen.SetActive(true); // Enables the pause screen
    } // End of pausegame

    // Called when the player dies
    public void KillPlayer()
    {
        currentLives--; // Removes a life
        UIManager.instance.livesText.text = "X " + currentLives; // Re-displays the lives after one is lost
        playerDeathSound.Play(); // Plays the player death sound

        if (currentLives > 0)
        { // If current lives is greater than 0,
            Respawn.instance.RespawnPlayer(); // Respawns the player using the respawn code
        }
        else
        { // If lives are not greater than 0,
            GameObject Player = GameObject.FindGameObjectWithTag("Player"); // Finds a game object with the tag "Player"
            Player.SetActive(false); // Sets the player false so the player can't do anything
            UIManager.instance.gameOverscreen.SetActive(true); // Displays the game over screen
            Time.timeScale = 0; // Sets the time scale to 0 (freezes the game)
        }

    } // End of Kill Player function
    
    // The function for adding score
    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd; // Adds on the score to the current score
        UIManager.instance.scoreText.text = "" + currentScore; // Displays the current score after being updated
    } // End of addscore

    // Called whenever the player gets a coin
    public void CoinGet()
    {
        coinsGained += 1; // Adds one to coins gained
        UIManager.instance.coinDisplay.text = "X " + coinsGained; // Displays the new value on the coin display
    } // End of coin get

    // Called whenever the player gets a big coin
    public void BigCoinGet()
    {
        if (bigcoinsGained == 1)
        { // If the player has collected 1 big coin,
            UIManager.instance.bigCoin1.gameObject.SetActive(true); // Display one big coin on the counter
        }
        if (bigcoinsGained == 2)
        { // If the player has collected 2 big coins,
            UIManager.instance.bigCoin2.gameObject.SetActive(true); // Display two big coins on the counter
        }
        if (bigcoinsGained == 3)
        { // If the player has collected 3 big coins,
            UIManager.instance.bigCoin3.gameObject.SetActive(true); // Display three big coins on the counter
            AddScore(10000); // Add 10,000 score
        }
    } // End of Big Coin Get function

    // TimeLeft is called by Start
    IEnumerator TimeLeft()
    {
        while (timeSpent >= 0) // Constantly run the code while the time spent is greater than or equal to 0
        {
            yield return new WaitForSeconds(1); // Wait for a second
            timeSpent += 1; // Increase the time spent
            timebonusScore -= 100; // Decrease the time bonus score
            UIManager.instance.timeSpentText.text = timeSpent + "s"; // Display the new amount of time left
        }
    } // End of TimeLeft
} // End of class
