using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance; // Creates an instance of this code, allowing other code to use this code

    private float direction = 0f; // Stores the direction of the player, and sets it at 0
    private float acceldecel = 12f; // Stores how fast the player can accelerate (reach top speed) and decelerate (go from current speed to 0) and sets it at 12
    public float moveSpeedMult = 6f; // Stores the move speed multiplier (how fast the player can go), and sets it to 6
    
    public float jumpForce = 15f; // Stores the jump force (the amount of force applied to the player when the player jumps up), and sets it at 10

    private float frictionAmount = 0.2f; // Stores the maximum amount of friction that is used when the player stops (higher value = bigger slide when stopping at higher speeds), and sets it to 0.2

    public bool IncreaseSpeed = true; // Stores the increase speed boolean (Stops the Speed Up couroutine from constantly running the speed increase over and over, as it will only run while this is true), and sets it to true
    public bool resetSpeed = true; // Stores the reset speed boolean (Stops the Speed Up couroutine from constatnly running the move speed reset on direction change over and over, as it will only run while this is true), and sets it to true

    public Transform groundCheck; // Stores the ground check location (located at the player's feet)
    private Vector2 groundCheckSize = new Vector2(0.8f,0.2f); // Stores the ground check size (The size of the ground check across the x and y axis), and sets it to X = 0.8, Y = 0.2
    public LayerMask groundLayer; // Stores the layer mask of the ground (The layer assigned to the ground)
    public bool touchingGround; // Stores the touching ground boolean (If the player is touching the ground or not)

    public Rigidbody2D player; // Stores the rigidbody of the player

    public Animator playerAnimation; // Stores the player animations
    public AudioSource jump; // Stores the jump sound
    public ParticleSystem jumpEffect; // Stores the jump effect
    public GameObject runEffect; // Stores the running effect

    // Awake is called when the game starts
    void Awake()
    {
        instance = this; // Creates an instance of this code, allowing other code to use this code
    }

    // FixedUpdate is called once per engine frame
    void FixedUpdate()
    {
        touchingGround = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer); // Checks if the player is touching the ground by checking if the ground check game object is aligning with the ground
        playerAnimation.SetBool("touchingGround", touchingGround); // Sets the touchingGround bool of the animator to the touching ground bool of the code
        direction = Input.GetAxis("Horizontal"); // Changes the direction of the player dependant on horizontal input (right = 1, left = -1)

        if (direction > 0f)
        { // If the player's direction is to the right,
            player.transform.localScale = new Vector3(5, 5, 1); // Changes the scale of the player to face right when moving right
            playerAnimation.SetBool("Walking", true); // Sets the animation Walking bool to true
        }
        else if (direction < 0f)
        { // If the player's direction is to the left,
            player.transform.localScale = new Vector3(-5, 5, 1); // Changes the scale of the player to face left when moving left
            playerAnimation.SetBool("Walking", true); // Sets the animation Walking bool to true
        }

        float targetSpeed = direction * moveSpeedMult; // Calculates the target speed depending on the direction the player is going in times the movement speed multiplier
        float speedDif = targetSpeed - player.velocity.x; // Calculates the amount of force needed to change current speed to the target speed
        float movement = (Mathf.Abs(speedDif) * acceldecel) * Mathf.Sign(speedDif); // Calculates the movement speed dependant on the absolute of the speed difference (negative values will become positive), times the acceleration and deceleration speed, times the sign of the speed difference (-1 for negative values, 1 for positive values)
        player.AddForce(movement * Vector2.right); // Adds force to the player dependant on the movement speed times the vector 2 of (1, 0) (so then the value always comes out as a vector 2 only on the X axis, as the rigidbody can only add force dependant on a vector, and we want to go left and right, so the Y value should remain 0)
        
        if (touchingGround && direction == 0f)
        { // If the player is touching the ground and the player is not moving,
            float amount = Mathf.Min(Mathf.Abs(player.velocity.x), Mathf.Abs(frictionAmount)); // Sets the amount of friction applied depending on the minimum of either the positive value of the player's velocity, or positive value of the set friction amount (the friction amount controls the slide amount if the player's velocity is high, meaning even if they move fast, as long as the friction amount remains the minimum it'll take from that)
            amount *= Mathf.Sign(player.velocity.x); // The amount is then multiplied by the sign of the player's velocity (If the player is moving left, the amount will become negative, if the player is moving right, the amount will become positive)
            player.AddForce(Vector2.right * -amount, ForceMode2D.Impulse); // The player gets force added to them on the X axis times the negative of the amount (The negative amount on the player's velocity means they will have force added in the opposite direction of what direction they were moving before stopping (left will apply force to the right, right will apply force to the left))
        }

        if ( player.velocity.x > 0f || player.velocity.x < 0f)
        { // If the player's velocity is going in either direction on the X axis,
            StartCoroutine(SpeedUp()); // Starts the SpeedUp coroutine
            playerAnimation.SetBool("Walking", true); // Sets the animation Walking bool to true
        }
        else
        { // If the player's velocity is not in either direction on the X axis,
            moveSpeedMult = 6f; // Reset the move speed multiplier to 6
            StopCoroutine(SpeedUp()); // Stop the SpeedUp coroutine
            playerAnimation.SetBool("Walking", false); // Sets the animation Walking bool to false
            playerAnimation.SetBool("Running", false); // Sets the animation Running bool to false
            runEffect.gameObject.SetActive(false); // Disables the running effect
        }
    } // End of FixedUpdate

    // Update is called once every frame
    void Update()
    {
        if (!touchingGround)
        { // If the player is not touching the ground,
            jumpEffect.gameObject.SetActive(true); // Enables the jumping particles
            playerAnimation.SetBool("Walking", false); // Sets the animation Walking bool to false
            playerAnimation.SetBool("Running", false); // Sets the animation Running bool to false
            runEffect.gameObject.SetActive(false); // Disables the running effect
        }
        else
        { // If the player is touching the ground
            jumpEffect.gameObject.SetActive(false); // Disables the jumping particles
        }
              
        if (Input.GetButtonDown("Jump") && touchingGround)
        { // If the player presses the space bar and is touching the ground,
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // The player gets force added to them on the Y axis multiplied by the jump force
            jump.Play(); // Play the jump sound
        }
    } // End of Update

    // The SpeedUp courotuine gets called by other parts of the code
    IEnumerator SpeedUp()
    {
        if (IncreaseSpeed == true)
        { // If the increase speed bool is true,
            if (moveSpeedMult < 11f)
            { // If the player's move speed multiplier is less than 11,
                IncreaseSpeed = false; // Stop (set false) the increase speed bool
                yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
                moveSpeedMult *= 1.1f; // Increase the move speed multiplier by times 1.1
                yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
                IncreaseSpeed = true; // Start (set true) the increase speed bool
            }
            else
            { // If the player's move speed is greater than or equal to 11,
                moveSpeedMult = 12f; // Keep the move speed multiplier at 12
                playerAnimation.SetBool("Running", true); // Sets the animation Running bool to true
                runEffect.gameObject.SetActive(true); // Enables the running effect
            }
        }

        if (resetSpeed == true)
        { // If the reset speed bool is true,
            if (player.velocity.x > 0f)
            { // If the player's X velocity is greater than 0 (moving right),
                resetSpeed = false; // Stop (set false) the reset speed bool
                yield return new WaitUntil(() => direction < 0); // Wait until the player's direction is less than 0 (moving left)
                moveSpeedMult = 6f; // Reset the player's movement speed multiplier to 6
                playerAnimation.SetBool("Running", false); // Sets the animation Running bool to false
                runEffect.gameObject.SetActive(false); // Disables the running effect
                resetSpeed = true; // Start (set true) the reset speed bool
            }
            else if (player.velocity.x < 0f)
            { // If the player's X velocity is greater than 0 (moving left),
                resetSpeed = false; // Stop (set false) the reset speed bool
                yield return new WaitUntil(() => direction > 0); // Wait until the player's direction is greater than 0 (moving right)
                moveSpeedMult = 6f; // Reset the player's movement speed multiplier to 6
                playerAnimation.SetBool("Running", false); // Sets the animation Running bool to false
                runEffect.gameObject.SetActive(false); // Disables the running effect
                resetSpeed = true; // Start (set true) the reset speed bool
            }
        }
    } // End of SpeedUp coroutine
} // End of class

