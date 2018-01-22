using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondBlizzard : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.0f;

    [Header("Distance vars")]
    [Tooltip("Distance in any direction that bullets can spawn")]
    public float maxDistanceFromSelf = 3.0f;

    [Header("Bullet Vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Number of bullets per spray")]
    public int numBulletsPerSpray = 6;

    [Header("Bullet set up vars")]
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeed = 5.0f;

    //script refs
    private EnemyState enemyState;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
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

        //get a random x and z around self
        float randomX = Random.Range(-maxDistanceFromSelf, maxDistanceFromSelf);
        float randomZ = Random.Range(-maxDistanceFromSelf, maxDistanceFromSelf);

        //make a vector to be used as the spawn location of bullets for this run of func
        Vector3 spawnPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //float to track angle change
        float angleChange = 0;
        //get a random starting angle
        float startAngle = Random.Range(0.0f, 360.0f);
        //until angle change >= 360
        while (angleChange < 360)
        {
            //get the current angle as quaternion
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, angleChange + startAngle, 0.0f);

            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, spawnPoint, transform.rotation);

            //set rotation to current rotation
            bulletClone.transform.rotation = currentRotation;

            //setup bullet variables
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletTravelSpeed);

            //increment angle change
            angleChange += angleChangePerShot;
        }

    }
}
