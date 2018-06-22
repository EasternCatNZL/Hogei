using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverControllerHandler : MonoBehaviour {

	public List<Button> Buttons;
	private int ButtonIndex = 0;
	public Button Focus;
	public WhatCanIDO PlayerSettings;
	[Header("Controller Settings")]
	public float Deadzone = 0.1f;
	public float Delay = 0.1f;
	private float LastTime;

	void Start()
	{
		Focus = Buttons[ButtonIndex];
		PlayerSettings = PlayerManager.GetInstance().Player.GetComponent<WhatCanIDO>();
	}
	// Update is called once per frame
	void Update () {
        if (PlayerSettings.useController) CheckControllerUpdate();

	}

	void CheckControllerUpdate()
	{
		if(Luminosity.IO.InputManager.GetAxisRaw("CHorizontal") > Deadzone)
		{
            if (Time.time - LastTime > Delay)
            {
                NextFocus();
                LastTime = Time.time;
            }
            
		}
		else if(Luminosity.IO.InputManager.GetAxisRaw("CHorizontal") < -Deadzone)
		{
            if (Time.time - LastTime > Delay)
            {
                PreviousFocus();
                LastTime = Time.time;
            }
            
		}
		if(Luminosity.IO.InputManager.GetButtonDown("CSelect"))
		{        
            if (Time.time - LastTime > Delay)
            {
                print("Selecting");
                Focus.onClick.Invoke();
                LastTime = Time.time;
            }
            
		}
	}

	void NextFocus()
	{
		ButtonIndex++;
		if(ButtonIndex >= Buttons.Count)
		{
			ButtonIndex = Buttons.Count -1;
		}
		else if(ButtonIndex < 0)
		{
			ButtonIndex = 0;
		}
		Focus = Buttons[ButtonIndex];
		Focus.Select();
	}

		void PreviousFocus()
	{
		print("Previous");
		ButtonIndex--;
		if(ButtonIndex >= Buttons.Count)
		{
			ButtonIndex = Buttons.Count -1;
		}
		else if(ButtonIndex < 0)
		{
			ButtonIndex = 0;
		}
		Focus = Buttons[ButtonIndex];
		Focus.Select();
	}
}
