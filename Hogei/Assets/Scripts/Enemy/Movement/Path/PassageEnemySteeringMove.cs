using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageEnemySteeringMove : MonoBehaviour {

    [Header("Movement")]
    [Tooltip("The travel speed of unit")]
    public float travelSpeed = 5.0f;

    //script ref
    private EnemyWaypointManager waypointManager;

    //control vars
    [HideInInspector]
    public int currentWaypointIndex = 0; //current index in waypoint
    private Transform currentDestination; //where the enemy is currently moving
    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        waypointManager = GetComponent<EnemyWaypointManager>();
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckArrivedAtWaypoint();
        if (currentDestination)
        {
            SteerToDestination();
        }
    }

    ////movement logic between waypoints
    //private void MoveToNextWaypoints()
    //{
    //    //get time variable that is somewhat consistent over distance
    //    float timeToTravel = (Vector3.Distance(waypointManager.waypointList[currentWaypointIndex].position, waypointManager.waypointList[currentWaypointIndex - 1].position)) / travelSpeed;
    //    //tween to next destination over this amount of time
    //    transform.DOMove(currentDestination.position, timeToTravel, false);
    //    //look at the next waypoint
    //    transform.rotation = Quaternion.LookRotation(currentDestination.position - transform.position);
    //}

    //steer to destination
    private void SteerToDestination()
    {
        //get vector towards target
        Vector3 desireVelocity = currentDestination.position - transform.position;
        float distance = desireVelocity.magnitude;
        desireVelocity = Vector3.Normalize(desireVelocity) * travelSpeed;
        //get steering force
        Vector3 steeringForce = desireVelocity - myRigid.velocity;
        //adjust velocity
        myRigid.velocity = Vector3.ClampMagnitude(myRigid.velocity + (steeringForce * Time.deltaTime), travelSpeed);
        //face movement direction
        transform.rotation = Quaternion.LookRotation(myRigid.velocity);
    }

    //checks if arrived at waypoint
    private void CheckArrivedAtWaypoint()
    {
        //check if arrived at waypoint
        if (transform.position == waypointManager.waypointList[currentWaypointIndex].position)
        {
            //increment the current index
            currentWaypointIndex++;
            //set the current destination to new index
            currentDestination = waypointManager.waypointList[currentWaypointIndex];
        }
    }

    //called by assistent to redirect
    public void ArrivedAtWaypoint()
    {
        //increment the current index
        currentWaypointIndex++;
        //set the current destination to new index
        currentDestination = waypointManager.waypointList[currentWaypointIndex];
    }
}
