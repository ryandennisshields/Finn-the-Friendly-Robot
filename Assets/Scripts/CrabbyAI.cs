using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabbyAI : MonoBehaviour
{

    private int moveDirection = 1; // Stores the initial moving direction (1 = right, -1 = left)

    private float moveSpeed = 2f; // Stores the movement speed

    private float movingTime = 4f; // Stores how long the object moves
    private float stopTime = 1f; // Stores how long the object stops
    private float fireTime = 1f; // Stores how long it takes before the object fires

    public Transform firePoint1; // Stores the position of the first firing point
    public Transform firePoint2; // Stores the position of the second firing point
    public GameObject bullet; // Stores the bullet

    private float bulletSpeed = 5f; // Stores the speed of the bullet 

    private bool active = true; // Stores a bool that decides if the object does anything, and sets it to true

    public Rigidbody2D rb; // Stores the rigidbody of the object

    public ParticleSystem explosion; // Stores the explosion particle

    // Fixed update is called once every physics frame
    void FixedUpdate()
    {
        StartCoroutine(CrabbyActions()); // Starts the actions of this enemy (Crabby)
    }

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
    }

    // Called by FixedUpdate
    IEnumerator CrabbyActions()
    {
        if (active == true)
        { // If the object is active,
            active = false; // Make the object inactive
            rb.velocity = new Vector2(moveDirection, 0) * moveSpeed; // Make the object move in the move direction times the movement speed
            yield return new WaitForSeconds(movingTime); // Wait for the moving time
            rb.velocity = new Vector2(0, 0); // Stop the object
            yield return new WaitForSeconds(fireTime); // Wait for the firing time
            GameObject projectile1 = Instantiate(bullet, firePoint1.position, firePoint1.rotation); // Sets a bullet to the position and rotation of the first firepoint
            GameObject projectile2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation); // Sets a bullet to the position and rotation of the second firepoint
            Rigidbody2D projectileRb1 = projectile1.GetComponent<Rigidbody2D>(); // Gets the rigidbody of a bullet
            Rigidbody2D projectileRb2 = projectile2.GetComponent<Rigidbody2D>(); // Gets the rigidbody of a bullet
            projectileRb1.AddForce(firePoint1.up * bulletSpeed, ForceMode2D.Impulse); // Takes the rigidbody of a bullet and applies force to it, coming out upwards from the first fire point
            projectileRb2.AddForce(firePoint2.up * bulletSpeed, ForceMode2D.Impulse); // Takes the rigidbody of a bullet and applies force to it, coming out upwards from the second fire point
            yield return new WaitForSeconds(stopTime); // Wait for the stop time
            moveDirection *= -1; // Multiply the move direction by -1, so then positive becomes negative and negative becomes positive
            active = true; // Make the object active
        }
    }
}
