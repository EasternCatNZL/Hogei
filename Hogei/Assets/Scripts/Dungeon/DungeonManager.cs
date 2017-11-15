using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Delay before player is transported out after death")]
    public float exitDelayDeath = 2.0f;

    [Header("Prefabs")]
    public GameObject Camera = null;
    public GameObject Player = null;

    [Header("Tags")]
    public string playerTag = "Player";

    //control vars
    [HideInInspector]
    public int currentFloor = 0;
    private float delayStartTime = 0;
    private bool isExiting = false;

    //script refs
    private EntityHealth playerHealth;
    private DungeonToTown dungeonToTown;
    private WhatCanIDO canDo;

	// Use this for initialization
	void Start () {
        SpawnPlayer();
        dungeonToTown = GetComponent<DungeonToTown>();
    }
	
	// Update is called once per frame
	void Update () {

        if(!playerHealth)
        {
            playerHealth = GameObject.FindGameObjectWithTag(playerTag).GetComponent<EntityHealth>();
            canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
        }
        else
        {
            CheckPlayerHealth();
        }

        if (isExiting)
        {
            if (Time.time > delayStartTime + exitDelayDeath)
            {
                //move to town
                dungeonToTown.MoveToTown();
                //once moved, allow movement again
                canDo.canMove = true;
            }
        }

	}

    public void SpawnPlayer()
    {
        Vector3 SpawnPoint = GetComponent<DungeonGenerator>().GetPlayerSpawn();
        GameObject player = Instantiate(Player, SpawnPoint, Quaternion.identity);
        GameObject cam = Instantiate(Camera, SpawnPoint, Quaternion.identity);
        cam.GetComponent<ARPGCamera>().TrackingTarget = player.transform;
    }

    //track player health, and when at 0, prepare to move player out
    private void CheckPlayerHealth()
    {
        //if health below 0
        if (playerHealth.CurrentHealth <= 0)
        {
            //delay start time is now
            delayStartTime = Time.time;
            //set leave to true
            isExiting = true;
            //prevent player from acting
            canDo.canMove = false;
            canDo.canShoot = false;
            canDo.canAbility = false;
        }
    }
}
