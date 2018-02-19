using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTriggerer : MonoBehaviour {

    [Header("Tags")]
    public string targetTag = "Player";

    //script refs
    SheepBehaviour sheep;

    private bool isTriggered = false; //checks to see if trigger has been triggered

    // Use this for initialization
    void Start () {
        sheep = GetComponentInChildren<SheepBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //trigger enter
    void OnTriggerEnter(Collider other)
    {
        //check other
        if (other.gameObject.CompareTag(targetTag) && !isTriggered)
        {
            //Setup();
            //change has setup to true
            isTriggered = true;
            sheep.isTriggered = true;
            //set time charge begins to now
            sheep.timeChargeBegan = Time.time;
            //set target object
            sheep.target = other.gameObject;
        }
    }
}
