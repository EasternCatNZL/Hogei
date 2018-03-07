using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {

    [Header("Input axis")]
    public string rightStickX = "RightStickX";
    public string rightStickY = "RightStickY";

    [Header("Mouse input check handling")]
    [Tooltip("Amount mouse has to have moved to have been considered moved")]
    public float change = 1.0f;

    [Header("Dead zone var")]
    public float deadZone = 0.5f;

    //control vars
    Vector3 mouseLastPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(CheckMouseChange())
        {
            MouseInput();
        }
        else
        {
            ControllerInput();
        }
    }

    //mouse input
    private void MouseInput()
    {
        Vector3 Direction = MouseTarget.GetWorldMousePos() - transform.position;
        //Player Rotations
        if (Vector3.Dot(transform.right, Direction) < 0.0f)
        {
            transform.Rotate(0.0f, -Vector3.Angle(transform.forward, Direction), 0.0f);
        }
        if (Vector3.Dot(transform.right, Direction) > 0.0f)
        {
            transform.Rotate(0.0f, Vector3.Angle(transform.forward, Direction), 0.0f);
        }

        mouseLastPos = MouseTarget.GetWorldMousePos();
    }

    //controller input
    private void ControllerInput()
    {
        //get direction from sticks
        Vector3 direction = new Vector3(Input.GetAxis(rightStickX), 0.0f, Input.GetAxis(rightStickY));
        //only work if meaningful
        if(direction.sqrMagnitude < deadZone)
        {
            return;
        }
        //apply rotation
        float angle = Mathf.Atan2(Input.GetAxis(rightStickX), Input.GetAxis(rightStickY)) * Mathf.Rad2Deg;
        print(angle);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    //check if mouse has moved enough to warrent change
    private bool CheckMouseChange()
    {
        bool hasMoved = false;
        //get the current pos
        Vector3 currentPos = MouseTarget.GetWorldMousePos();
        //check if it has moved enough to warrent input
        if(currentPos.x > mouseLastPos.x + change || currentPos.x < mouseLastPos.x - change
            || currentPos.y > mouseLastPos.y + change || currentPos.y < mouseLastPos.y - change
            || currentPos.z > mouseLastPos.z + change || currentPos.z < mouseLastPos.z - change)
        {
            hasMoved = true;
        }


        return hasMoved;
    }
}
