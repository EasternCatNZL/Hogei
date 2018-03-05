using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamTriggerer : MonoBehaviour {

    [Header("Tags")]
    public string targetTag = "Player";

    //script refs
    RamBehaviour ram;

    private bool isTriggered = false; //checks to see if trigger has been triggered

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
        if (other.gameObject.CompareTag(targetTag) && !isTriggered)
        {
            //check object to trigger still exists
            if (ram)
            {
                //change has setup to true
                isTriggered = true;
                ram.isTriggered = true;
                //set target object
                ram.target = other.gameObject;
                ram.ChargeUp();
            }
            
        }
    }
}
