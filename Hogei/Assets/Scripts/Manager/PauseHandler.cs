﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour {

    [Header("Key inputs")]
    public KeyCode pauseKey = KeyCode.Escape;

    public delegate void PauseDelegate();
    public static event PauseDelegate PauseEvent;
    public static event PauseDelegate UnpauseEvent;
    [HideInInspector]
    public static bool isPaused = false;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        PauseMenuPressed();
	}

    //When the pause menu key is pressed
    private void PauseMenuPressed()
    {
        //if pause pressed
        if (Input.GetKeyDown(pauseKey))
        {
            //check if currently paused
            if (isPaused)
            {
                //call unpause event
                UnpauseEvent();
                isPaused = false;
            }
            //else pause
            else
            {
                //call pause event
                PauseEvent();
                isPaused = true;
            }
        }
    }
}
