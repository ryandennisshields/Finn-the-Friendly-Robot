using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzyAI : MonoBehaviour
{

    private int moveDirection = 1; // Stores the initial moving direction (1 = right, -1 = left)

    private float moveSpeed = 4f; // Stores the movement speed

    private float movingTime = 2f; // Stores how long the object moves
    private float stopTime = 1f; // Stores how long the object stops
    private float fireTime = 1f; // Stores how long it takes before the object fires

    public Transform firePoint; // Stores the position of the firing point
    public GameObject bullet; // Stores the bullet

    private float bulletSpeed = 5f; // Stores the speed of the bullet 

    private bool active = true; // Stores a bool that decides if the object does anything, and sets it to true

    public Rigidbody2D rb; // Stores the rigidbody of the object

    public ParticleSystem explosion; // Stores the explosion particle

    // Fixed update is called once every physics frame
    void FixedUpdate()
    {
        StartCoroutine(BuzzyActions()); // Starts the actions of this enemy (Buzzy)
    } // End of FixedUpdate

    // OnCollisionEnter is called whenever this object collides with another
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerMovement.instance.touchingGround == false)
        { // If the other object has the tag "Player" and the player is not touching the ground,
            gameObject.SetActive(false); // Disable this object
            GameManager.instance.AddScore(1000); // Add 1000 score
            PlayerMovement.instance.player.AddForce(Vector2.up * PlayerMovement.instance.jumpForce, ForceMode2D.Impulse); // The player gets force added to them on the Y axis multiplied by the jump force
            Instantiate(explosion, transform.position, transform.rotation); // Create the explosion particle on this object's position
            Destroy(gameObject, 1); // Destroy this object after one second
        }
        else if (other.gameObject.tag == "Player" && PlayerMovement.instance.touchingGround == true)
        { // If the other object has the tag "Player" and the player is touching the ground,
            GameManager.instance.KillPlayer(); // Kill the player
        }
    } // End of OnCollisionEnter

    // Called by FixedUpdate
    IEnumerator BuzzyActions()
    {
        if (active == true)
        { // If the object is active,
            active = false; // Make the object inactive
            rb.velocity = new Vector2(moveDirection, 0) * moveSpeed; // Make the object move in the move direction times the movement speed
            yield return new WaitForSeconds(movingTime); // Wait for the moving time
            rb.velocity = new Vector2(0, 0); // Stop the object
            yield return new WaitForSeconds(fireTime); // Wait for the firing time
            GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation); // Sets the bullet to the position and rotation of the firepoint
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>(); // Gets the rigidbody of the bullet
            projectileRb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse); // Takes the rigidbody of the bullet and applies force to it, coming out upwards from the fire point
            yield return new WaitForSeconds(stopTime); // Wait for the stop time
            moveDirection *= -1; // Multiply the move direction by -1, so then positive becomes negative and negative becomes positive
            active = true; // Make the object active
        }
    } // End of BuzzyActions
}
