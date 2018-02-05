using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDreamVine : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenShots = 1.0f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Header("Spawn locations")]
    [Tooltip("The distance around self to spawn bullets")]
    public float[] spawnLocationDistanceArray = new float[0];
    [Tooltip("Pattern bullet set speed")]
    public float bulletTravelSpeed = 3.0f;
    [Tooltip("Number of spawn points")]
    public int numSpawns = 5;
    [Tooltip("Number of bullets per spawn")]
    public int numBulletsPerSpawn = 3;

    //script refs
    private EnemyState enemyState;

    //control vars
    private int currentSpawnIndex = 0; //current index in array for spawn distances
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float angleBetweenSpawns = 0.0f; //the angle between spawn points around self
    private float angleBetweenShots = 0.0f; //the angle between shots
    private float pauseStartTime = 0.0f; //the time when pause starts
    private float pauseEndTime = 0.0f; //the time when pause ends
    private bool isPaused = false; //check if paused

    // Use this for initialization
    void Start () {
        enemyState = GetComponent<EnemyState>();
        //get angles
        angleBetweenSpawns = 360 / numSpawns;
        angleBetweenShots = 360 / numBulletsPerSpawn;
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyState.GetIsActive() && !isPaused)
        {
            if (Time.time > (timeLastSprayFired + timeBetweenShots) - (pauseEndTime - pauseStartTime))
            {
                BulletSpray();
            }
        }
    }

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        print("Unsubscribed to event");
    }

    private void BulletSpray()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //if pause was enacted before this shot, reset the vars
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        print("Firing with index " + currentSpawnIndex);

        //for each spawn point
        for (int i = 0; i < numSpawns; i++)
        {
            //get location based on current angle
            Vector3 direction = Quaternion.Euler(new Vector3(0.0f, i * angleBetweenSpawns, 0.0f)) * transform.forward;
            Vector3 location = transform.position + (direction * spawnLocationDistanceArray[currentSpawnIndex]); 
            //for each bullet in spawn
            for (int j = 0; j < numBulletsPerSpawn; j++)
            {
                //get a rotation for the current bullet
                Quaternion newRotation = new Quaternion();
                newRotation.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y + (i * angleBetweenSpawns) + (j * angleBetweenShots), 0.0f);
                //create a new bullet at the location
                //create a bullet
                GameObject bulletClone = Instantiate(bulletObject, location, transform.rotation);
                //set rotation to current rotation
                bulletClone.transform.rotation = newRotation;
                //setup bullet variables
                bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletTravelSpeed);
            }
        }
        //increment the current index
        currentSpawnIndex++;
        //if larger than array length, reset
        if(currentSpawnIndex >= spawnLocationDistanceArray.Length)
        {
            currentSpawnIndex = 0;
        }
    }

    //Pause events
    void OnPause()
    {
        pauseStartTime = Time.time;
        isPaused = true;
    }

    void OnUnpause()
    {
        pauseEndTime = Time.time;
        isPaused = false;
    }
}
