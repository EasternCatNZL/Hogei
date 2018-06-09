using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabbing : MonoBehaviour {

    public GameObject DebugSphere;
    public float ItemHoldDistance = 10f;
    public GameObject HeldItem = null;
    public bool ItemsFollowPlane = false;
    [Tooltip("The plane which the items can move across")]
    public GameObject MovementPlane = null;
    private Vector3 LastMousePos = Vector3.zero;

    private float LastTime;
    private bool JustPickedUp = false;
    private Vector3 lastPos = Vector3.zero;

    [Header("Inputs")]
    public string contSelect = "CSelect";

    [Header("Tags")]
    public string playerTag = "Player";

    void Start()
    {
        if (ItemsFollowPlane)
        {
            MovementPlane.SetActive(false);
        }
    }

	// Update is called once per frame
	void Update () {
        if(ItemsFollowPlane)
        {
            PlaneFollowGrabbing();
        }
        else
        {
            MouseFollowGrabbing();
        }
        
	}

    private void PlaneFollowGrabbing()
    {
        //This grabs items when there isn't one held on mouse click
        if (!HeldItem && Input.GetMouseButtonDown(0))
        {
            CheckRayCast();
        }
        //This lets go of the object on mouse click
        else if (!JustPickedUp)
        {
            //find player
            if (GameObject.FindGameObjectWithTag(playerTag))
            {
                //get can do script
                WhatCanIDO canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
                //if keyboard
                if (canDo.useKeyboard)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ReleaseHeldItem();
                    }
                }
                ////if controller
                //else if (canDo.useController)
                //{
                //    if (Luminosity.IO.InputManager.GetButton(contSelect))
                //    {
                //        ReleaseHeldItem();
                //    }
                //}
            }
        }
        //If there is a held item make it follow the mouse curser on the plane
        if (HeldItem)
        {            
            //raycast stuff
            RaycastHit rayHit; //ray hit info                             
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //send a ray from the position of mouse on screen

            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, 256))//256 is the layer for the floor           
            {
                lastPos = rayHit.point;
                HeldItem.transform.position = rayHit.point;
            }
            else HeldItem.transform.position = lastPos;
            JustPickedUp = false;
        }
    }

    //release held item
    public void ReleaseHeldItem()
    {
        if (DebugSphere) DebugSphere.transform.position = Vector3.zero;
        MovementPlane.SetActive(false);
        HeldItem.GetComponent<SphereCollider>().enabled = true;
        HeldItem = null;//By setting helditem to null it lets go of the object
    }

    private void MouseFollowGrabbing()
    {
        if (!HeldItem && Input.GetMouseButtonDown(0))
        {
            CheckRayCast();
        }
        else if (!JustPickedUp && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = ItemHoldDistance;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 ReleaseForce = mousePos - LastMousePos;
            DebugSphere.transform.position = Vector3.zero;
            HeldItem = null;
        }
        if (HeldItem)
        {
            //Make the held item follow the mouse
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = ItemHoldDistance;
            HeldItem.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            if (Time.time - LastTime > 0.1f)
            {

                //Set current mouse position as the LastMousePos
                LastMousePos = Camera.main.ScreenToWorldPoint(mousePos);
                LastTime = Time.time;
                //Debugging
                if (DebugSphere) DebugSphere.transform.position = LastMousePos;
            }
            JustPickedUp = false;

        }
    }

    void CheckRayCast()
    {
        RaycastHit rayHit; //ray hit info
        //send a ray from the position of mouse on screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out rayHit, Mathf.Infinity))
        {
            if(rayHit.collider.gameObject.CompareTag("Item"))
            {
                GrabItem(rayHit);
            }
        }
    }

    void GrabItem(RaycastHit rayHit)
    {
        HeldItem = rayHit.collider.gameObject;
        //Plane following
        if(ItemsFollowPlane)
        {
            HeldItem.GetComponent<SphereCollider>().enabled = false;
            MovementPlane.SetActive(true);
        }
    }

    public void SetHeldItem(GameObject _Object)
    {
        if (!HeldItem)
        {
            HeldItem = _Object;
            JustPickedUp = true;
            //Plane following
            if (ItemsFollowPlane)
            {
                HeldItem.GetComponent<SphereCollider>().enabled = false;
                MovementPlane.SetActive(true);
            }
        }
        else Debug.Log("Item already being held - " + HeldItem.name);
    }
}
