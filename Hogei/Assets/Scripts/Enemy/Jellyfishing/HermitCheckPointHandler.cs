using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitCheckPointHandler : MonoBehaviour {

    //[Header("Tags")]
    //public string playerTag = "Player";

    //object refs
    private HermitMoveBehavior myHermit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //setup logic for setting up points
    public void SetupPoint(HermitMoveBehavior hermit)
    {
        myHermit = hermit;
    }

    //when hermit enters trigger, change destination
    private void OnTriggerEnter(Collider other)
    {
        //check if other is my hermit
        if(other.gameObject == myHermit.gameObject)
        {
            print("Collided with " + name);
            myHermit.ChangeDestination();
        }
    }
}
