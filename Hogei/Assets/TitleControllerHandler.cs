using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleControllerHandler : MonoBehaviour {

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
        PlayerSettings = GetComponent<WhatCanIDO>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - LastTime > Delay)
        {
            if(PlayerSettings.useController) CheckControllerUpdate();
            LastTime = Time.time;
        }
    }

    void CheckControllerUpdate()
    {
        if (Luminosity.IO.InputManager.GetAxisRaw("CVertical") > Deadzone)
        {
            PreviousFocus();
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw("CVertical") < -Deadzone)
        {         
            NextFocus();
        }
        if (Luminosity.IO.InputManager.GetButtonDown("CSelect"))
        {
            Focus.onClick.Invoke();
        }
    }

    void NextFocus()
    {
        ButtonIndex++;
        if (ButtonIndex >= Buttons.Count)
        {
            ButtonIndex = Buttons.Count - 1;
        }
        else if (ButtonIndex < 0)
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
        if (ButtonIndex >= Buttons.Count)
        {
            ButtonIndex = Buttons.Count - 1;
        }
        else if (ButtonIndex < 0)
        {
            ButtonIndex = 0;
        }
        Focus = Buttons[ButtonIndex];
        Focus.Select();
    }
}
