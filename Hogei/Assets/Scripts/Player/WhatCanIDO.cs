using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatCanIDO : GameEntity
{
    public bool DontDestroy = true;
    [Header("Control bools")]
    public bool canMove = false;
    public bool canShoot = false;
    public bool canAbility = false;
    public bool canTalk = false;
    public bool useKeyboard = true;
    public bool useController = false;

    [Header("Input settings")]
    [Tooltip("Keyboard scheme name")]
    public string keyboardSchemeName = "KeyboardMouse";
    [Tooltip("Controller scheme name")]
    public string controllerSchemeName = "Controller";
    [Tooltip("Joystick number")]
    [Range(0, Luminosity.IO.InputBinding.MAX_JOYSTICKS)]
    public int joystick = 0;
    [Tooltip("Timeout timer")]
    public float timeout = 0.1f;

    //control vars
    private float lastScanTime = 0.0f; //time last input scan occured

    private Luminosity.IO.InputAction inputAct;
    private KeyCode cancelKey = KeyCode.Escape;

    // Use this for initialization
    void Start () {
       if(DontDestroy) DontDestroyOnLoad(gameObject);

        SetControlScheme();
	}
	
	// Update is called once per frame
	void Update () {
        if (!PauseHandler.isPaused)
        {
            if(Time.time > lastScanTime + timeout)
            {
                PeriodicInputScan();
            }
        }
    }
    
    //set control scheme on start
    private void SetControlScheme()
    {
        if (Luminosity.IO.InputManager.PlayerOneControlScheme.Name == keyboardSchemeName)
        {
            useKeyboard = true;
            useController = false;
        }
        else if (Luminosity.IO.InputManager.PlayerOneControlScheme.Name == controllerSchemeName)
        {
            useKeyboard = false;
            useController = true;
        }
    }

    //All in one funcs
    public void TurnAllOn()
    {
        canMove = true;
        canShoot = true;
        canAbility = true;
    }

    public void TurnAllOff()
    {
        canMove = false;
        canShoot = false;
        canAbility = false;
    }

    //switch between keyboard and controller
    public void SwitchKeyboardToController()
    {
        useKeyboard = false;
        useController = true;
    }

    public void SwitchControllerToKeyboard()
    {
        useKeyboard = true;
        useController = false;
    }

    //Perform periodic scan
    private void PeriodicInputScan()
    {
        //set timing 
        lastScanTime = Time.time;
        //print("Scan start");
        //make sure input scan is not already occuring
        if (!Luminosity.IO.InputManager.IsScanning /*&& inputAct != null*/)
        {
            //set up scan settings
            Luminosity.IO.ScanSettings settings;
            settings.Joystick = joystick;
            settings.Timeout = timeout;
            settings.CancelScanKey = cancelKey;
            settings.UserData = null;
            //check the current input
            if (useKeyboard)
            {
                //print("Trying keyboard to controller");
                settings.ScanFlags = Luminosity.IO.ScanFlags.JoystickButton;
                Luminosity.IO.InputManager.StartInputScan(settings, JoystickButtonScan);
            }
            else if (useController)
            {
                //print("Trying controller to keyboard");
                settings.ScanFlags = Luminosity.IO.ScanFlags.Key;
                Luminosity.IO.InputManager.StartInputScan(settings, KeyScan);
            }
        }
    }

    //keyboard scan
    private bool KeyScan(Luminosity.IO.ScanResult result)
    {
        bool keyPressed = false;

        //make sure flag is correct
        if(result.ScanFlags == Luminosity.IO.ScanFlags.Key)
        {
            //check if key was pressed
            if (IsKeyValid(result.Key))
            {
                //switch to keyboard control
                useController = false;
                useKeyboard = true;

                Luminosity.IO.InputManager.SetControlScheme(keyboardSchemeName, Luminosity.IO.PlayerID.One);

                //set pressed to true
                keyPressed = true;                
            }
        }

        return keyPressed;
    }

    //check if key is valid
    private bool IsKeyValid(KeyCode key)
    {
        bool isValid = false;

        if((int)key < (int)KeyCode.JoystickButton0
            && (key != KeyCode.LeftApple || key != KeyCode.RightApple)
            && (key != KeyCode.LeftWindows || key != KeyCode.RightWindows))
        {
            isValid = true;
        }

        return isValid;
    }

    //joystick button scan
    private bool JoystickButtonScan(Luminosity.IO.ScanResult result)
    {
        bool joystickPressed = false;
        //make sure scan flag is correct
        if(result.ScanFlags == Luminosity.IO.ScanFlags.JoystickButton)
        {
            //check if joystick button pressed
            if (IsJoystickButtonValid(result.Key))
            {
                //switch to controller control
                useController = true;
                useKeyboard = false;

                Luminosity.IO.InputManager.SetControlScheme(controllerSchemeName, Luminosity.IO.PlayerID.One);

                //set pressed to true
                joystickPressed = true;
            }
        }

        return joystickPressed;
    }

    //check if joystick button valid
    private bool IsJoystickButtonValid(KeyCode key)
    {
        bool isValid = false;
        //if key pressed is one of joystick buttons
        if((int)key >= (int)KeyCode.Joystick1Button0 && (int)key <= (int)KeyCode.Joystick1Button19)
        {
            isValid = true;
        }

        return isValid;
    }

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    //Pause events
    void OnPause()
    {
        canMove = false;

        //if in town
        //if(inTown){
        //  canTalk = false;
        //}

        //if in dungeon
        //if(inDungeon){
        canShoot = false;
        canAbility = false;
        //}
        print("Paused called");
    }

    void OnUnpause()
    {
        canMove = true;

        //if in town
        //if(inTown){
        //  canTalk = true;
        //}

        //if in dungeon
        //if(inDungeon){
        canShoot = true;
        canAbility = true;
        //}
        print("Unpause called");
    }
}
