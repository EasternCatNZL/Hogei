using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PassageEnemyConstantMove : MonoBehaviour {

    [Header("Movement")]
    [Tooltip("The travel speed of unit")]
    public float travelSpeed = 5.0f;

    //script ref
    private EnemyWaypointManager waypointManager;

    //control vars
    private int currentWaypointIndex = 0; //current index in waypoint
    float timeToTravel = 0.0f;
    private float travelStartTime = 0.0f; //time traveling to this waypoint started
    private float tempTravelTime = 0.0f; //temp travel time needed to handle tween when resuming from pause

    private bool isPaused = false; //checks if game is paused

    private Transform currentDestination; //where the enemy is currently moving

	// Use this for initialization
	void Start () {
        waypointManager = GetComponent<EnemyWaypointManager>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckArrivedAtWaypoint();
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

    //movement logic between waypoints
    private void MoveToNextWaypoints()
    {
        //get time variable that is somewhat consistent over distance
        timeToTravel = (Vector3.Distance(waypointManager.waypointList[currentWaypointIndex].position, waypointManager.waypointList[currentWaypointIndex - 1].position)) / travelSpeed;
        //tween to next destination over this amount of time
        transform.DOMove(currentDestination.position, timeToTravel, false);
        //look at the next waypoint
        transform.rotation = Quaternion.LookRotation(currentDestination.position - transform.position);
    }

    //checks if arrived at waypoint
    private void CheckArrivedAtWaypoint()
    {
        //check if arrived at waypoint
        if(transform.position == waypointManager.waypointList[currentWaypointIndex].position)
        {
            //increment the current index
            currentWaypointIndex++;
            //set the current destination to new index
            currentDestination = waypointManager.waypointList[currentWaypointIndex];
            //begin moving towards new destination
            MoveToNextWaypoints();
            travelStartTime = Time.time;
        }
    }

    //Pause events
    void OnPause()
    {
        isPaused = true;
        //kill the tween
        DOTween.Kill(transform);
        //get temp travel time
        tempTravelTime = (travelStartTime + timeToTravel) - Time.time;
    }

    void OnUnpause()
    {
        isPaused = false;
        //resume movement
        transform.DOMove(currentDestination.position, tempTravelTime, false);
        //reset travel start time
        travelStartTime = Time.time;
    }
}
