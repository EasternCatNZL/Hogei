﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemyManager : MonoBehaviour {

    //[Header("Enemy group object")]
    //[Tooltip("The enemy group object")]
    //public GameObject enemyGroup;

    [Header("Enemy List")]
    [Tooltip("List of enemy objects")]
    public List<GameObject> enemyList = new List<GameObject>();

    //script ref
    private RoomManager roomManager;

    //control vars
    private bool enemiesCleared = false; //check if enemies in room have been cleared

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!enemiesCleared)
        {
            if (enemyList.Count == 0)
            {
                enemiesCleared = true;
                roomManager.RoomCleared();
            }
        }
		
	}

    //set room manager ref
    public void SetRoomManager(RoomManager manager)
    {
        roomManager = manager;
    }
}