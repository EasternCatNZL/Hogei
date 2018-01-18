using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleReleaseAimedShot : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 3.0f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Number of bullets per spray")]
    public int numBulletsPerSpray = 12;

    [Header("Bullet set up vars")]
    [Tooltip("Bullet base setup distance")]
    public float bulletBaseSetupDistance = 2.5f;
    [Tooltip("Bullet setup move time")]
    public float bulletSetupTime = 0.5f;
    [Tooltip("Bullet active move start time")]
    public float bulletStartMoveTimeDelay = 0.2f;
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeed = 5.0f;

    [Header("Tags")]
    [Tooltip("Target tag")]
    public string targetTag = "Player";

    //script refs
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngleTotal = 0.0f; //the current angle the bullet is angled at in regards to owner
    private float angleChangePerShot = 0.0f; //the angle change between each shot

    // Use this for initialization
    void Start () {
        enemyState = GetComponent<EnemyState>();
        //get angle change value
        angleChangePerShot = 360 / numBulletsPerSpray;
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyState.GetIsActive())
        {
            if (Time.time > timeLastSprayFired + timeBetweenSprays)
            {
                BulletSpray();
            }
        }
    }

    //bullet spray function
    private void BulletSpray()
    {
        //set time of last spray to now
        timeLastSprayFired = Time.time;

        //float to track angle change
        float angleChange = 0;
        //until angle change >= 360
        while(angleChange < 360)
        {
            //get the current angle as quaternion
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, angleChange, 0.0f);

            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);

            //set rotation to current rotation
            bulletClone.transform.rotation = currentRotation;

            //setup bullet variables
            bulletClone.GetComponent<TargetingBullet>().SetupVars(bulletBaseSetupDistance, bulletSetupTime, bulletSetupTime + bulletStartMoveTimeDelay, bulletTravelSpeed, targetTag);

            //increment angle change
            angleChange += angleChangePerShot;
        }
    }
}
