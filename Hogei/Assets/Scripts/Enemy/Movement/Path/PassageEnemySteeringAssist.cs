using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageEnemySteeringAssist : MonoBehaviour {

    [Header("Waypoint #")]
    [Tooltip("This waypoints number, as a failsafe")]
    public int waypointNum = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //if other has steering move
        if (other.gameObject.GetComponent<PassageEnemySteeringMove>())
        {
            //if other is moving towards this waypoint
            if (other.gameObject.GetComponent<PassageEnemySteeringMove>().currentWaypointIndex == waypointNum)
            {
                //set to next destination
                other.gameObject.GetComponent<PassageEnemySteeringMove>().ArrivedAtWaypoint();
            }
        }
    }
}
