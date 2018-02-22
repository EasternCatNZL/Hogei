using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";

    [Header("Key codes")]
    [Tooltip("Key to toggle debug tools")]
    public KeyCode debugToolToggleKey = KeyCode.F1;
    [Tooltip("Key to toggle player invincibility")]
    public KeyCode invincibilityToggleKey = KeyCode.F2;
    [Tooltip("Key to refill health")]
    public KeyCode healthRefillKey = KeyCode.F3;

    //Object refs
    private GameObject player; //ref to player

    //script refs
    private EntityHealth playerEntityHealth; //the entity health attached to the player object

    //control vars
    public bool debugToolsOn = false; //checks if debug tools are being used
    public bool invincibilityOn = false; //check if currently invincible

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ToggleDebug();
        if (debugToolsOn)
        {
            DebugFuncs();
        }
	}

    //Checks components and searches if null
    void FindComponents()
    {
        //check if player ref exists
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag(playerTag))
            {
                player = GameObject.FindGameObjectWithTag(playerTag);
            }
        }

        //if player ref exists, check for components of player that refs are needed
        if (player)
        {
            if (!playerEntityHealth)
            {
                if (player.GetComponent<EntityHealth>())
                {
                    playerEntityHealth = player.GetComponent<EntityHealth>();
                }
            }
        }
    }

    //Toggle for debug
    private void ToggleDebug()
    {
        if (Input.GetKeyDown(debugToolToggleKey))
        {
            if (debugToolsOn)
            {
                debugToolsOn = false;
            }
            else
            {
                debugToolsOn = true;
                //when turning on debug tools, check for components
                FindComponents();
            }
        }
    }

    private void DebugFuncs()
    {
        //Invincibility
        if (Input.GetKeyDown(invincibilityToggleKey))
        {
            if (invincibilityOn)
            {
                invincibilityOn = false;
                playerEntityHealth.enabled = true;
            }
            else
            {
                invincibilityOn = true;
                playerEntityHealth.enabled = false;
            }
        }


    }


    //Toggle for invincibility
    private void ToggleInvincibility()
    {
        if (Input.GetKeyDown(invincibilityToggleKey))
        {
            if (invincibilityOn)
            {
                invincibilityOn = false;
                //activate players entity health script
                playerEntityHealth.enabled = true;
            }
            else
            {
                invincibilityOn = true;
                //deactivate players entity health script
                playerEntityHealth.enabled = false;
            }
        }
    }

    //Refill health
    private void RefillHealth()
    {
        playerEntityHealth.IncreaseHealth(playerEntityHealth.MaxHealth);
    }
}
