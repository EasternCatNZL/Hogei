﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMenuNavigator : MonoBehaviour {

    [Header("Menu")]
    [Tooltip("The current menu controller is navigating")]
    public ControllerIndexedMenu menu;

    [Header("Dead zone var")]
    public float deadZone = 0.5f;

    [Header("Inputs")]
    public string contX = "CHorizontal";
    public string contY = "CVertical";

    //control vars
    private bool stickHeld = false; //checks if stick held to avoid scrolling through menu at lightspeed

    private int currentIndex = 0;  //the current index currently seleceted

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //set the current menu to navigate
    public void SetMenu(ControllerIndexedMenu thisMenu)
    {
        //reset current index to first
        currentIndex = 0;
        //set menu to this menu
        menu = thisMenu;
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
        menu.menuItemArray[currentIndex].CallSelectedEvent();
    }
}