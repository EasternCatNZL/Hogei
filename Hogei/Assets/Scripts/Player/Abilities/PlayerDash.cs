using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {

    [Header("Player object")]
    public GameObject player;

    [Header("Distance vars")]
    [Tooltip("The distance to move in a dash")]
    public float dashDistance = 3.0f;

    [Header("Speed vars")]
    [Tooltip("Speed of dash")]
    public float speed = 5.0f;

    [Header("Timing vars")]
    [Tooltip("Length of dash(Time)")]
    public float dashTime = 1.0f;

    //control vars
    private bool isDashing = false; //check if player is in dash

    private float dashStartTime = 0.0f; //time dash started

    private Vector3 dashStartLocation = Vector3.zero; //the location dash began
    private Vector3 destination = Vector3.zero; //the location to aim at

    //script refs
    [Header("Script refs")]
    public WhatCanIDO canDo;
    public Movement movement;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isDashing)
        {
            if(Time.time < dashStartTime + dashTime)
            {
                Dash();
            }
            else
            {
                canDo.canMove = true;
                isDashing = false;
            }
        }
	}

    //Use func
    public void Use()
    {
        //Get position in direction
        destination = player.transform.position + (movement.GetDirection() * dashDistance);
        //set up control vars
        isDashing = true;
        dashStartLocation = player.transform.position;
        dashStartTime = Time.time;
        canDo.canMove = false;
    }

    //Dash logic
    private void Dash()
    {
        float distCovered = (Time.time - dashStartTime) * speed;
        float fracJourney = distCovered / dashDistance;
        player.transform.position = Vector3.Lerp(dashStartLocation, destination, fracJourney);
    }
}
