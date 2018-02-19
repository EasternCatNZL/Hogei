using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OutlawBehaviour : MonoBehaviour {

    //set up vars
    [HideInInspector]
    public float setupDistance = 0.0f;
    [HideInInspector]
    public float setupTime = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //setup vars
    public void SetupVars(float setupDist, float time)
    {
        setupDistance = setupDist;
        setupTime = time;
    }

    //move
    public void MoveToSetupLocation()
    {
        //get the location to setup to
        Vector3 locationToSetUp = transform.position + (transform.forward * setupDistance);
    }
}
