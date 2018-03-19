using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsorbNExpunge : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Asorbtion time")]
    public float asorbTime = 3.0f;
    [Tooltip("Time after spray till next asorb")]
    public float timeTillNextAsorb = 5.0f;

    [Header("Attack vars")]
    [Tooltip("The bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet min speed")]
    [Range(0.0f, 100.0f)]
    public float bulletSpeedMin = 5.0f;
    [Tooltip("Bullet max speed")]
    [Range(0.0f, 100.0f)]
    public float bulletSpeedMax = 15.0f;

    [Header("Periodic release?")]
    [Tooltip("When true, releases bullets on timer")]
    public bool doPeriodicRelease = false;

    [Header("Tags")]
    public string bulletTag = "Bullet";

    //control vars
    private bool isAsorbing = false;

    private int numStockpiledBullets = 0;

    private float asorbStartTime = 0.0f;
    private float sprayStartTime = 0.0f;

	// Use this for initialization
	void Start () {
        asorbStartTime = Time.time;
        isAsorbing = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (doPeriodicRelease)
        {
            if (isAsorbing)
            {
                if (Time.time > asorbStartTime + asorbTime)
                {
                    ReleaseBullets();
                }
            }
            else
            {
                if (Time.time > sprayStartTime + timeTillNextAsorb)
                {
                    isAsorbing = true;
                    asorbStartTime = Time.time;
                }
            }
        }
	}

    //Bullet release 
    private void ReleaseBullets()
    {
        //print("Firing " + numStockpiledBullets + " Bullets.");
        //set is asorbing to false
        isAsorbing = false;
        //set timings
        sprayStartTime = Time.time;

        //for all bullets gathered
        for (int i = 0; i < numStockpiledBullets; i++)
        {
            //spawn a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
            //get a random rotation
            Quaternion newRotation = Quaternion.Euler(new Vector3(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
            //set the rotation
            bulletClone.transform.rotation = newRotation;
            //setup bullet speed
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(Random.Range(bulletSpeedMin, bulletSpeedMax));
        }
        numStockpiledBullets = 0;
    }

    private void OnDestroy()
    {
        ReleaseBullets();
    }

    //collision logic
    private void OnCollisionEnter(Collision collision)
    {
        //if other object is bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //if currently asorbing
            if (isAsorbing)
            {
                numStockpiledBullets++;
            }
            //else take damage
            else
            {
                if (GetComponent<EntityHealth>())
                {
                    GetComponent<EntityHealth>().DecreaseHealth(1);
                }
            }
        }
    }
}
