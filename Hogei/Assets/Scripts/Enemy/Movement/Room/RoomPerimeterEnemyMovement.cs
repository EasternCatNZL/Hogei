using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoomPerimeterEnemyMovement : MonoBehaviour {

    //travel speed
    private float travelSpeed = 20.0f;
    private float travelTime = 1.0f;

    //script ref
    private RoomPerimeterMovement roomMovement;

    //control vars
    [HideInInspector]
    public int currentWaypointIndex = 0; //current index in waypoint
    private Vector3 currentDestination; //where the enemy is currently moving
    private float travelStartTime = 0.0f; //time travelling to next point began, needed for pause timing
    private float tempTravelTime = 0.0f; //used when leaving pause
    private bool isPaused = false; //check if game paused

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            CheckArrivedAtWaypoint();
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

    //movement logic between waypoints
    private void MoveToNextWaypoints()
    {
        transform.DOMove(currentDestination, travelTime, false);
        //look at the next waypoint
        transform.rotation = Quaternion.LookRotation(currentDestination - transform.position);
    }

    //checks if arrived at waypoint
    private void CheckArrivedAtWaypoint()
    {
        print(name + " " + currentWaypointIndex);
        //check if arrived at waypoint
        if (transform.position == roomMovement.waypoints[currentWaypointIndex])
        {
            //increment the current index
            currentWaypointIndex++;
            //check not greater than list count
            if(currentWaypointIndex >= roomMovement.waypoints.Count)
            {
                //set current way point back to 0
                currentWaypointIndex = 0;
            }
            //set the current destination to new index
            currentDestination = roomMovement.waypoints[currentWaypointIndex];
            //begin moving towards new destination
            MoveToNextWaypoints();
            travelStartTime = Time.time;
        }
    }

    //set the room perimeter movement ref
    public void SetRoomPeriMoveRef(RoomPerimeterMovement script)
    {
        roomMovement = script;
    }

    //Pause events
    void OnPause()
    {
        isPaused = true;
        //kill tween
        DOTween.Kill(transform);
        //get temp travel time
        tempTravelTime = (travelStartTime + travelTime) - Time.time;
    }

    private void OnUnpause()
    {
        isPaused = false;
        //resume movement
        transform.DOMove(currentDestination, tempTravelTime, false);
        //reset travel start time
        travelStartTime = Time.time;
    }
}
