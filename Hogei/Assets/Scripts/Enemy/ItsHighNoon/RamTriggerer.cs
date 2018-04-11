using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamTriggerer : EnemyTrigger {

    //script refs
    RamBehaviour ram;

    // Use this for initialization
    void Start()
    {
        ram = GetComponentInChildren<RamBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //trigger enter
    void OnTriggerEnter(Collider other)
    {
        //check other
        if ((doTriggerPlayer && other.gameObject.CompareTag(targetTag)) || (doTriggerBullet && other.gameObject.CompareTag(bulletTag)) && !isTriggered)
        {
            //check object to trigger still exists
            if (ram)
            {
                //change has setup to true
                isTriggered = true;
                ram.isActive = true;
                //set target object
                ram.target = other.gameObject;
                ram.ChargeUp();
            }
            
        }
    }
}
