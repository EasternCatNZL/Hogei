using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selaginella9 : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Shots")]
    public float timeBetweenShots = 0.2f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Header("Bullet set up vars")]
    [Tooltip("Bullet setup distance")]
    public float[] bulletSetUpDistanceArray = new float[0];
    [Tooltip("Bullet angle changes")]
    public float[] bulletAngleChangeArray = new float[0];
    [Tooltip("Bullet setup move time")]
    public float bulletSetupTime = 0.5f;
    [Tooltip("Pattern bullet set speed")]
    public float bulletTravelSpeed = 3.0f;
    [Tooltip("The number of steps")]
    public int steps = 2;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 4.0f;
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerSpray = 60.0f;

    //script refs
    //private BulletBank bank;
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
    private float pauseStartTime = 0.0f; //the time when pause starts
    private float pauseEndTime = 0.0f; //the time when pause ends
    private bool isPaused = false; //check if paused

    // Use this for initialization
    void Start () {
        enemyState = GetComponent<EnemyState>();
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

    //bullet spray function
    private void BulletSpray()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //if pause was enacted before this shot, reset the vars
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //reset current angle total between bullet rings
        float currentAngleTotal = 0.0f;
        //while current angle total not reached 360, keep spawning bullets
        while (currentAngleTotal < 360.0f)
        {
            //create a shot
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
            //rotate it to face desired rotation
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, currentAngle + currentAngleTotal, 0.0f);
            bulletClone.transform.rotation = currentRotation;
            //set up setup distances and angle changes
            for (int i = 0; i < steps; i++)
            {
                bulletClone.GetComponent<MultiStepBullet>().setupDistances.Add(bulletSetUpDistanceArray[i]);
                bulletClone.GetComponent<MultiStepBullet>().angleChanges.Add(bulletAngleChangeArray[i]);
            }
            //set up bullet variables
            bulletClone.GetComponent<MultiStepBullet>().SetupVars(bulletSetupTime, bulletTravelSpeed, steps);

            //create second shot in reverse direction
            GameObject bulletClone2 = Instantiate(bulletObject, transform.position, transform.rotation);
            //rotate it to face desired rotation
            currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, -currentAngle + -currentAngleTotal, 0.0f);
            bulletClone2.transform.rotation = currentRotation;
            //set up setup distances and angle changes
            for (int i = 0; i < steps; i++)
            {
                bulletClone2.GetComponent<MultiStepBullet>().setupDistances.Add(bulletSetUpDistanceArray[i]);
                bulletClone2.GetComponent<MultiStepBullet>().angleChanges.Add(-bulletAngleChangeArray[i]);
            }
            //set up bullet variables
            bulletClone2.GetComponent<MultiStepBullet>().SetupVars(bulletSetupTime, bulletTravelSpeed, steps);

            //change angle between bullets in same spray
            currentAngleTotal += angleChangePerSpray;
        }
        //change angle between shots
        currentAngle += angleChangePerShot;
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
