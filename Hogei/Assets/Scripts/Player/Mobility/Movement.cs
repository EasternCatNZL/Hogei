using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [Header("Remember to set the floor to the floor layor")]
    public float Speed = 0;
    private float currentSpeed = 0;
    private float SpeedModifier = 1;

    public float Hori = 0;
    public float Vert = 0;

    private Vector3 Direction = Vector3.zero;

    [Header("Sounds")]
    public AudioClip FootstepSound = null;

    [Header("Camera")]
    [Tooltip("Budget camera that follows the player and nothing else")]
    public GameObject followCamera;

    [Header("Input axis")]
    public string leftStickX = "LeftStickX";
    public string leftStickY = "LeftStickY";

    //component refs
    Rigidbody Rigid;
    Animator Anim;

    //script ref
    private WhatCanIDO canDo;

    //control vars <- Budget camera vars
    float yOffset = 0.0f;
    float zOffset = 0.0f;
    Vector3 cameraOffset = Vector3.zero;
    

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
            if (canDo.useKeyboard)
            {
                MovePlayer();
            }
            else if (canDo.useController)
            {
                MovePlayerController();
            }
        }
    }

    //move player pos
    private void MovePlayer()
    {
        Vector3 newPos = Vector3.zero;
        Direction = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            newPos.z += 1;
            Direction += new Vector3(0f, 0f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            newPos.z -= 1;
            Direction += new Vector3(0f, 0f, -1f);
        }
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            newPos.x -= 1;
            Direction += new Vector3(-1f, 0f, 0f);
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            newPos.x += 1;
            Direction += new Vector3(1f, 0f, 0f);
        }

        if(newPos != Vector3.zero)
        {
            Anim.SetBool("IsMoving", true);
        }
        else
        {
            Anim.SetBool("IsMoving", false);
        }
        newPos.Normalize();
        transform.position = transform.position + newPos * (Speed * SpeedModifier) * Time.deltaTime;
        //Rigid.MovePosition(transform.position + newPos * (Speed * SpeedModifier) * Time.deltaTime);
        //Rigid.velocity = Direction * (Speed * SpeedModifier);

    }

    //move player pos
    private void MovePlayerController()
    {
        Vector3 newPos = Vector3.zero;
        Direction = Vector3.zero;
        if (Input.GetAxisRaw(leftStickX) > 0f)
        {
            newPos.z += 1;
            Direction += new Vector3(0f, 0f, 1f);
        }
        else if (Input.GetAxisRaw(leftStickX) < 0f)
        {
            newPos.z -= 1;
            Direction += new Vector3(0f, 0f, -1f);
        }
        if (Input.GetAxisRaw(leftStickY) > 0f)
        {
            newPos.x -= 1;
            Direction += new Vector3(-1f, 0f, 0f);
        }
        else if (Input.GetAxisRaw(leftStickY) < 0f)
        {
            newPos.x += 1;
            Direction += new Vector3(1f, 0f, 0f);
        }

        if (newPos != Vector3.zero)
        {
            Anim.SetBool("IsMoving", true);
        }
        else
        {
            Anim.SetBool("IsMoving", false);
        }
        newPos.Normalize();
        Rigid.MovePosition(transform.position + newPos * (Speed * SpeedModifier) * Time.deltaTime);
        //transform.position = transform.position + newPos * (Speed * SpeedModifier) * Time.deltaTime;

    }

    public void SetSpeedModifier(float _Percentage)
    {
        SpeedModifier = _Percentage;
    }

    public Vector3 GetDirection()
    {
        return Direction.normalized;
    }

    private void PlayFootstepSound()
    {
        MusicManager.PlaySoundAtLocation(FootstepSound, transform.position);
    }
}
