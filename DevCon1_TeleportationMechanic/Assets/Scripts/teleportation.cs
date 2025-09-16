using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public bool tempRage = false;
    public Camera Cam;
    Vector3 newPortalArea = Vector3.up;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray angry = Cam.ScreenPointToRay(Input.mousePosition);
        //get raycast from camera, 
        //Debug.Log("working");
        //if(Physics.Raycast)
        if (Physics.Raycast(angry, out hit))
        {

            newPortalArea = hit.point;
            //Debug.Log(hit.point);

        }
        if (Input.GetMouseButtonDown(0))
        {
            Portal1Pos = newPortalArea;
            //newPortalArea
            portal1.GetComponent<Transform>().position = Portal1Pos;
            //Debug.Log("yagsidhabsakfdjb");

        }
        else if (Input.GetMouseButtonDown(1))
        {
            Portal2Pos = newPortalArea;
            //newPortalArea
            portal2.GetComponent<Transform>().position = Portal2Pos;
            //Debug.Log("yagsidhabsakfdjb");
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //teleport player to other box
        //if collides with box 1: teleport player to box 2
        // else if collides with box 2: teleport player to box 1
    }
    public void OnMouseDown()
    {
        
    }
}
