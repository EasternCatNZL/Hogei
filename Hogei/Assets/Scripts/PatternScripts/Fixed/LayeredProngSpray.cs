using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeredProngSpray : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 5.0f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Number of bullet layers")]
    public int numBulletLayers = 5;
    [Tooltip("Number of arcs")]
    public int numArcs = 2;
    [Tooltip("First layer speed")]
    public float firstLayerBulletSpeed = 1.0f;
    [Tooltip("Layer speed increment value")]
    public float layerSpeedIncrementValue = 0.5f;

    [Header("Angle Control")]
    [Tooltip("Starting angle")]
    [Range(0.0f, 360.0f)]
    public float startingAngle = 0.0f;
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 4.0f;

    [Header("Tags")]
    public string bulletBankTag = "Bullet Bank";

    //script refs
    //private BulletBank bank;
    private EnemyState enemyState;

    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
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

    //bullet firing coroutine
    private void BulletSpray()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //if pause was enacted before this shot, reset the vars
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //speed var
        float speed = firstLayerBulletSpeed;

        //for all layers
        for (int i = 0; i < numBulletLayers; i++)
        {
            //for all arcs
            for(int j = 0; j < numArcs; j++)
            {
                //get a rotation
                Quaternion alteredRotation = transform.rotation;
                alteredRotation.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + (startingAngle + (angleChangePerShot * j)), 0.0f);
                //get a bullet from the bank
                GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
                //set the bullets position to this pos
                bullet.transform.position = transform.position;
                //set the bullet's rotation to current rotation
                bullet.transform.rotation = alteredRotation;
                //setup the bullet and fire
                bullet.GetComponent<RegularStraightBullet>().SetupVars(speed);

                //if not first (middle) shot, than create a second shot with negative angle
                if (j > 0)
                {
                    //get a rotation
                    alteredRotation = transform.rotation;
                    alteredRotation.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + (startingAngle + (-angleChangePerShot * j)), 0.0f);
                    //get a bullet from the bank
                    GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);
                    //set the bullets position to this pos
                    bullet2.transform.position = transform.position;
                    //set the bullet's rotation to current rotation
                    bullet2.transform.rotation = alteredRotation;
                    //setup the bullet and fire
                    bullet2.GetComponent<RegularStraightBullet>().SetupVars(speed);
                }
                
            }
            //increment the speed between layers
            speed += layerSpeedIncrementValue;
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
