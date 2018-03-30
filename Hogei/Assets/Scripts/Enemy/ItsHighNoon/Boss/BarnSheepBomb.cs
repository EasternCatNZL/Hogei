using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnSheepBomb : SheepBehaviour {

    [HideInInspector]
    public float launchSpeed = 10.0f;
    [HideInInspector]
    public float dropSpeed = 10.0f;
    [HideInInspector]
    public float dropHeight = 10.0f;
    [HideInInspector]
    public Vector3 dropPos = Vector3.zero;

    //control vars
    private bool isDropping = false; //check is should be falling

    private float startHeight = 0.0f; //the start height of this object

    private Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();

        startHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDropping)
        {
            Launch();
        }
        else if (isDropping)
        {
            Drop();
        }
        
	}

    //setup vars
    public void SetupVars(float upSpeed, float downSpeed, float height, Vector3 pos)
    {
        launchSpeed = upSpeed;
        dropSpeed = downSpeed;
        dropHeight = height;
        dropPos = pos;
    }

    //launch logic
    private void Launch()
    {
        rigid.velocity = Vector3.up * launchSpeed;
        //if reached drop height, start dropping
        if(transform.position.y >= dropHeight)
        {
            isDropping = true;
            //move to drop pos
            transform.position = dropPos;
        }

    }

    //Drop logic
    private void Drop()
    {

        //begin falling
        rigid.velocity = -Vector3.up * dropSpeed;
    }

    //on collision override
    private void OnCollisionEnter(Collision collision)
    {
        //release bullets
        BulletExplosion();
        //destroy self
        Destroy(gameObject);
    }
}
