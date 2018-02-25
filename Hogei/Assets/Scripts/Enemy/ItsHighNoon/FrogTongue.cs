using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FrogTongue : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Travel time for tongue")]
    public float tongueTravelTime = 3.0f;
    [Tooltip("Extend held time")]
    public float extendHoldTime = 1.0f;

    [Header("Distance vars")]
    [Tooltip("Base distance for first bullet")]
    public float baseDistance = 3.0f;
    [Tooltip("Step distance for other bullets")]
    public float stepDistance = 1.5f;

    //control vars
    private bool isExtending = false; //check if tongue is extending
    private bool isRetracting = false; //check if tongue is retracting

    private float extendStartTime = 0.0f; //Time tongue extend began
    private float retractStartTime = 0.0f; //Time tongue retract began

    [Header("The tongue")]
    public List<GameObject> bulletList = new List<GameObject>();

    [Header("Tags")]
    public string playerTag = "Player";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isExtending && !isRetracting)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ExtendTongue();
            }
        }
        else if (isExtending)
        {
            if (Time.time > extendStartTime + tongueTravelTime + extendHoldTime)
            {
                RetractTongue();
            }
        }
        else if (isRetracting)
        {
            if(Time.time > retractStartTime + tongueTravelTime)
            {
                isRetracting = false;
            }
        }
	}

    //Extend tongue
    public void ExtendTongue()
    {
        //set extend to true
        isExtending = true;
        //set start time to now
        extendStartTime = Time.time;

        //for all bullets
        for(int i = 0; i < bulletList.Count; i++)
        {
            //get the destination of this bullet
            Vector3 destination = transform.position + (transform.forward * (baseDistance + (stepDistance * i)));
            //tween to this location
            bulletList[i].transform.DOMove(destination, tongueTravelTime, false);
        }
    }

    //Extend tongue aimed
    public void ExtendTongueAimed()
    {
        //set extend to true
        isExtending = true;
        //set start time to now
        extendStartTime = Time.time;

        //get direction to player
        Vector3 direction = GameObject.FindGameObjectWithTag(playerTag).transform.position - transform.position;

        //for all bullets
        for (int i = 0; i < bulletList.Count; i++)
        {
            //get the destination of this bullet
            Vector3 destination = transform.position + (transform.forward * (baseDistance + (stepDistance * i)));
            //tween to this location
            bulletList[i].transform.DOMove(destination, tongueTravelTime, false);
        }
    }

    //Retract tongue
    private void RetractTongue()
    {
        //set extend to true
        isRetracting = true;
        isExtending = false;
        //set start time to now
        retractStartTime = Time.time;

        //for all bullets
        for (int i = 0; i < bulletList.Count; i++)
        {
            //tween back to self
            bulletList[i].transform.DOMove(transform.position, tongueTravelTime, false);
        }
    }
}
