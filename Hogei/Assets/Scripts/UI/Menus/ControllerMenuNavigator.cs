using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMenuNavigator : MonoBehaviour {

    [Header("Menu")]
    [Tooltip("The current menu controller is navigating")]
    public ControllerIndexedMenu menu;

    [Header("Dead zone var")]
    public float deadZone = 0.5f;

    [Header("Tags")]
    public string playerTag = "Player";

    [Header("Inputs")]
    public string contX = "CHorizontal";
    public string contY = "CVertical";
    public string contSelect = "CSelect";

    //control vars
    private bool stickHeld = false; //checks if stick held to avoid scrolling through menu at lightspeed
    private bool navigatingWithCont = false; //checks to see if was using controller last frame

    private int currentIndex = 0;  //the current index currently seleceted

    //script refs
    WhatCanIDO canDo;

	// Use this for initialization
	void Start () {
        canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
        //SetMenu(menu);
	}
	
	// Update is called once per frame
	void Update () {
        //only do when using controller
        if (canDo.useController)
        {
            //if was not using controller, re init
            if (!navigatingWithCont)
            {
                navigatingWithCont = true;
                SetMenu(menu);
            }
            //Cursor.visible = false;
            ResetStickHeld();
            NavigateMenu();
            SelectItem();
        }
        else
        {
            if (navigatingWithCont)
            {
                navigatingWithCont = true;
            }
            //Cursor.visible = true;
        }
	}

    //set the current menu to navigate
    public void SetMenu(ControllerIndexedMenu thisMenu)
    {
        //reset current index to first
        currentIndex = 0;
        //set menu to this menu
        menu = thisMenu;
        //if navigating with controller, then select
        if (navigatingWithCont)
        {
            //select the first menu item preemptively
            menu.menuItemArray[currentIndex].Selected();
        }
        
    }

    //navigation logic for menu
    private void NavigateMenu()
    {
        //check that stick is not already held down
        if (!stickHeld)
        {
            //check inputs
            //if positive
            if(Luminosity.IO.InputManager.GetAxisRaw(contX) > deadZone
                || Luminosity.IO.InputManager.GetAxisRaw(contY) > deadZone)
            {
                //set stick held to true
                stickHeld = true;
                //deselect current
                menu.menuItemArray[currentIndex].Deselected();
                //increment the current index
                currentIndex++;
                //check that current index has not exceeded the limit
                if(currentIndex >= menu.menuItemArray.Length)
                {
                    //loop round
                    currentIndex = 0;
                }
                //TODO set currently seleceted item to this index
                menu.menuItemArray[currentIndex].Selected();
            }
            //if negative
            else if (Luminosity.IO.InputManager.GetAxisRaw(contX) < -deadZone
                || Luminosity.IO.InputManager.GetAxisRaw(contY) < -deadZone)
            {
                //set stick held to true
                stickHeld = true;
                //deselect current
                menu.menuItemArray[currentIndex].Deselected();
                //decrement the current index
                currentIndex--;
                //check that current index has not become negative
                if(currentIndex < 0)
                {
                    //loop round
                    currentIndex = menu.menuItemArray.Length - 1;
                }
                //TODO set currently seleceted item to this index
                menu.menuItemArray[currentIndex].Selected();
            }
        }
    }

    //reset stick held
    private void ResetStickHeld()
    {
        //if stick between these values
        if((Luminosity.IO.InputManager.GetAxisRaw(contX) < deadZone
            && Luminosity.IO.InputManager.GetAxisRaw(contX) > -deadZone)
            && (Luminosity.IO.InputManager.GetAxisRaw(contY) < deadZone
            && Luminosity.IO.InputManager.GetAxisRaw(contY) > -deadZone))
        {
            stickHeld = false;
        }
    }

    //trigger seleceted item's select event
    private void SelectItem()
    {
        //check input
        if (Luminosity.IO.InputManager.GetButtonDown(contSelect))
        {
            menu.menuItemArray[currentIndex].CallInteractedEvent();
        }
        
    }
}
