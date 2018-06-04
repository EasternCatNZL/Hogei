using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusRoundSpray : EnemyBehavior {

    [Header("Attack vars")]
    [Tooltip("The bullet game object fires")]
    public GameObject bulletObject;
    [Tooltip("Speed of the bullet")]
    public float bulletSpeed = 3.0f;

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 1.5f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    public float angleChangePerShot = 60.0f;

    [Header("Bullet vars")]
    public float bulletYOffset = 1f;

    //control vars
    private float timeLastSprayFired = 0.0f; //the time last spray began
    private float currentAngleTotal = 0.0f; //the current angle the bullet is angled at in regards to owner
    private float pauseStartTime = 0.0f; //the time when pause starts
    private float pauseEndTime = 0.0f; //the time when pause ends
    private bool isPaused = false; //check if paused

    private Vector3 bulletOffset = Vector3.zero;

    // Use this for initialization
    void Start () {
        bulletOffset = new Vector3(0f, bulletYOffset, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive && !isPaused)
        {
            if (Time.time > timeLastSprayFired + timeBetweenSprays + (pauseEndTime - pauseStartTime))
            {
                //print(isActive);
                Attack();
            }
        }
    }

    //attack logic
    private void Attack()
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
            GameObject bullet = Instantiate(bulletObject, transform.position + bulletOffset, transform.rotation);
            //set the bullets position to this pos
            bullet.transform.position = transform.position + bulletOffset;
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
