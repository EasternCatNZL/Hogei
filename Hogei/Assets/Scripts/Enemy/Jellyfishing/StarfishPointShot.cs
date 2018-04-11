using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishPointShot : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("The bullet object")]
    public GameObject bulletObject;
    [Tooltip("The speed of the bullet")]
    public float bulletSpeed = 5.0f;

    [Header("Timing vars")]
    [Tooltip("Time between shots")]
    public float timeBetweenShots = 0.1f;

    //control vars
    private float lastShotTime = 0.0f;

    //script refs
    private StarfishSpinBehavior star;

	// Use this for initialization
	void Start () {
        star = GetComponentInParent<StarfishSpinBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
        if (star.isActive)
        {
            ShootBullet();
        }

	}

    //Bullet fire logic
    private void ShootBullet()
    {
        //if timing reached
        if (Time.time > lastShotTime + timeBetweenShots)
        {
            //create a clone of the bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the speed of the bullet
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            //set timing to now
            lastShotTime = Time.time;
        }
    }
}
