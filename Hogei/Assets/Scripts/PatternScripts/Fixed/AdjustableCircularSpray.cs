using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableCircularSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Tooltip("Num of sprays")]
    public int numSprays = 1;

    [Tooltip("Number of bullets in each spray")]
    public int numBulletsPerSpray = 5;

    [Tooltip("Speed of bullet")]
    public float bulletSpeed = 2.0f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 20.0f;

    [Tooltip("Angle change per spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerSpray = 20.0f;

    [Tooltip("Positive or negative (1 or -1)")]
    [Range(-1, 1)]
    public float rotationDirection = 1.0f;

    //[Header("Tags")]
    //public string bulletBankTag = "Bullet Bank";

    //script refs
    //private BulletBank bank;
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
    private bool canShootBullet = false; //checks whether bullet can be fired
    private float angleChangeBetweenSprays = 0.0f; //the difference in angle between sprays
    private float pauseStartTime = 0.0f; //the time when pause starts
    private float pauseEndTime = 0.0f; //the time when pause ends
    private bool isPaused = false; //check if paused

    // Use this for initialization
    void Start () {
        //bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
        enemyState = GetComponent<EnemyState>();
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyState.GetIsActive() && !isPaused)
        {
            if (Time.time > (timeLastSprayFired + timeBetweenSprays) - (pauseEndTime - pauseStartTime))
            {
                BulletSpray();
            }
        }
        //print("bruh");
    }

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    //get the angle change between sprays based on number of specified sprays
    private void GetAngleBetweenSprays()
    {
        angleChangeBetweenSprays = 360 / numSprays;
    }

    //coroutine version of bullet spray
    private void BulletSpray()
    {
       // print("Starting");
        GetAngleBetweenSprays();

        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //if pause was enacted before this shot, reset the vars
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //for the number of shots in a spray
        for (int i = 0; i < numBulletsPerSpray; i++)
        {
            //for the number of sprays per call
            for (int j = 0; j < numSprays; j++)
            {
                //get an altered angle based on which spray
                float alteredAngle = currentAngle + (angleChangeBetweenSprays * j);
                //get a rotation
                Quaternion alteredRotation = new Quaternion();
                alteredRotation.eulerAngles = new Vector3(0.0f, alteredAngle, 0.0f);
                //get a bullet from the bank
                GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
                //set the bullets position to this pos
                bullet.transform.position = transform.position;
                //set the bullet's rotation to current rotation
                bullet.transform.rotation = alteredRotation;
                //setup the bullet and fire
                if (bullet.GetComponent<RegularStraightBullet>())
                {
                    bullet.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
                }
                else if (bullet.GetComponent<AcceleratingBullet>())
                {
                    bullet.GetComponent<AcceleratingBullet>().SetupVars(1.0f, 2.0f, 50.0f);
                }
                else if (bullet.GetComponent<DecceleratingBullet>())
                {
                    bullet.GetComponent<DecceleratingBullet>().SetupVars(20.0f, 10.0f, 1.0f);
                }
            }
            //change the angle between shots
            currentAngle += angleChangePerShot * rotationDirection;
        }
        //wait for the next spray
        //yield return new WaitForSecondsRealtime(scaledTimeBetweenSprays);

        //increase angle in prep of next spray
        currentAngle += angleChangePerSpray * rotationDirection;
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
