using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    public Camera Cam; // this imports the camera

    public Object portal1;
    public Object portal2;

    
    bool portal1Placed = false; //this is to track if the portals have been placed
    bool portal2Placed = false;

    bool inPortal1 = false; // this is to track if the player is in a portal
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
