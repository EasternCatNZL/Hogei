using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusRandomSpray : EnemyBehavior {

    [Header("Attack vars")]
    [Tooltip("The bullet game object fires")]
    public GameObject bulletObject;
    [Tooltip("Speed of the bullet")]
    public float bulletSpeed = 3.0f;
    [Tooltip("Speed step of bullet")]
    public float bulletSpeedStep = 1.0f;
    [Tooltip("Number of shots per spray")]
    public float numShotsPerSpray = 3.0f;

    [Header("Timing vars")]
    [Tooltip("The time between shots")]
    public float timeBetweenShots = 0.1f;
    [Tooltip("How much the time between shots increases")]
    public float rampAmount = 0.05f;
    [Tooltip("Percentage of health loss to trigger a ramp up")]
    public float rampPercentTrigger = 0.5f;

    [Header("Bullet vars")]
    [Tooltip("The y offset added to shots")]
    public float bulletOffsetY = 1f;

    [Header("Tags")]
    public string bulletTag = "Bullet";

    //control vars
    private bool isPaused = false; //check if pause has been called
    private float timeLastShot = 0.0f; //the time the last shot was fired
    private float pauseStartTime = 0.0f; //time pause started
    private float pauseEndTime = 0.0f; //time pause ended
    private Vector3 bulletOffset = Vector3.zero;
    private float NextRampTrigger = 1f;

    // Use this for initialization
    void Start () {
        isActive = false;
        NextRampTrigger -= rampPercentTrigger;
        bulletOffset = new Vector3(0f, bulletOffsetY, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive && !isPaused)
        {
            if(Time.time > timeLastShot + timeBetweenShots + (pauseEndTime - pauseStartTime))
            {
                //print(isActive);
                Attack();
            }
        }
	}

    //Attack logic
    private void Attack()
    {
        //print("Doin stuff");
        //set time of last shot to now
        timeLastShot = Time.time;
        //reset pause times
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //for the number of shots per spray
        for (int i = 0; i < numShotsPerSpray; i++)
        {         
            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position + bulletOffset, transform.rotation);
            //get a new rotation
            Quaternion newRotation = new Quaternion();
            float random = Random.Range(0, 360.0f);
            newRotation.eulerAngles = new Vector3(0.0f, random, 0.0f);
            //Assign rotation to bullet
            bulletClone.transform.rotation = newRotation;
            //assign speed
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed + (bulletSpeedStep * i));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if not active
        if (!isActive)
        {
            //check is bullet
            if (other.gameObject.CompareTag(bulletTag))
            {
                //activate
                isActive = true;
            }
        }
        EntityHealth myHealth = GetComponent<EntityHealth>();
        float percentLoss = myHealth.CurrentHealth / myHealth.MaxHealth;
        if (percentLoss < NextRampTrigger)
        {
            timeBetweenShots -= rampAmount;
            NextRampTrigger -= rampPercentTrigger;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EntityHealth myHealth = GetComponent<EntityHealth>();
        float percentLoss = myHealth.CurrentHealth / myHealth.MaxHealth;
        if(percentLoss < NextRampTrigger)
        {
            timeBetweenShots -= rampAmount;
            NextRampTrigger -= rampPercentTrigger;
        }
    }

    //pause funcs
    protected override void OnPause()
    {
        isPaused = true;
        pauseStartTime = Time.time;
    }
    
    protected override void OnUnpause()
    {
        isPaused = false;
        pauseEndTime = Time.time;
    }
}
