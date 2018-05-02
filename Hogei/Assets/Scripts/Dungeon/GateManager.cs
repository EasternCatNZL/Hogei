using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateManager : MonoBehaviour
{

    [Header("Timer Settings")]
    public bool OpenAfterTime = false;
    public float TimerLength = 0f;
    private float StartTime = 0f;
    [Header("Enemies")]
    [Tooltip("Enemies required to defeat to pass the gate")]
    public List<GameObject> enemyList = new List<GameObject>();
    [Header("Doors")]
    [Tooltip("Array of gates to this room")]
    public GameObject[] gateArray = new GameObject[0];

    [Header("Functions")]
    public UnityEvent FunctionOnEnter;
    public UnityEvent FunctionOnExit;


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
        if(OpenAfterTime)
        {
            if(Time.time - StartTime >= TimerLength)
            {
                RoomCleared();
                OpenAfterTime = false;
            }
        }
    }

    //on room activation logic
    private void ActivateRoom()
    {
        if(FunctionOnEnter != null) FunctionOnEnter.Invoke();
        isActivated = true;
        //close doors
        CloseDoors();
    }

    void CheckRoomCleared()
    {
        if (isActivated && !isCleared)
        {
            bool EnemiesCleared = true;
            for (int i = 0; i < enemyList.Count; ++i)
            {
                if (enemyList[i] != null && enemyList[i].activeSelf == true)
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
        if (FunctionOnExit != null) FunctionOnExit.Invoke();
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
                ActivateRoom();
            }
        }
    }
}
