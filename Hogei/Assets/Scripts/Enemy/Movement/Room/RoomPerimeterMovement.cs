using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPerimeterMovement : MonoBehaviour {

    public GameObject testBall;

    [Header("Minimum Operation Space")]
    public float minimumOperationSpace = 2.0f;

    [Header("Diameter")]
    [Tooltip("Distance from center to side of square area")]
    public float distanceToSide = 30.0f;
    [Tooltip("the base distance that enemies are away from center")]
    public float baseDistance = 6.0f;

    [Header("Timing")]
    [Tooltip("Time that enemies move from point to point")]
    public float moveTime = 20.0f;

    //waypoints list
    [HideInInspector]
    public List<Vector3> waypoints = new List<Vector3>();

    //enemies ref
    List<GameObject> enemiesList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        enemiesList = GetComponent<RoomEnemyManager>().enemyList;
        SetUpWaypoints();
        SetUpEnemies();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Set up the waypoints from center
    private void SetUpWaypoints()
    {
        //if operation size exists appropriatly
        if (GetComponent<RoomEnemyManager>().operationSize > 0)
        {
            //scale distance by operational space
            distanceToSide = baseDistance * GetComponent<RoomEnemyManager>().operationSize;
        }

        //north 1
        Vector3 n = transform.forward * distanceToSide;
        waypoints.Add(n);
        //north north east 2
        Vector3 nne = (transform.forward * distanceToSide) + (transform.right * (distanceToSide / 2));
        waypoints.Add(nne);
        //north east 3
        Vector3 ne = (transform.forward * distanceToSide) + (transform.right * distanceToSide);
        waypoints.Add(ne);
        //east north east 4 
        Vector3 ene = (transform.forward * (distanceToSide / 2)) + (transform.right * distanceToSide);
        waypoints.Add(ene);
        //east 5
        Vector3 e = transform.right * distanceToSide;
        waypoints.Add(e);
        //east south east 6
        Vector3 ese = (-transform.forward * (distanceToSide / 2)) + (transform.right * distanceToSide);
        waypoints.Add(ese);
        //south east 7
        Vector3 se = (-transform.forward * distanceToSide) + (transform.right * distanceToSide);
        waypoints.Add(se);
        //south south east 8
        Vector3 sse = (-transform.forward * distanceToSide) + (transform.right * (distanceToSide / 2));
        waypoints.Add(sse);
        //south 9
        Vector3 s = -transform.forward * distanceToSide;
        waypoints.Add(s);
        //south south west 10
        Vector3 ssw = (-transform.forward * distanceToSide) + (-transform.right * (distanceToSide / 2));
        waypoints.Add(ssw);
        //south west 11
        Vector3 sw = (-transform.forward * distanceToSide) + (-transform.right * distanceToSide);
        waypoints.Add(sw);
        //west south west 12
        Vector3 wsw = (-transform.forward * (distanceToSide / 2)) + (-transform.right * distanceToSide);
        waypoints.Add(wsw);
        //west 13
        Vector3 w = -transform.right * distanceToSide;
        waypoints.Add(w);
        //west north west 14
        Vector3 wnw = (transform.forward * (distanceToSide / 2)) + (-transform.right * distanceToSide);
        waypoints.Add(wnw);
        //north west 15
        Vector3 nw = (transform.forward * distanceToSide) + (-transform.right * distanceToSide);
        waypoints.Add(nw);
        //north north west 16
        Vector3 nnw = (transform.forward * distanceToSide) + (-transform.right * (distanceToSide / 2));
        waypoints.Add(nnw);

        Instantiate(testBall, n, Quaternion.identity);
        Instantiate(testBall, nne, Quaternion.identity);
        Instantiate(testBall, ne, Quaternion.identity);
        Instantiate(testBall, ene, Quaternion.identity);
        Instantiate(testBall, e, Quaternion.identity);
        Instantiate(testBall, ese, Quaternion.identity);
        Instantiate(testBall, se, Quaternion.identity);
        Instantiate(testBall, sse, Quaternion.identity);
        Instantiate(testBall, s, Quaternion.identity);
        Instantiate(testBall, ssw, Quaternion.identity);
        Instantiate(testBall, sw, Quaternion.identity);
        Instantiate(testBall, wsw, Quaternion.identity);
        Instantiate(testBall, w, Quaternion.identity);
        Instantiate(testBall, wnw, Quaternion.identity);
        Instantiate(testBall, nw, Quaternion.identity);
        Instantiate(testBall, nnw, Quaternion.identity);
    }

    //setup the enemies
    private void SetUpEnemies()
    {
        //get the number of enemies
        int numEnemies = enemiesList.Count;
        print(numEnemies);
        //get how far to space enemies apart
        int space = waypoints.Count / numEnemies;
        print(space);
        //for all the enemies, space them onto waypoints and setup movement
        for (int i = 0; i < numEnemies; i++)
        {
            //move to waypoint
            enemiesList[i].transform.position = waypoints[i * space];
            //set script ref
            enemiesList[i].GetComponent<RoomPerimeterEnemyMovement>().SetRoomPeriMoveRef(this);
            //set current waypoint
            enemiesList[i].GetComponent<RoomPerimeterEnemyMovement>().currentWaypointIndex = i * space;
        }
    }
}
