using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class GateManager : MonoBehaviour
{

    [Header("Timer Settings")]
    public bool OpenAfterTime = false;
    public float TimerLength = 0f;
    private GameObject CountDownUI;
    private float StartTime = 0f;
    private bool CountDownActive = false;

    [Header("UI Settings")]
    public bool ShowNotification = false;
    public string NotificationMessage = "";
    private GameObject NotificationUI;

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
        if (OpenAfterTime)
        {
            if (CountDownActive)
            {
                CountDownUI.GetComponent<Text>().text = (TimerLength - (Time.time - StartTime)).ToString("F2");
                if (Time.time - StartTime >= TimerLength)
                {
                    KillAllEnemies();
                    RoomCleared();
                }
            }
        }
    }

    //on room activation logic
    private void ActivateRoom()
    {
        //Call functions to activate when the room is entered
        if (FunctionOnEnter != null) FunctionOnEnter.Invoke();
        //If timer based get the countdown ui
        if (OpenAfterTime)
        {
            StartTime = Time.time;

            CountDownUI = SceneHandler.GetSceneHandler().GetCountDownUI();
            CountDownUI.SetActive(true);
            CountDownActive = true;

            if (ShowNotification)
            {
                NotificationUI = SceneHandler.GetSceneHandler().GetNotificationUI();
                NotificationUI.SetActive(true);
                NotificationUI.GetComponent<Text>().text = NotificationMessage;
                NotificationUI.transform.localScale = Vector3.zero;
                Sequence EnterExit = DOTween.Sequence();
                // Add a movement tween at the beginning
                EnterExit.Append(NotificationUI.transform.DOScale(1f, 1f).SetEase(Ease.OutBack));
                // Add a rotation tween as soon as the previous one is finished
                EnterExit.Append(NotificationUI.transform.DOScale(0f, 1f));
                EnterExit.Play();
            }
        }

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
        //Call Functions to activate when a room is cleared
        if (FunctionOnExit != null) FunctionOnExit.Invoke();
        //Set variables to null
        if (CountDownUI)
        {
            CountDownUI.SetActive(false);
            CountDownUI = null;
        }
        if (NotificationUI)
        {
            NotificationUI.transform.localScale = Vector3.one;
            NotificationUI.SetActive(false);
            NotificationUI = null;
        }
        OpenAfterTime = false;
        CountDownActive = false;
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

    //Kill all the enemies
    private void KillAllEnemies()
    {
        for (int i = enemyList.Count - 1; i >= 0; --i)
        {
            if(enemyList[i] != null) enemyList[i].GetComponent<EntityHealth>().Kill(); //If the enemy isn't already dead then kill them
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
