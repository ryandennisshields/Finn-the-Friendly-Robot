using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public ParticleSystem coinPickup; // Stores the coin pick up particle
    public ParticleSystem bigcoinPickup; // Stores the big coin pick up particle

    // OnTriggerEnter is called whenever an object triggers this object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        { // If the other object has the tag "Coin",
            GameManager.instance.AddScore(100); // Add 100 score
            GameManager.instance.CoinGet(); // Register that a coin has been picked up
            Instantiate(coinPickup, other.transform.position, other.transform.rotation); // Create the coin pick up effect on the object
            Destroy(other.gameObject); // Destroy the other game object
        }
        if (other.gameObject.tag == "Big Coin")
        { // If the other object has the tag "Big Coin",
            GameManager.instance.AddScore(1000); // Add 1000 score
            GameManager.instance.bigcoinsGained += 1; // Add 1 to big coins gained 
            GameManager.instance.BigCoinGet(); // Register that a big coin has been picked up
            Instantiate(bigcoinPickup, other.transform.position, other.transform.rotation); // Create the big coin pick up effect on the object
            Destroy(other.gameObject); // Destroy the other game object
        }
    }
}
