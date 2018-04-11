using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishTrigger : EnemyTrigger {

    //script refs
    public StarfishSpinBehavior star;

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
        if ((doTriggerPlayer && other.gameObject.CompareTag(targetTag)) || (doTriggerBullet && other.gameObject.CompareTag(bulletTag)) && !isTriggered)
        {
            //check object still exists
            if (star)
            {
                //set triggered to true
                isTriggered = true;
                star.isActive = true;
            }
        }
    }
}
