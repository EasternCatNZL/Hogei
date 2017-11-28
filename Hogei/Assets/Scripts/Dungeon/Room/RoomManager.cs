using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    [Header("Enemy group")]
    [Tooltip("Object holding enemy group")]
    public GameObject enemyGroupObject;

    [Header("Doors")]
    [Tooltip("Array of doors to this room")]
    public GameObject[] doorsArray = new GameObject[0];

    [Header("Tags")]
    public string playerTag = "Player";

    //control vars
    private bool isActivated = false; //checks if room has been activated
    private bool isCleared = false; //checks if room has been flagged as cleared

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //on room activation logic
    private void ActivateRoom()
    {
        //close doors
        CloseDoors();
        //spawn enemy group
        GameObject enemyGroupObjectClone = Instantiate(enemyGroupObject, transform.position, transform.rotation);
        //make relation to enemy group
        enemyGroupObjectClone.GetComponent<RoomEnemyManager>().SetRoomManager(this);
    }

    //Called on room clear
    public void RoomCleared()
    {
        //set to cleared
        isCleared = true;
        //open the doors
        OpenDoors();
    }

    //close the doors
    private void CloseDoors()
    {
        //Set doors to block path
        //For all doors
        for (int i = 0; i < doorsArray.Length; i++)
        {
            //set door to active
            doorsArray[i].SetActive(true);
        }

        //TODO: Animation for doors that would close off path
    }

    //open the doors
    private void OpenDoors()
    {
        //Set doors to block path
        //For all doors
        for (int i = 0; i < doorsArray.Length; i++)
        {
            //set door to active
            doorsArray[i].SetActive(false);
        }

        //TODO: Animation for doors that would open path
    }

    //on entry
    private void OnTriggerEnter(Collider other)
    {
        //only if room hasnt been activated
        if (!isActivated)
        {
            //check if other is player
            if (other.gameObject.CompareTag(playerTag))
            {
                //activate room
                isActivated = true;
                ActivateRoom();
            }
        }
    }
}
