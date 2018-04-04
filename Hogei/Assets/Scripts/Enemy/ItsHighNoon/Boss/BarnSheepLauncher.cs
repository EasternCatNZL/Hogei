using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnSheepLauncher : MonoBehaviour {

    [Header("Sheep vars")]
    [Tooltip("Sheep object")]
    public GameObject sheepObject;
    [Tooltip("Number of sheep per")]
    public int numSheepPer = 7;

    [Header("Timing vars")]
    [Tooltip("Time between sheep")]
    public float timeBetweenSheep = 0.1f;
    [Tooltip("Time between launches")]
    public float timeBetweenLaunches = 4.0f;

    [Header("Launch vars")]
    [Tooltip("Launch speed")]
    public float launchSpeed = 20.0f;
    [Tooltip("Drop speed")]
    public float dropSpeed = 10.0f;
    [Tooltip("Drop height")]
    public float dropHeight = 10.0f;
    [Tooltip("Radius of launch area")]
    public float launchAreaRadius = 2.0f;
    [Tooltip("Stage center")]
    public Vector3 dropCenter = Vector3.zero;
    [Tooltip("Drop radius")]
    public float dropRadius = 25.0f;

    [Header("Object refs")]
    [Tooltip("Crosshair object")]
    public GameObject crosshair;

    //control vars
    //[HideInInspector]
    public bool isUsing = false;

    private bool isLanuching = false; //check to see if currently launching sheep

    private int currentSheepIndex = 0; //the current sheep that has been launched

    private float lastSheepTime = 0.0f; //time last sheep launched
    private float launchStartTime = 0.0f; //time launches began

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isUsing)
        {
            if (isLanuching)
            {
                if (Time.time >= lastSheepTime + timeBetweenSheep)
                {
                    SheepLaunch();
                }
            }
            else if (!isLanuching)
            {
                if (Time.time >= launchStartTime + timeBetweenLaunches)
                {
                    BeginLaunch();
                }
            }
        }

	}

    //begin launch
    private void BeginLaunch()
    {
        //set timing
        launchStartTime = Time.time;
        //set is launching to true
        isLanuching = true;
    }

    //Launch logic
    private void SheepLaunch()
    {
        //set timing
        lastSheepTime = Time.time;
        //get a random location around launch radius
        float randomX = Random.Range(-launchAreaRadius, launchAreaRadius);
        float randomZ = Random.Range(-launchAreaRadius, launchAreaRadius);
        Vector3 spawnPos = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //create a sheep object at spawn pos
        GameObject sheepClone = Instantiate(sheepObject, spawnPos, Quaternion.identity);
        //get random drop pos
        randomX = Random.Range(-dropRadius, dropRadius);
        randomZ = Random.Range(-dropRadius, dropRadius);
        Vector3 dropPos = new Vector3(dropCenter.x + randomX, dropCenter.y + dropHeight, dropCenter.z + randomZ);
        //set up vars
        sheepClone.GetComponent<BarnSheepBomb>().SetupVars(launchSpeed, dropSpeed, sheepClone.transform.position.y + dropHeight, dropPos);

        //increment current sheep index
        currentSheepIndex++;
        //if sheep index = num of sheep to be launched, reset
        if(currentSheepIndex >= numSheepPer)
        {
            isLanuching = false;
            currentSheepIndex = 0;
        }
    }
}
