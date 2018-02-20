using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusRandomSpray : MonoBehaviour {

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

    //control vars
    [HideInInspector]
    public bool isActive = false; // check if cactus active
    private float timeLastShot = 0.0f; //the time the last shot was fired

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            if(Time.time > timeLastShot + timeBetweenShots)
            {
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

        //for the number of shots per spray
        for (int i = 0; i < numShotsPerSpray; i++)
        {
            //create a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
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
}
