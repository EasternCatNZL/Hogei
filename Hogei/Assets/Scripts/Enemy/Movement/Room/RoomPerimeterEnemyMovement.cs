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

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        CheckArrivedAtWaypoint();
    }

    //movement logic between waypoints
    private void MoveToNextWaypoints()
    {
        ////get previous waypoint num, if at 0, get last in count
        //int prevWaypoint = 0;
        //if (currentWaypointIndex == 0)
        //{
        //    prevWaypoint = roomMovement.waypoints.Count - 1;
        //}
        ////get time variable that is somewhat consistent over distance
        //float timeToTravel = (Vector3.Distance(roomMovement.waypoints[currentWaypointIndex], roomMovement.waypoints[prevWaypoint])) / travelSpeed;
        //tween to next destination over this amount of time
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
        }
    }

    //set the room perimeter movement ref
    public void SetRoomPeriMoveRef(RoomPerimeterMovement script)
    {
        roomMovement = script;
    }
}
