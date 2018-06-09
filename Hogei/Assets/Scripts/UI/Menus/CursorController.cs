using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

    //[Header("Cursor object")]
    //[Tooltip("Game representation of cursor")]
    //public GameObject cursorObject;

    [Header("Camera ref")]
    [Tooltip("Current camera in use")]
    public Camera cam;

    [Header("Cursor move speed")]
    [Tooltip("Speed of cursor movement with controller")]
    public float cursorMoveSpeed = 5.0f;

    [Header("Dead zone var")]
    public float deadZone = 0.5f;

    [Header("Inputs")]
    public string contX = "CHorizontal";
    public string contY = "CVertical";
    public string contSelect = "CSelect";

    [Header("Tags")]
    public string playerTag = "Player";
    public string soupTag = "Soup";

    //script refs
    WhatCanIDO canDo;

	// Use this for initialization
	void Start () {
        canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
	}

    void Awake()
    {
        canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (canDo.useKeyboard)
        {
            CursorFollow();
        }
        else if (canDo.useController)
        {
            CursorMove();
            CursorMouseBehavior();
        }
    }

    ////handle cursor distance from camera
    //void CursorDistanceFromCamera()
    //{
    //    //get vector3 of set distance from alignments forward
    //    Vector3 posAhead = alignment.forward * distanceFromCamera;
    //    //set the 
    //}

    //cursor should follow cursor when control set to keyboard
    void CursorFollow()
    {
        transform.position = Input.mousePosition;
    }

    //cursor is controlled  using the controller when on controller controls
    void CursorMove()
    {

        //vector 3 to direct movement
        Vector3 moveDirection = Vector3.zero;

        //get movement input from sticks
        //x axis
        if (Luminosity.IO.InputManager.GetAxisRaw(contX) > deadZone)
        {
            moveDirection += transform.right * cursorMoveSpeed * Luminosity.IO.InputManager.GetAxisRaw(contX);
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw(contX) < -deadZone)
        {
            moveDirection += transform.right * cursorMoveSpeed * Luminosity.IO.InputManager.GetAxisRaw(contX);
        }
        //y axis
        if (Luminosity.IO.InputManager.GetAxisRaw(contY) > deadZone)
        {
            moveDirection += transform.up * cursorMoveSpeed * Luminosity.IO.InputManager.GetAxisRaw(contY);
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw(contY) < -deadZone)
        {
            moveDirection += transform.up * cursorMoveSpeed * Luminosity.IO.InputManager.GetAxisRaw(contY);        }
        //move the object
        transform.position = transform.position + moveDirection * cursorMoveSpeed * Time.deltaTime;
    }

    //when on controller controls, launch ray from cursor pos into world as if mouse behavior
    private void CursorMouseBehavior()
    {
        //if camera not set, set it
        if (!cam) {
            cam = Camera.main;
        }
            
        //check if input
        if (Luminosity.IO.InputManager.GetButton(contSelect))
        {
            //first check soup manager holding something
            if (GameObject.FindGameObjectWithTag(soupTag).GetComponent<ItemGrabbing>().HeldItem)
            {
                //let go
                GameObject.FindGameObjectWithTag(soupTag).GetComponent<ItemGrabbing>().ReleaseHeldItem();
            }
            //else do things
            else
            {
                //send ray from cursor pos in canvas
                Ray ray = cam.ScreenPointToRay(transform.position);
                //get hit information
                RaycastHit rayHit;
                Physics.Raycast(ray, out rayHit, Mathf.Infinity);

                //if map node is hovered over
                if (rayHit.collider.gameObject.GetComponent<TableMapNode>())
                {
                    rayHit.collider.gameObject.GetComponent<TableMapNode>().LoadLevel();
                }
                else if (rayHit.collider.gameObject.GetComponent<IngredientBowl>())
                {
                    rayHit.collider.gameObject.GetComponent<IngredientBowl>().IngredientSelected();
                }
            }
            
        }

        
    }


}
