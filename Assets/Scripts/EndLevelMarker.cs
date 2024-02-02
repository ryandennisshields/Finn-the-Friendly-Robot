using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMarker : MonoBehaviour
{
    
    private string endoflevel = "End of Level"; // Stores the end of level string and sets it to "End of Level"

    // OnTriggerEnter is called whenever another object triggers this object
    void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.tag == "Player")
        { // If the other object has the tag "Player",
            SceneManager.LoadScene(endoflevel); // Load the end of level scene
        }
    } // End of OnTriggerEnter
}
