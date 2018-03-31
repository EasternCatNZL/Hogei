using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlawTrigger : MonoBehaviour {

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";

    OutlawBehaviour outlaw;

    private bool isTriggered = false; //checks to see if trigger has been triggered

    // Use this for initialization
    void Start () {
        outlaw = GetComponentInChildren<OutlawBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //trigger enter
    void OnTriggerEnter(Collider other)
    {
        //check other
        if ((other.gameObject.CompareTag(targetTag) || other.gameObject.CompareTag(bulletTag)) && !isTriggered)
        {
            //check object hasnt been destroyed before being triggered
            if (outlaw)
            {
                isTriggered = true;
                outlaw.target = GameObject.FindGameObjectWithTag(targetTag);
                outlaw.isSetup = true;
            }
            
        }
    }
}
