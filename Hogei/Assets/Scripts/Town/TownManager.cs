using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour {

    [Header("Player object")]
    [Tooltip("The player object")]
    public GameObject playerObject;

    [Header("Camera object")]
    [Tooltip("The camera object")]
    public GameObject cameraObject;

    [Header("Player spawn point")]
    public Transform playerSpawn;

    [Header("Tags")]
    public string playerTag = "Player";

    //object refs
    private GameObject player;
    private GameObject cam;

    // Use this for initialization
    void Start () {
        //if player not yet in scene, spawn one in
        if (!GameObject.FindGameObjectWithTag(playerTag))
        {
            player = Instantiate(playerObject, playerSpawn.position, Quaternion.identity);
            player.GetComponent<WhatCanIDO>().canShoot = false;
            player.GetComponent<WhatCanIDO>().canAbility = false;
            cam = Instantiate(cameraObject, Vector3.zero, Quaternion.identity);
            cam.GetComponent<ARPGCamera>().TrackingTarget = player.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
