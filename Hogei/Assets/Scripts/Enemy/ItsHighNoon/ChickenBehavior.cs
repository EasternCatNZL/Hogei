using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehavior : EnemyBehavior {

    [Header("Speed vars")]
    [Tooltip("Speed object travels at")]
    public float moveSpeed = 3.0f;

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";

    //object refs
    private GameObject target;

    //control refs
    private bool isActive = false;
    Rigidbody myRigid;

	// Use this for initialization
	void Start () {
        //debug
        //target = GameObject.FindGameObjectWithTag(targetTag);
        myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            MoveAtTarget();
        }
        
	}

    public void SetUp(GameObject thing)
    {
        isActive = true;
        target = thing;
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

    //On death logic
    public void AmDead()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if not active
        if (!isActive)
        {
            //check is bullet
            if (collision.gameObject.CompareTag(bulletTag))
            {
                //activate
                isActive = true;
                //set target
                target = GameObject.FindGameObjectWithTag(targetTag);
            }
        }
    }
}
