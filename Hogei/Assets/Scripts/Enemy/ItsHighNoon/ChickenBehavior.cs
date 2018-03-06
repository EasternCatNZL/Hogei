using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehavior : MonoBehaviour {

    [Header("Speed vars")]
    [Tooltip("Speed object travels at")]
    public float moveSpeed = 3.0f;

    [Header("Tags")]
    public string targetTag = "Player";

    //object refs
    private GameObject target;

    //control refs
    Rigidbody myRigid;

	// Use this for initialization
	void Start () {
        //debug
        target = GameObject.FindGameObjectWithTag(targetTag);
        myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveAtTarget();
	}

    //Move towards target, disregards terrain restrictions
    private void MoveAtTarget()
    {
        if (target)
        {
            //Look at target
            transform.LookAt(target.transform.position);
            //remove rotations on x and z
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            //move in that direction
            myRigid.velocity = transform.forward * moveSpeed;
            //transform.position += (transform.forward * moveSpeed) * Time.deltaTime;
        }
        else
        {
            myRigid.velocity = Vector3.zero;
        }
    }
}
