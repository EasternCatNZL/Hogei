using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWideSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Number of arcs")]
    public int numArcs = 2;

    [Header("Angle Control")]
    [Tooltip("Starting angle")]
    [Range(0.0f, 360.0f)]
    public float startingAngle = 0.0f;
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 4.0f;

    [Tooltip("Speed of bullet")]
    public float bulletSpeed = 2.0f;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
    private float pauseStartTime = 0.0f; //the time when pause starts
    private float pauseEndTime = 0.0f; //the time when pause ends
    private bool isPaused = false; //check if paused

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if (Time.time > (timeLastSprayFired + timeBetweenSprays) - (pauseEndTime - pauseStartTime))
            {
                ShootBullets();
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

    //bullet firing logic
    private void ShootBullets()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //if pause was enacted before this shot, reset the vars
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //create a bullet that fires straight forwards
        GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        //setup bullet and fire
        bullet.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        //DEBUG: name enemy for management
        bullet.name = "Forward bullet";

        //create bullets that fire in arc backwards
        //for all arcs
        for (int j = 0; j < numArcs; j++)
        {
            //get a rotation
            Quaternion alteredRotation = transform.rotation;
            alteredRotation.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + 180.0f + (startingAngle + (angleChangePerShot * j)), 0.0f);
            //get a bullet from the bank
            GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet2.transform.position = transform.position;
            //set the bullet's rotation to current rotation
            bullet2.transform.rotation = alteredRotation;
            //setup the bullet and fire
            bullet2.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);

            //if not first (middle) shot, than create a second shot with negative angle
            if (j > 0)
            {
                //get a rotation
                alteredRotation = transform.rotation;
                alteredRotation.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + 180.0f + (startingAngle + (-angleChangePerShot * j)), 0.0f);
                //get a bullet from the bank
                GameObject bullet3 = Instantiate(bulletObject, transform.position, transform.rotation);
                //set the bullets position to this pos
                bullet3.transform.position = transform.position;
                //set the bullet's rotation to current rotation
                bullet3.transform.rotation = alteredRotation;
                //setup the bullet and fire
                bullet3.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            }
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
