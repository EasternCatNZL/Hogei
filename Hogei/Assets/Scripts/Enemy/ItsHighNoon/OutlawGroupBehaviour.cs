﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawGroupBehaviour : MonoBehaviour {

    [Header("Setup vars")]
    [Tooltip("Setup starting distance")]
    public float startDistance = 1.0f;
    [Tooltip("Setup step up distance")]
    public float stepDistance = 0.5f;
    [Tooltip("Time needed to setup")]
    public float setupTime = 0.5f;

    [Header("Enemy Group")]
    [Tooltip("Enemy array")]
    public GameObject[] enemyGroupArray = new GameObject[0];

    [Header("Tags")]
    public string targetTag = "Player";

    //control vars
    private bool hasSetup = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Setup function
    void Setup()
    {
        //for every enemy in the array
        for (int i = 0; i < enemyGroupArray.Length; i++)
        {
            //get this enemies setup distance
            float myDistance = startDistance + (stepDistance * i);
            //setup the vars for the enemy
            enemyGroupArray[i].GetComponent<OutlawBehaviour>().SetupVars(myDistance, setupTime);
            enemyGroupArray[i].GetComponent<OutlawBehaviour>().MoveToSetupLocation();
            enemyGroupArray[i].GetComponent<OutlawBehaviour>().isMoving = true;
        }
    }

    //trigger enter
    void OnTriggerEnter(Collider other)
    {
        //check other
        if (other.gameObject.CompareTag(targetTag) && !hasSetup)
        {
            Setup();
            //change has setup to true
            hasSetup = true;
        }
    }
}