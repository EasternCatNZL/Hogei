using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageManager : MonoBehaviour {

    //control vars
    private bool isActivated = false; //checks if passage has been activated by any part

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //return passage activation state
    public bool GetIsActivated()
    {
        return isActivated;
    }

    //activate passage
    public void ActivatePassage(bool activate)
    {
        isActivated = activate;
    }
}
