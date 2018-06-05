using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {

    [Header("Input axis")]
    public string rightStickX = "CHorizontalAim";
    public string rightStickY = "CVerticalAim";

    [Header("Mouse input check handling")]
    [Tooltip("Amount mouse has to have moved to have been considered moved")]
    public float change = 1.0f;

    [Header("Controller check handling")]
    [Tooltip("Delay to check for controllers")]
    public float controllerDelayCheck = 2.0f;

    [Header("Dead zone var")]
    public float deadZone = 0.5f;

    [Header("Transform alignment")]
    public Transform alignment;

    //control vars
    private bool controllerConnected = false;

    private float controllerCheck = 0.0f;

    Vector3 mouseLastPos = Vector3.zero;

    //script refs
    private WhatCanIDO canDo;

	// Use this for initialization
	void Start () {
        canDo = GetComponent<WhatCanIDO>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canDo.useKeyboard)
        {
            MouseInput();
        }
        else if (canDo.useController)
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
        Vector3 direction = new Vector3(Luminosity.IO.InputManager.GetAxisRaw(rightStickX), 0.0f, Luminosity.IO.InputManager.GetAxisRaw(rightStickY));
        //only work if meaningful
        if (direction.sqrMagnitude < deadZone)
            {
                //check controller checker timing
                //controllerCheck -= Time.deltaTime;
                ////if no meaningful input has been recieved, set to false;
                //if(controllerCheck <= 0)
                //{
                //    controllerConnected = false;
                //}
                return;
            }
        //apply rotation
        float angle = Mathf.Atan2(Luminosity.IO.InputManager.GetAxisRaw(rightStickX), Luminosity.IO.InputManager.GetAxisRaw(rightStickY)) * Mathf.Rad2Deg;
        //print(angle);
        transform.rotation = Quaternion.Euler(0.0f, alignment.rotation.eulerAngles.y + angle, 0.0f);
        ////set controller check timing
        //controllerCheck = controllerDelayCheck;
    }

    ////check if mouse has moved enough to warrent change
    //private bool CheckMouseChange()
    //{
    //    bool hasMoved = false;
    //    //get the current pos
    //    Vector3 currentPos = MouseTarget.GetWorldMousePos();
    //    //check if it has moved enough to warrent input
    //    if(currentPos.x > mouseLastPos.x + change || currentPos.x < mouseLastPos.x - change
    //        || currentPos.y > mouseLastPos.y + change || currentPos.y < mouseLastPos.y - change
    //        || currentPos.z > mouseLastPos.z + change || currentPos.z < mouseLastPos.z - change)
    //    {
    //        hasMoved = true;
    //    }


    //    return hasMoved;
    //}
}
