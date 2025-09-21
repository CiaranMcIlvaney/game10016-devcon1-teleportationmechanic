/*
 * Name: Ciaran McIlvaney
 * Student Number: 000945633
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // Variables for character movement
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float strafeSpeed = 7f;

    // Vector3 used to store the calcultions of the movements
    private Vector3 movement;

    // Rigidbody 
    private Rigidbody rb;

    // Variables for jumping mechanics 
    [Header("Jump")]
    public float jumpHeight = 7f;
    public bool isGrounded;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Use a different function for checking if player is grounded
        isGrounded = CheckGround();

        // If the player is touching the ground then they are allowed to jump
        if (isGrounded)
        {
            Jump();
        }
    }

    // FixedUpdate() is a function that is called every fixed interval
    // Usually used for physics related calculations

    private void FixedUpdate()
    {
        //Create two temporary float variables to hold the Inputs
        // Create two temp variables to hold the Horizontal (WASD) and Verticle (Mouse Movement) inputs 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Scale movement to a number between 0 and 1
        Vector3.Normalize(movement);

        // Set the verticle movemet to transform.forward times v (GetAxis is allowed to be anything from -1, 0, 1) then after times it by the moveSpeed
        // Do the same thing for the horizonatal movement but instead times it by transform.right and the strafeSpeed
        movement = (transform.forward * v * moveSpeed) + (transform.right * h * strafeSpeed);

        // rb.MovePosition is taking the current position and then adding the movement calculation done above. Then scaling by Time.deltaTime
        rb.MovePosition(transform.position + movement * Time.deltaTime);
    }
    void Jump()
    {
        // Can jump by using Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Adding upward force of to the rigidbody on the y axis times the jumpHeight (1 * 7 = 7)
            // ForceMode.Impulse means the frame that it was just called on, meaining right away instead of skipping a frame
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    
    // Bool function must return a true or false
    bool CheckGround()
    {
        // RaycastHit is a variable that stores collision information of a Raycast
        RaycastHit hit;

        // The parameters for this if statement is the starting point of the ray, then the direction of the ray, where to store the results,
        // length, then the layer to check
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, groundLayer))
        {
            // If all criteria is met then return true
            return true;
        }

        // If not then return false
        return false;
    }
}
