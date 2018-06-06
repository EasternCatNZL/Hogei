using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    [Header("Alignment transforms")]
    public Transform alignment;

    [Header("Cursor move speed")]
    [Tooltip("Speed of cursor movement with controller")]
    public float cursorMoveSpeed = 5.0f;

    [Header("Inputs")]
    public string contX = "CHorizontal";
    public string contY = "CVertical";

    [Header("Tags")]
    public string playerTag = "Player";

    //script refs
    WhatCanIDO canDo;

	// Use this for initialization
	void Start () {
		
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

        }
	}

    //cursor should follow cursor when control set to keyboard
    void CursorFollow()
    {
        //get the cursors pos in the world
        Vector3 mousePos = MouseTarget.GetWorldMousePos();
        //align self with camera
        transform.rotation = alignment.rotation;
    }

    //cursor is controlled  using the controller when on controller controls
    void CursorMove()
    {

    }
}
