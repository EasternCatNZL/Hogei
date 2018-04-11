using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnSheepRumble : MonoBehaviour {

    [Header("Sheep object")]
    [Tooltip("Sheep object")]
    public GameObject sheepObject;
    [Tooltip("Num of sheep spawned per")]
    public int numSheepSpawnedPer = 3;

    [Header("Timing vars")]
    [Tooltip("Time between launches")]
    public float timeBetweenLaunches = 0.2f;

    [Header("Attack vars")]
    [Tooltip("Sheep max speed")]
    public float maxChargeSpeed = 20.0f;
    [Tooltip("Sheep min speed")]
    public float minChargeSpeed = 10.0f;

    [Header("Angle control")]
    [Tooltip("Angle variance")]
    public float angleVariance = 30.0f;

    //control vars
    public bool isUsing = false; //use this attack

    private float lastAttackTime = 0.0f; //time of last attack

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (isUsing)
        {
            if(Time.time > lastAttackTime + timeBetweenLaunches)
            {
                SheepLaunch();
            }
        }
	}

    //Sheep launch
    private void SheepLaunch()
    {
        //set timing
        lastAttackTime = Time.time;
        //for the number of sheep per
        for(int i = 0; i < numSheepSpawnedPer; i++)
        {
            //create a sheep with random angle 
            GameObject sheepClone = Instantiate(sheepObject, transform.position, Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + Random.Range(-angleVariance, angleVariance), 0.0f));
            //set up vars on sheep behavior
            SheepBehaviour sheepBehave = sheepClone.GetComponent<SheepBehaviour>();
            sheepBehave.doTrack = false;
            sheepBehave.chargeSpeed = Random.Range(minChargeSpeed, maxChargeSpeed);
            sheepBehave.isActive = true;
            sheepClone.GetComponent<EntityHealth>().CurrentHealth = 1;
        }
    }
}
