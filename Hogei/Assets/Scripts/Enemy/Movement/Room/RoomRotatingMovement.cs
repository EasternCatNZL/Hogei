using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRotatingMovement : MonoBehaviour {

    [Header("Minimum Operation Space")]
    public float minimumOperationSpace = 2.0f;

    [Header("Distance")]
    [Tooltip("The distance away from center enemies are")]
    public float distance = 35.0f;
    [Tooltip("the base distance that enemies are away from center")]
    public float baseDistance = 12.0f;

    [Header("Speed")]
    [Tooltip("Speed that enemies rotate at")]
    public float rotationSpeed = 20.0f;

    //control vars
    private bool isPaused = false;

    //enemies ref
    List<GameObject> enemiesList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        enemiesList = GetComponent<RoomEnemyManager>().enemyList;
        SpreadEnemies();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            EnemiesLookCenter();
        }
        
	}

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    //spread out enemies
    private void SpreadEnemies()
    {
        //if operation size exists appropriatly
        if (GetComponent<RoomEnemyManager>().operationSize > 0)
        {
            //scale distance by operational space
            distance = baseDistance * GetComponent<RoomEnemyManager>().operationSize;
        }
        

        float angleApart = 360.0f / enemiesList.Count;
        //for the number of enemies in list
        for (int i = 0; i < enemiesList.Count; i++)
        {
            //get an altered rotation
            Quaternion alteredRotation = new Quaternion();
            alteredRotation.eulerAngles = new Vector3(0, angleApart * i, 0);
            enemiesList[i].transform.rotation = alteredRotation;
            //move enemy forward by specified distance
            enemiesList[i].transform.position = enemiesList[i].transform.forward * distance;
        }
    }

    //rotate all enemies to look at center
    private void EnemiesLookCenter()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].transform.LookAt(transform.position);
        }
    }

    //Pause events
    void OnPause()
    {
        isPaused = true;

    }

    private void OnUnpause()
    {
        isPaused = false;

    }
}
