using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusTwoSideSpray : EnemyBehavior {

    [Header("Attack vars")]
    [Tooltip("The bullet game object fires")]
    public GameObject bulletObject;
    [Tooltip("Speed of the bullet")]
    public float bulletSpeed = 3.0f;
    [Tooltip("Number of shots per spray")]
    public int numShotsPerSpray = 3;

    [Header("Angle control")]
    [Tooltip("Angle between bullets")]
    public float angleBetweenBullets = 4.0f;

    [Header("Timing vars")]
    [Tooltip("The time between shots")]
    public float timeBetweenShots = 1f;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive && !isPaused)
        {
            if (Time.time > timeLastShot + timeBetweenShots + (pauseEndTime - pauseStartTime))
            {
                //print(isActive);
                Attack();
            }
        }
    }

    //attack logic
    private void Attack()
    {
        //set timing
        timeLastShot = Time.time;
        //reset pause times
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;

        //get a random starting angle
        float randomAngle = Random.Range(0.0f, 360.0f);

        //for the number of shots per spray
        for(int i = 0; i < numShotsPerSpray; i++)
        {
            //create a bullet
            GameObject bulletOne = Instantiate(bulletObject, transform.position + transform.up * bulletOffsetY, Quaternion.Euler(0.0f, (transform.rotation.eulerAngles.y + randomAngle) + (i * angleBetweenBullets), 0.0f));
            //set up the bullet vars
            bulletOne.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            //if not the first bullet, make second
            if(i > 0)
            {
                GameObject bulletTwo = Instantiate(bulletObject, transform.position + transform.up * bulletOffsetY, Quaternion.Euler(0.0f, (transform.rotation.eulerAngles.y + randomAngle) - (i * angleBetweenBullets), 0.0f));
                //set up the bullet vars
                bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            }
        }

        //create second spray in reverse <- 180 degree
        randomAngle += 180.0f;

        //for the number of shots per spray
        for (int i = 0; i < numShotsPerSpray; i++)
        {
            //create a bullet
            GameObject bulletOne = Instantiate(bulletObject, transform.position + transform.up * bulletOffsetY, Quaternion.Euler(0.0f, (transform.rotation.eulerAngles.y + randomAngle) + (i * angleBetweenBullets), 0.0f));
            //set up the bullet vars
            bulletOne.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            //if not the first bullet, make second
            if (i > 0)
            {
                GameObject bulletTwo = Instantiate(bulletObject, transform.position + transform.up * bulletOffsetY, Quaternion.Euler(0.0f, (transform.rotation.eulerAngles.y + randomAngle) - (i * angleBetweenBullets), 0.0f));
                //set up the bullet vars
                bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            }
        }
    }
}
