using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ControllerIndexedMenuItem : MonoBehaviour {

    [Header("Sprite indicator")]
    [Tooltip("Visible when seleceted")]
    public SpriteRenderer selectedIndicator;
    [Tooltip("Visible when seleceted")]
    public Image selectedUiIndicator;

    [Header("Control vars")]
    public bool isSelected = false; //checks to see if current selected

    [Header("Events")]
    public UnityEvent selectedEvent;
    public UnityEvent deselectedEvent;
    public UnityEvent interactedEvent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when selected by controller <- same as mouse hovered over
    public void CallSelectedEvent()
    {
        //check event exists
        if (selectedEvent != null)
        {
            selectedEvent.Invoke();
        }
    }

    //when deselected by controller <- same as mouse hovered away
    public void CallDeselectedEvent()
    {
        //check event exists
        if (deselectedEvent != null)
        {
            deselectedEvent.Invoke();
        }
    }

    //when interacted with controller <- same as mouse clicked
    public void CallInteractedEvent()
    {
        //check event exists
        if (interactedEvent != null)
        {
            //deselect self
            Deselected();
            interactedEvent.Invoke();
        }
    }

    //when seleceted
    public void Selected()
    {
        //set seleceted to true
        isSelected = true;
        //make indicator visible
        if (selectedIndicator)
        {
            selectedIndicator.color = new Color(1, 1, 1, 1);
        }
        else if (selectedUiIndicator)
        {
            selectedUiIndicator.color = new Color(1, 1, 1, 1);
        }
        //call event when selected
        CallSelectedEvent();
    }

    //when deselected
    public void Deselected()
    {
        //set seleceted to true
        isSelected = false;
        //make indicator visible
        if (selectedIndicator)
        {
            selectedIndicator.color = new Color(0, 0, 0, 0);
        }
        else if (selectedUiIndicator)
        {
            selectedUiIndicator.color = new Color(0, 0, 0, 0);
        }
        //call event when deselected
        CallDeselectedEvent();
    }
}
