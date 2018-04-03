using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BarnCannonHandler : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 15.0f;
    [Tooltip("Shot timing min")]
    public float timeBetweenShotsMin = 2.0f;
    [Tooltip("Shot timing max")]
    public float timeBetweenShotsMax = 4.0f;

    [Header("Rotation vars")]
    [Tooltip("Rotation limit")]
    public float rotationLimit = 30.0f;
    [Tooltip("Rotation time")]
    public float rotationTime = 2.0f;
    [Tooltip("Rotation delay")]
    public float rotationDelay = 5.0f;
    [Tooltip("Starting rotation")]
    public float startingRotation = 90.0f;

    //control vars
    [HideInInspector]
    public bool isUsing = false; //check if this should be used
    private bool doRotation = true; //check if cannons should be rotating
    private bool doShoot = true; //check if cannons should be shooting

    //private float startingRotation = 0.0f; //for rotation control
    private float cannonTurnTime = 0.0f; //time cannon turn began
    private float cannonShotTime = 0.0f; //time cannon last shot
    private float cannonReloadTime = 0.0f; //time needed for next reload

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (doRotation)
        {
            if(Time.time > cannonTurnTime + rotationDelay)
            {
                TurnCannon();
            }
        }
        if (doShoot)
        {
            if(Time.time > cannonShotTime + cannonReloadTime)
            {
                FireCannon();
            }
        }
	}

    //Turn cannon
    private void TurnCannon()
    {
        //set timing
        cannonTurnTime = Time.time;
        //get random rotation
        Vector3 randRot = new Vector3(0.0f, startingRotation + Random.Range(-rotationLimit, rotationLimit), 0.0f);
        //print(randRot);
        //turn cannon
        transform.DOLocalRotate(randRot, rotationTime);
    }

    //Fire cannon
    private void FireCannon()
    {
        //set timing
        cannonShotTime = Time.time;
        //create a bullet
        GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
        //set vars
        bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        //set next reload time
        cannonReloadTime = Random.Range(timeBetweenShotsMin, timeBetweenShotsMax);
    }
}
