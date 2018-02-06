using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatCanIDO : MonoBehaviour {

    [Header("Control bools")]
    public bool canMove = false;
    public bool canShoot = false;
    public bool canAbility = false;
    public bool canTalk = false;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        //if (PauseHandler.isPaused){

        //}
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
