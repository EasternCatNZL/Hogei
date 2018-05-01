using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour {

 //   [Header("Timing vars")]
 //   [Tooltip("Delay before player is transported out after death")]
 //   public float exitDelayDeath = 2.0f;

 //   [Header("Prefabs")]
 //   public GameObject Camera = null;
 //   public GameObject Player = null;
 //   public GameObject PlayerUI = null;

 //   [Header("Tags")]
 //   public string playerTag = "Player";
 //   public string enemyTag = "Enemy";

 //   //control vars
 //   //[HideInInspector]
 //   public int currentFloor = 1;
 //   private float delayStartTime = 0;
 //   private bool isExiting = false;

 //   //object refs
 //   private GameObject player;
 //   private GameObject cam;

 //   //script refs
 //   private EntityHealth playerHealth;
 //   private DungeonToTown dungeonToTown;
 //   private WhatCanIDO canDo;

	//// Use this for initialization
	//void Start () {
 //       //if player already exists <- bringing from town scene
 //       if (GameObject.FindGameObjectWithTag(playerTag))
 //       {
 //           player = GameObject.FindGameObjectWithTag(playerTag);
 //           //make a camera
 //           cam = Instantiate(Camera, Vector3.zero, Quaternion.identity);
 //           cam.GetComponent<ARPGCamera>().TrackingTarget = player.transform;
 //       }
 //       SpawnPlayer();
 //       dungeonToTown = GetComponent<DungeonToTown>();
 //   }
	
	//// Update is called once per frame
	//void Update () {

 //       if(!playerHealth)
 //       {
 //           playerHealth = GameObject.FindGameObjectWithTag(playerTag).GetComponent<EntityHealth>();
 //           canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();
 //       }
 //       else
 //       {
 //           CheckPlayerHealth();
 //       }

 //       if (isExiting)
 //       {
 //           if (Time.time > delayStartTime + exitDelayDeath)
 //           {
 //               //move to town
 //               dungeonToTown.MoveToTown();
 //               //once moved, allow movement again
 //               canDo.canMove = true;
 //           }
 //       }

	//}

 //   public void SpawnPlayer()
 //   {
 //       Vector3 SpawnPoint = GetComponent<DungeonGenerator>().GetPlayerSpawn();
 //       //if player does not exist
 //       if (!GameObject.FindGameObjectWithTag(playerTag))
 //       {
 //           player = Instantiate(Player, SpawnPoint, Quaternion.identity);
 //           cam = Instantiate(Camera, SpawnPoint, Quaternion.identity);
 //           cam.GetComponent<ARPGCamera>().TrackingTarget = player.transform;
 //           Instantiate(PlayerUI, Vector3.zero, Quaternion.identity);
 //       }
 //       else
 //       {
 //           player.transform.position = SpawnPoint;
 //           player.transform.rotation = Quaternion.identity;
 //           Instantiate(PlayerUI, Vector3.zero, Quaternion.identity);
 //       }
        
 //   }

 //   public void PlayerDead()
 //   {

 //   }

 //   //track player health, and when at 0, prepare to move player out
 //   private void CheckPlayerHealth()
 //   {
 //       //if health below 0
 //       if (playerHealth.CurrentHealth <= 0)
 //       {
 //           //delay start time is now
 //           delayStartTime = Time.time;
 //           //set leave to true
 //           isExiting = true;
 //           //prevent player from acting
 //           canDo.canMove = false;
 //           canDo.canShoot = false;
 //           canDo.canAbility = false;
 //           SceneManager.LoadScene(1);
 //       }
 //   }

 //   //Move to next floor in dungeon
 //   public void MoveToNextFloor()
 //   {
 //       //stop the players actions
 //       canDo.TurnAllOff();
 //       //increment floor
 //       currentFloor++;
 //       //find all enemies still in scene
 //       GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
 //       //destroy all the objects
 //       for (int i = 0; i < enemies.Length; i++)
 //       {
 //           Destroy(enemies[i]);
 //       }
 //       //destroy the current dungeon
 //       GetComponent<DungeonGenerator>().DeleteDungeon();
 //       //create a new dungeon
 //       GetComponent<DungeonGenerator>().GenerateDungeon();
 //       //place player into dungeon
 //       SpawnPlayer();
 //       //turn all players actions back on
 //       canDo.TurnAllOn();
 //   }
}
