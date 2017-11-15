using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    [Tooltip("Target Tag")]
    public string targetTag = "Player";

    //target ref
    private GameObject target;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag(targetTag);
    }
	
	// Update is called once per frame
	void Update () {
        LookAtPlayer();
	}

    //look at the player
    private void LookAtPlayer()
    {
        //get direction to target
        Vector3 directionToTarget = target.transform.position - transform.position;
        //remove any changes in y
        directionToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }
}
