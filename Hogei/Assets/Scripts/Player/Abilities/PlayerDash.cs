using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {

    [Header("Player object")]
    public GameObject player;

    [Header("Force vars")]
    [Tooltip("The dash force")]
    public float dashForce = 15.0f;

    [Header("Timing vars")]
    [Tooltip("Length of dash(Time)")]
    public float dashTime = 1.0f;
    [Tooltip("Time between uses")]
    public float timeBetweenUses = 5.0f;

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
            if(Time.time > dashStartTime + dashTime)
            {
                //Dash();
                canDo.canMove = true;
                isDashing = false;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
	}

    //Use func
    public void Use()
    {
        //Get position in direction
        //destination = player.transform.position + (movement.GetDirection() * dashDistance);
        //set up control vars
        isDashing = true;
        //dashStartLocation = player.transform.position;
        dashStartTime = Time.time;
        canDo.canMove = false;
        Dash();
    }

    //Dash logic
    private void Dash()
    {
        //get the direction vector
        Vector3 dashDirection = movement.GetDirection();
        //remove y change
        dashDirection.y = 0.0f;
        player.GetComponent<Rigidbody>().AddForce(dashDirection * dashForce, ForceMode.Impulse);
    }
}
