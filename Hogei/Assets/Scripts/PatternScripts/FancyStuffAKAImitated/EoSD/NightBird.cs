using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBird : MonoBehaviour {

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 5.0f;

    [Tooltip("Time between layers")]
    public float timeBetweenLayers = 0.5f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Tooltip("Number of sprays")]
    public int numSpray = 2;

    [Tooltip("Number of bullet layers")]
    public int numBulletLayers = 1;

    [Tooltip("Number of bullets per layer")]
    public int numBulletsPerLayer = 16;

    [Tooltip("First layer speed")]
    public float firstLayerBulletSpeed = 1.0f;
    [Tooltip("Layer speed increment value")]
    public float layerSpeedIncrementValue = 0.5f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 4.0f;
    [Tooltip("Starting angle in releation to self")]
    [Range(0.0f, 360.0f)]
    public float startingAngle = 40.0f;
    [Tooltip("Slight angle alteration")]
    [Range(0.0f, 360.0f)]
    public float slightAngleAlteration = 0.7f;

    [Header("Tags")]
    public string bulletBankTag = "Bullet Bank";

    //script refs
    //private BulletBank bank;
    //private EnemyState enemyState;

    //control vars
    private int currentRotationDireciton = 1; //current rotation of spray
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngle = 0.0f; //the current angle the bullet is angled at in regards to owner
    private float pauseStartTime = 0.0f; //the time when pause starts
    private float pauseEndTime = 0.0f; //the time when pause ends
    private bool isPaused = false; //check if paused

    // Use this for initialization
    void Start () {
        //bank = GameObject.FindGameObjectWithTag(bulletBankTag).GetComponent<BulletBank>();
        //enemyState = GetComponent<EnemyState>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (enemyState.GetIsActive() && !isPaused)
        //{
        //    if (Time.time > (timeLastSprayFired + timeBetweenSprays) - (pauseEndTime - pauseStartTime))
        //    {
        //        BulletSpray();
        //    }
        //}
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
    public void BulletSpray()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //if pause was enacted before this shot, reset the vars
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //for the total num of sprays
        for (int i = 0; i < numSpray; i++)
        {
            //speed var for layers
            float speed = firstLayerBulletSpeed;
            //for all layers
            for (int j = 0; j < numBulletLayers; j++)
            {
                //make a storage angle
                float angle = (startingAngle * currentRotationDireciton) + (slightAngleAlteration * currentRotationDireciton * j);

                //for all bullets in the layer
                for (int k = 0; k < numBulletsPerLayer; k++)
                {
                    //create a shot
                    //get the current angle as a quaternion
                    Quaternion newRotation = new Quaternion();
                    newRotation.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y + angle, 0.0f);
                    //create a bullet clone, and orient it using the current angle
                    GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);

                    //GameObject bulletClone = bank.GetRegularStraightBullet();

                    //set the bullets position to this pos
                    bulletClone.transform.position = transform.position;
                    //set the bullet's rotation to current rotation
                    bulletClone.transform.rotation = newRotation;

                    //change angle between shots
                    angle -= angleChangePerShot * currentRotationDireciton;

                    //setup the bullet and fire
                    bulletClone.GetComponent<RegularStraightBullet>().SetupVars(speed);
                }
                //increment the speed between layers
                speed += layerSpeedIncrementValue;
            }
            //change the direction between layers
            currentRotationDireciton *= -1;
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
