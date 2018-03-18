using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {

    [Header("Player object")]
    public GameObject player;

    [Header("Force vars")]
    [Tooltip("The dash force")]
    public float dashForce = 15.0f;

    [Header("Speed")]
    [Tooltip("Top starting speed")]
    public float topSpeed = 30.0f;

    [Header("Timing vars")]
    [Tooltip("Length of dash(Time)")]
    public float dashTime = 1.0f;
    [Tooltip("Time between uses")]
    public float timeBetweenUses = 5.0f;

    //control vars
    private bool isDashing = false; //check if player is in dash

    private float dashStartTime = 0.0f; //time dash started
    private float decayRate = 0.0f; //rate speed dacays
    private float lastUseTime = 0.0f; //time dash was last used

    private Vector3 dashStartLocation = Vector3.zero; //the location dash began
    private Vector3 destination = Vector3.zero; //the location to aim at
    private Vector3 dashDirection = Vector3.zero; //direction of dash

    //script refs
    [Header("Script refs")]
    public WhatCanIDO canDo;
    public Movement movement;

    // Use this for initialization
    void Start () {
        decayRate = topSpeed / dashTime;
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
            //else
            //{
            //    player.GetComponent<Rigidbody>().velocity -= dashDirection * (decayRate * Time.deltaTime);
            //}
        }
	}

    //Use func
    public void Use()
    {
        //check timing
        if(Time.time > lastUseTime + timeBetweenUses && movement.GetDirection() != Vector3.zero)
        {
            //set up control vars
            isDashing = true;
            //dashStartLocation = player.transform.position;
            dashStartTime = Time.time;
            canDo.canMove = false;
            //get direction
            GetDashDirection();
            //start dash
            Dash();
            //set timing
            lastUseTime = Time.time;
        }
    }

    //get direction
    private void GetDashDirection()
    {
        //get the direction vector
        dashDirection = movement.GetDirection();
        //remove y change
        dashDirection.y = 0.0f;
    }

    //Dash logic
    private void Dash()
    {
        player.GetComponent<Rigidbody>().AddForce(dashDirection * dashForce, ForceMode.Impulse);
        //player.GetComponent<Rigidbody>().velocity = dashDirection * topSpeed;
    }
}
