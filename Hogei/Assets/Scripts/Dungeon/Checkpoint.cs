using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [Header("Respawn location")]
    [Tooltip("The location player returns to")]
    public Transform respawnLocation;

    [Header("Tags")]
    public string playerTag = "Player";

    //object refs
    private GameObject playerClone;

    //control vars
    private bool isActivated = false; //checks if this checkpoint has already been activated

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //on entering trigger
    private void OnTriggerEnter(Collider other)
    {
        //if yet to be activated
        if (isActivated)
        {
            //check other is player
            if (other.gameObject.CompareTag(playerTag))
            {
                //create a clone of the player object
                playerClone = Instantiate(other.gameObject);
                //turn off copy
                playerClone.SetActive(false);
                //set checkpoint to activated
                isActivated = true;
            }
        }
    }
}
