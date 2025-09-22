#### **Design Rationale:**

With Open World games and bigger games continuing to become more and more popular, having some sort of fast travel system is becoming quite necessary to mitigate the annoyance of tedious traveling, allowing players to focus more on main core part of the game, helping give players more immersion. While yes, some people tend to run around maps on feet and explore the maps that the Level Designers were able to create, having some sort of fast travel system will help players who just want to follow through with the games narrative and quest objectives be able to do so without the frustration of needing to walk everywhere on feet. 

Our prototype will be designed to explore teleportation as a mechanic inside of a 3D environment. Our goal is not intended on making a fully fleshed out fast travel system, but to test out how teleportation could impact a players game experience inside of a smaller scaled down space. 

Our rationale behind this prototype will be to spark curiosity from players, a sort of "**Where will this take me?"** kind of reaction, while also reducing the frustration of unnecessary and long traversal. 


#### **Objective:**

Our main objective with this prototype is to try and test whether or not a teleportation mechanic can improve players mobility by reducing the idea of unnecessary long on feet traversal, or would it introduce more confusion among them. We would like to try and look and see how players can experience the teleportation and whether or not it feels intuitive or disruptive. 

To do this we will firstly place 2 teleporters on the map that will both go to and from each other. That is going to be our main objective for the prototype. Just trying to make an easy cheap but solid working teleportation system. By completing this prototype we will be able to allow for players help give us feedback and a better understanding of the potential of teleportation inside of video games. 

Our objective is not to develop a fully fleshed out fast travel system, but to simply answer the design question: **Can teleportation be used in a way to balance player convenience with game engagement.** 

- Support level creation by cutting down on time
- Simple and efficient traveling 
- Less backtracking; faster traversal


#### **Core Statement:**

Is to create and develop a teleportation mechanic where a player enters a trigger/hitbox zone and is instantly transported into a different area of the map. 


#### **Main Gameplay Overview:**

The goal is to allow the player to freely move around a simple 3D test space using assets found on https://assetstore.unity.com/. To move around freely we will be using an already made player script from a project created a couple months ago [here.](#^player-scripts) 
- Players will be able to walk around the environment using WASD + the spacebar to move and jump around.
- Players will be able to press right or left click to place down temporary collision markers (Portals), and if both of the portals exist, entering the collider will teleport the player to the other portal. 
- If there's time we would like to make the teleporting as smooth as possible similar to Portal's idea where you smoothly get sent to the other area without any vision of moving inconsistently. 


#### **Team Roles**

Alice (Programmer): Will be writing most of the code for the prototype, including implementing the teleportation mechanic and making sure it works inside of Unity.

Ciaran (Designer/Documenter): Responsible for writing and fleshing out the design document, outlining the prototypes objectives, rationale, and core statements. Making sure that the projects vision is clearly communicated. 

Jasmine (Asset Researcher/Support): Responsible for finding free or open source assets to be used inside of the prototype. While her computer is currently damaged, she has still been contributing by identifying useful resources and helping a lot with the teams planning process. 


#### **Mechanics Breakdown**

Portal Placement: 
- The player can look at a valid wall and then drop a portal
- Left click places the first portal 
- Right click places the second portal
Activating Portals:
- The teleportation system only becomes active once two portals exist in the scene
Collision: 
- A collision/trigger box will be attached to each portal
- When the player enters the hitbox of one portal, they are instantly transported to the location of the other one
Focus of Testing:
- Making sure the mechanic feels smooth and easy to use
- Evaluating whether or not the mechanic improves player flow and reduces unnecessary traversal 

#### **Technical Notes**

- Engine: Unity 3D (RP) Version 2022.3.30f1
- Version Control: Git + Git LFS

#### **Controls and UX Feedback**
- Controls: WASD + Mouse, Left and Right mouse button to place portals
- Clarity: Colored portals to allow for easy understanding
- Teleporting: Don't want to take to long, as less clunky as possible

#### **Possible Problems**

- Infinite teleporting from each portal (need to add cooldown after teleporting)
- Portals places in correct spots only
- Motion sickness

#### **Player Scripts** ^player-scripts


To ease some of the stress for this prototype I have found a project made from a couple months ago with a player script already created. This will allow us to focus more on the main objective of our prototype. 


**Player Moving Script**
```csharp
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

```

**Player Camera Script**
```cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeballs : MonoBehaviour
{
    // Mouse movement variables 
    public float mouseSensitivity = 5f;
    public float smoothing = 1.5f; // Smoother mouse movement 

    // Vectors to store the calculations
    private Vector2 mouseLook;
    private Vector2 smoothMovement;

    // Reference to the player
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Call reference to the player
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // This will make your cursor invisible in the game
        // Press ESC to get cursor back
        Cursor.lockState = CursorLockMode.Locked;

        // Variable for mouse movemment
        Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X"),
                                Input.GetAxis("Mouse Y"));

        // Times the mouse input by the mouseSensitivity and smoothing
        mouseDirection.x *= mouseSensitivity * smoothing;
        mouseDirection.y *= mouseSensitivity * smoothing;

        // Linear interpolation between two positions. Moving between where the mouse is currently looking as well as the calculations done above
        smoothMovement.x = Mathf.Lerp(smoothMovement.x, mouseDirection.x, 1f / smoothing);
        smoothMovement.y = Mathf.Lerp(smoothMovement.y, mouseDirection.y, 1f / smoothing);

        // Add these calculations together
        mouseLook += smoothMovement;

        // Restrict the mouse position to make sure the player cannot rotate forever on the x-axis
        mouseLook.y = Mathf.Clamp(mouseLook.y, -80f, 90f);

        // Move the camera to the newest calculated position.
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        // Move the player object on the x-axis only
        player.transform.rotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}

```


#### **Teleportation Code**

Down below is the code to how Alice was able to get the teleportation mechanic to work.

Teleportation Script
```csharp

using Unity.VisualScripting;
using UnityEngine;

public class teleportation : MonoBehaviour
{
    // Alice/JamesBellefeuille
    // Start is called before the first frame update
    void Start()
    { // this gets portal potitions
        portal1Pos = portal1.GetComponent<Transform>().position;
        portal2Pos = portal2.GetComponent<Transform>().position;
    }
    Vector3 portal1Pos;
    Vector3 portal2Pos;

    [SerializeField] public Camera Cam; // this imports the camera

    [SerializeField] public Object portal1;
    [SerializeField] public Object portal2;

    
    bool portal1Placed = false; //this is to track if the portals have been placed
    bool portal2Placed = false;

    bool inPortal1 = false; // this is to track if the player  isin a portal
    bool inPortal2 = false;

    // Update is called once per frame
    void Update()
    {
        // This checks if the player presses a button and if so runs portal placment
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            PortalPlacment();
        }
    }
    public void PortalPlacment()
    { 
        // this fuction places the portals
        RaycastHit hit;
        Ray cast = Cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cast, out hit))
        { // this checks if the raycast hit and if so it save the position
            Vector3 newPortalArea = hit.point; // this is where the new portal will be placed
            if (Input.GetMouseButtonDown(0))
            { // if you press the left mouse button this places a portal
                portal1Pos = newPortalArea;
                portal1.GetComponent<Transform>().position = portal1Pos;
                portal1Placed = true;
            }
            else if (Input.GetMouseButtonDown(1))
            { // if you press the right mouse button this places a portal
                portal2Pos = newPortalArea;
                portal2.GetComponent<Transform>().position = portal2Pos;
                portal2Placed = true;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    { // this is for when you enter a portal to teleport the player to the other portal
        bool portalsPlaced = portal1Placed && portal2Placed;
        if (!inPortal1 && !inPortal2) // this checks that the player isnt in a portal atm
        {
            if (other.name == "portal1" && portalsPlaced)
            { // if your in the first portal this teleports you to the second one
                this.transform.position = portal2Pos;
                inPortal2 = true; // after going through a portal this makes it so you wont teleport back and forth constantly

            }
            else if (other.name == "portal2" && portalsPlaced)
            { // if your in the second portal this teleports you to the first one
                this.transform.position = portal1Pos;
                inPortal1 = true;
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        // this renables the portals when you leave a portal
        if (other.name == "portal1")
        {
            inPortal1 = false;

        }
        else if (other.name == "portal2")
        {
            inPortal2 = false;
        }

    }
}

```

Using raycasts from the camera the player can left or right click on their mouse to place down a portal at the position where you aimed at. Once both are activated inside of the level, entering one instantly teleports the player to the other. 

**How It Now Works**

Players can place portals using left or right mouse button. Entering one portal instantly moves the player to the other portal placed down. OnTriggerEnter/Exit handles the teleport activation. Right now portals can be placed anywhere but only look the correct way if angled at a wall 0°, or 90° rotations. Surfaces on other angles do not properly orient the portals visuals. Overall throughout testing we found out that the teleportation mechanic works quite well without any real bugs or glitches. This has confirmed our core function works, but also shows us what we could do next if we wanted to move forward. 


#### **Out of Scope / Possible Future Work**

- It would be nice to have a render of the world through the portal, sort of a looking through the other room effect
- Keeping momentum when going through the portals could also be cool to try and implement one day
- UI indicators showing us which portals are active and where they currently are on the map (Like a bright glow around the portal you can see through walls)

All these ideas would be really cool to try and implement however will take too much time to develop as we have only so much time.


#### **Sources / Assets**

- [Unity Asset Store](https://assetstore.unity.com/)
- [Cool Visual Effects - Part 1 - URP Support](https://assetstore.unity.com/packages/vfx/particles/cool-visual-effects-part-1-urp-support-176571)
- [Simple Modular Castle Assets](https://assetstore.unity.com/packages/3d/props/simple-modular-castle-assets-309297)
- .[gitattributes Used Reference](https://gist.github.com/nemotoo/b8a1c3a0f1225bb9231979f389fd4f3f) 