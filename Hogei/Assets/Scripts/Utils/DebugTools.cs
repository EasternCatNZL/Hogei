using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugTools : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";
    [Tooltip("Scene handler tag")]
    public string sceneTag = "Scene";
    [Tooltip("Enemy tag")]
    public string enemyTag = "Enemy";

    [Header("Key codes")]
    [Tooltip("Key to toggle debug tools")]
    public KeyCode debugToolToggleKey = KeyCode.F1;
    [Tooltip("Key to toggle player invincibility")]
    public KeyCode invincibilityToggleKey = KeyCode.F2;
    [Tooltip("Key to refill health")]
    public KeyCode healthRefillKey = KeyCode.F3;
    [Tooltip("Key to reload scene")]
    public KeyCode sceneReloadKey = KeyCode.F4;
    [Tooltip("Key to respawn enemies")]
    public KeyCode enemyRespawnKey = KeyCode.F5;
    [Tooltip("Key to change player attacks to instakill")]
    public KeyCode instakillToggleKey = KeyCode.F6;

    //Object refs
    private GameObject player; //ref to player

    //script refs
    private EntityHealth playerEntityHealth; //the entity health attached to the player object
    private SceneHandler sceneHandler; //scene hanlder of the current scene

    //control vars
    public bool debugToolsOn = false; //checks if debug tools are being used
    public bool invincibilityOn = false; //check if currently invincible
    public bool instakillOn = false; //check if player attacks should instakill

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

        sceneHandler = GameObject.FindGameObjectWithTag(sceneTag).GetComponent<SceneHandler>();
    }

    //Toggle for debug
    private void ToggleDebug()
    {
        if (Input.GetKeyDown(debugToolToggleKey))
        {
            if (debugToolsOn)
            {
                debugToolsOn = false;

                invincibilityOn = false;
                //activate players entity health script
                playerEntityHealth.enabled = true;

                instakillOn = false;
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
            ToggleInvincibility();
        }

        //Health refill
        if (Input.GetKeyDown(healthRefillKey))
        {
            RefillHealth();
        }

        //scene reload
        if (Input.GetKeyDown(sceneReloadKey))
        {
            ReloadScene();
        }

        //enemy respawn
        if (Input.GetKeyDown(enemyRespawnKey))
        {
            RespwanEnemies();
        }

        //toggle instakill
        if (Input.GetKeyDown(instakillToggleKey))
        {
            ToggleInstakill();
        }
    }


    //Toggle for invincibility
    private void ToggleInvincibility()
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

    //Refill health
    private void RefillHealth()
    {
        playerEntityHealth.IncreaseHealth(playerEntityHealth.MaxHealth);
    }

    //Scene reload
    private void ReloadScene()
    {
        //find the current scene manager and get current scene number
        int thisScene = sceneHandler.sceneNumber;
        //load this scene again
        SceneManager.LoadScene(thisScene);
    }

    //respawn the enemies
    private void RespwanEnemies()
    {
        //destroy all enemies currently in the scene
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag(enemyTag);
        for(int i = 0; i < enemiesInScene.Length; i++)
        {
            Destroy(enemiesInScene[i]);
        }
        //Re-Instantiate all enemies into the scene using scene handlers list of enemies
        for (int j = 0; j < sceneHandler.enemiesInSceneList.Count; j++)
        {
            Instantiate(sceneHandler.enemiesInSceneList[j]);
        }
    }

    //Toggle instakill
    private void ToggleInstakill()
    {
        if (instakillOn)
        {
            instakillOn = false;
        }
        else
        {
            instakillOn = true;
        }
    }
}
