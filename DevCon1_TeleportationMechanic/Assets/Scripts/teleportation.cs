using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class teleportation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector3 Portal1Pos = new Vector3 (0, 0, 0);
    Vector3 Portal2Pos = new Vector3(0, 0, 0);
    public Object portal1;
    public Object portal2;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //teleport player to other box
        //if collides with box 1: teleport player to box 2
        // else if collides with box 2: teleport player to box 1
    }
    public void OnMouseDown()
    {
        RaycastHit hit;
        //get raycast from camera, 

        Vector3 newPortalArea = Vector3.up; // temporary
        if (true)// mouse click
        {
            Portal1Pos = newPortalArea;
            //newPortalArea
        }
    }
}
