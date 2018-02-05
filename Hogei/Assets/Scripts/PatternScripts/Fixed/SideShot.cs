using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideShot : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Tooltip("Speed of bullet")]
    public float bulletSpeed = 2.0f;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
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
            if (Time.time > timeLastSprayFired + timeBetweenSprays)
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

        //create a bullet object
        GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
        //get altered rotation to fire sideways from entity
        Quaternion alteredRotation = new Quaternion();
        alteredRotation.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y + 90.0f, 0);
        bullet.transform.rotation = alteredRotation;
        //setup bullet and fire
        bullet.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        //DEBUG: name enemy for management
        bullet.name = "Right bullet";

        //create second bullet
        GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);
        //get altered rotation to fire sideways from entity
        alteredRotation.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y - 90.0f, 0);
        bullet2.transform.rotation = alteredRotation;
        //setup bullet and fire
        bullet2.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        //DEBUG: name enemy for management
        bullet2.name = "Left bullet";
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
