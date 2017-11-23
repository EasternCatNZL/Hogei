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
    private Transform currentDestination; //where the enemy is currently moving

	// Use this for initialization
	void Start () {
        waypointManager = GetComponent<EnemyWaypointManager>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckArrivedAtWaypoint();
	}

    //movement logic between waypoints
    private void MoveToNextWaypoints()
    {
        //get time variable that is somewhat consistent over distance
        float timeToTravel = (Vector3.Distance(waypointManager.waypointList[currentWaypointIndex].position, waypointManager.waypointList[currentWaypointIndex - 1].position)) / travelSpeed;
        //tween to next destination over this amount of time
        transform.DOMove(currentDestination.position, timeToTravel, false);
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
        }
    }
}
