using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabbing : MonoBehaviour {

    public GameObject DebugSphere;
    public float ItemHoldDistance = 10f;
    public GameObject HeldItem = null;
    private Vector3 LastMousePos = Vector3.zero;

    private float LastTime;
    private bool JustPickedUp = false;

	// Update is called once per frame
	void Update () {
		if(!HeldItem && Input.GetMouseButtonDown(0))
        {
            CheckRayCast();
        }
        else if(!JustPickedUp && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = ItemHoldDistance;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 ReleaseForce = mousePos - LastMousePos;
            DebugSphere.transform.position = Vector3.zero;
            HeldItem = null;
        }
        if(HeldItem)
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
                DebugSphere.transform.position = LastMousePos;
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
    }

    public void SetHeldItem(GameObject _Object)
    {
        if (!HeldItem)
        {
            HeldItem = _Object;
            JustPickedUp = true;
        }
        else Debug.Log("Item already being held - " + HeldItem.name);
    }
}
