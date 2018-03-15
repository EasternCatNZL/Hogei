using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{

    [Header("Enemies")]
    [Tooltip("Enemies required to defeat to pass the gate")]
    public GameObject[] EnemyArray = new GameObject[0];
    [Header("Doors")]
    [Tooltip("Array of gates to this room")]
    public GameObject[] gateArray = new GameObject[0];

    [Header("Tags")]
    public string playerTag = "Player";

    //control vars
    private bool isActivated = false; //checks if room has been activated
    private bool isCleared = false; //checks if room has been flagged as cleared

    private void OnEnable()
    {
        EntityHealth.OnDeath += CheckRoomCleared;
    }

    private void OnDisable()
    {
        EntityHealth.OnDeath -= CheckRoomCleared;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //on room activation logic
    private void ActivateRoom()
    {
        //close doors
        CloseDoors();
    }

    void CheckRoomCleared()
    {
        if (isActivated)
        {
            bool EnemiesCleared = true;
            for (int i = 0; i < EnemyArray.Length; ++i)
            {
                if (EnemyArray[i] != null && EnemyArray[i].activeSelf == true)
                {
                    Debug.Log("Enemies are not cleared");
                    EnemiesCleared = false;
                    break;
                }
            }
            if (EnemiesCleared)
            {
                Debug.Log("Enemies cleared");
                RoomCleared();
            }
        }
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
        Debug.Log("Closing doors");
        //Set doors to block path
        //For all doors
        for (int i = 0; i < gateArray.Length; i++)
        {
            //set door to active
            gateArray[i].GetComponent<GateController>().LockGate();
        }

        //TODO: Animation for doors that would close off path
    }

    //open the doors
    private void OpenDoors()
    {
        Debug.Log("Opening doors");
        //Set doors to block path
        //For all doors
        for (int i = 0; i < gateArray.Length; i++)
        {
            //set door to active
            gateArray[i].GetComponent<GateController>().UnlockGate();
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
