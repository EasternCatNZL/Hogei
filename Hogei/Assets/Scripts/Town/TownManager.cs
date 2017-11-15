using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour {

    [Header("Player object")]
    [Tooltip("The player object")]
    public GameObject player;

    [Header("Player spawn point")]
    public Transform playerSpawn;

    [Header("Tags")]
    public string playerTag = "Player";

	// Use this for initialization
	void Start () {
        //if player not yet in scene, spawn one in
        if (!GameObject.FindGameObjectWithTag(playerTag))
        {
            Instantiate(player, playerSpawn.position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
