using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnFrontCannon : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 15.0f;
    [Tooltip("Number of arcs")]
    public int numArcs = 2;

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 5.0f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 4.0f;

    //control vars

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
