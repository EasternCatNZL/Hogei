using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitTrigger : EnemyTrigger {

    //script refs
    public HermitMoveBehavior hermit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //trigger enter
    void OnTriggerEnter(Collider other)
    {
        //check other
        if((doTriggerPlayer && other.gameObject.CompareTag(targetTag)) || (doTriggerBullet && other.gameObject.CompareTag(bulletTag)) && !isTriggered)
        {
            //check object still exists
            if (hermit)
            {
                //set triggered to true
                isTriggered = true;
                hermit.isActive = true;
            }
        }
    }
}
