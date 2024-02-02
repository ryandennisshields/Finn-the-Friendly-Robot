using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    public Text firstScore; // Stores the first place score
    public Text secondScore; // Stores the second place score
    public Text thirdScore; // Stores the third place score
    public Text fourthScore; // Stores the fourth place score
    public Text fifthScore; // Stores the fifth place score

    private int score; // Stores the score

    // Start is called before the first frame update
    void Start()
    {
        score = EndLevelManager.instance.score; // Gets the score from the End of Level total score
        firstScore.text = PlayerPrefs.GetInt("FirstScore", 000000).ToString(); // Sets the first score to the first score stored within the player's registry
        secondScore.text = PlayerPrefs.GetInt("SecondScore", 000000).ToString(); // Sets the second score to the second score stored within the player's registry
        thirdScore.text = PlayerPrefs.GetInt("ThirdScore", 000000).ToString(); // Sets the third score to the third score stored within the player's registry
        fourthScore.text = PlayerPrefs.GetInt("FourthScore", 000000).ToString(); // Sets the fourth score to the fourth score stored within the player's registry
        fifthScore.text = PlayerPrefs.GetInt("FifthScore", 000000).ToString(); // Sets the fifth score to the fifth score stored within the player's registry

        if (score > PlayerPrefs.GetInt("FifthScore", 000000) && score < PlayerPrefs.GetInt("FourthScore", 000000))
        { // If the player's earned score is greater than the stored fifth score and less than the stored fourth score,
            PlayerPrefs.SetInt("FifthScore", score); // Set the fifth score to the player's earned score
            fifthScore.text = score.ToString(); // Display the new score on the leaderboard
        }
        if (score > PlayerPrefs.GetInt("FourthScore", 000000) && score < PlayerPrefs.GetInt("ThirdScore", 000000))
        { // If the player's earned score is greater than the stored fourth score and less than the stored third score,
            PlayerPrefs.SetInt("FourthScore", score); // Set the second score to the player's earned score
            fourthScore.text = score.ToString(); // Display the new score on the leaderboard
        }
        if (score > PlayerPrefs.GetInt("ThirdScore", 000000) && score < PlayerPrefs.GetInt("SecondScore", 000000))
        { // If the player's earned score is greater than the stored third score and less than the stored second score,
            PlayerPrefs.SetInt("ThirdScore", score); // Set the third score to the player's earned score
            thirdScore.text = score.ToString(); // Display the new score on the leaderboard
        }
        if (score > PlayerPrefs.GetInt("SecondScore", 000000) && score < PlayerPrefs.GetInt("FirstScore", 000000))
        { // If the player's earned score is greater than the stored second score and less than the stored first score,
            PlayerPrefs.SetInt("SecondScore", score); // Set the second score to the player's earned score
            secondScore.text = score.ToString(); // Display the new score on the leaderboard
        }
        if (score > PlayerPrefs.GetInt("FirstScore", 000000))
        { // If the player's earned score is greater than the stored first score,
            PlayerPrefs.SetInt("FirstScore", score); // Set the first score to the player's earned score
            firstScore.text = score.ToString(); // Display the new score on the leaderboard
        }
    } // End of start
} // End of class

