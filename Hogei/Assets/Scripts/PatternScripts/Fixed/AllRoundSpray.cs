using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRoundSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Tooltip("Speed of bullet")]
    public float bulletSpeed = 2.0f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    public float angleChangePerShot = 60.0f;

    //[Header("Tags")]
    //public string bulletBankTag = "Bullet Bank";

    //script refs
    //private BulletBank bank;
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngleTotal = 0.0f; //the current angle the bullet is angled at in regards to owner
    private float pauseStartTime = 0.0f; //the time when pause starts
    private float pauseEndTime = 0.0f; //the time when pause ends
    private bool isPaused = false; //check if paused

    // Use this for initialization
    void Start () {
        //check if angle change per shot can cleanly divide by 360
        //bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
        enemyState = GetComponent<EnemyState>();
    }

    private void Awake()
    {
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

    //bullet firing coroutine
    private void BulletSpray()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //if pause was enacted before this shot, reset the vars
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //get a random starting angle
        float angle = /*Random.Range(0.0f, 360.0f);*/ 0.0f;
        //reset the angle total
        currentAngleTotal = 0.0f;


        //while current angle total not reached 360, keep spawning bullets
        while (currentAngleTotal < 360.0f)
        {
            //create a shot
            //get the current angle as a quaternion
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, angle, 0.0f);
            //get a bullet from the bank
            GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet.transform.position = transform.position;
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = currentRotation;
            //setup the bullet and fire
            //bullet.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            //bullet.GetComponent<AcceleratingBullet>().SetupVars(1.0f, 1.0f, 10.0f);

            //change the angle between shots
            angle += angleChangePerShot;
            //add the amount angle changed to current angle total
            currentAngleTotal += angleChangePerShot;
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
