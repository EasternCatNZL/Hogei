using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerShot : MonoBehaviour {

    public GameObject trajectoryPointPrefab;
    public GameObject ballPrefab;
    public float shotPower = 0.0f;

    private GameObject balll;
    private bool isPressed = false;
    private bool ballTown = false;
    private int numOfTrajectoryPoints = 30;
    private List<GameObject> trajectoryPoints;

    
	// Use this for initialization
	void Start () {
        trajectoryPoints = new List<GameObject>();
        for(int i = 0; i < numOfTrajectoryPoints; i++)
        {
                        
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
