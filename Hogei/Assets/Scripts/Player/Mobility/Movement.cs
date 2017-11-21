using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Header("Remember to set the floor to the floor layor")]
    public float Speed = 0;

    public float Hori = 0;
    public float Vert = 0;

    [Header("Camera")]
    [Tooltip("Budget camera that follows the player and nothing else")]
    public GameObject followCamera;

    Rigidbody Rigid;
    Animator Anim;

    private WhatCanIDO canDo;
    

	// Use this for initialization
	void Start () {
        if(GetComponent<WhatCanIDO>())
        {
            canDo = GetComponent<WhatCanIDO>();
        }
        else
        {
            Debug.LogError("canDo can not be assigned. WhatCanIDO script not present on " + name);
        }

        Rigid = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (canDo.canMove)
        {
            MovePlayer();
        }
    }

    //move player pos
    private void MovePlayer()
    {
        Vector3 newPos = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            newPos.x += 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            newPos.x -= 1;
        }
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            newPos.z += 1;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            newPos.z -= 1;
        }

        if(newPos != Vector3.zero)
        {
            Anim.SetBool("IsMoving", true);
        }
        else
        {
            Anim.SetBool("IsMoving", false);
        }

        //Rigid.MovePosition(transform.position + newPos * Speed * Time.deltaTime);
        transform.position = transform.position + newPos * Speed * Time.deltaTime;

        if (followCamera)
        {
            //move the camera the same x and z
            followCamera.transform.position = followCamera.transform.position + newPos * Speed * Time.deltaTime;
        }
        
    }
}
