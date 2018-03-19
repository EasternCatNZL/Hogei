using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStreamShot : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeed = 10.0f;

    [Header("Timing vars")]
    [Tooltip("The amount of time between shots")]
    public float timeBetweenShots = 2.0f;

    [Header("Positioning vars")]
    [Tooltip("How far out to position bullet start from center")]
    public float distanceToStart = 0.1f;
    [Tooltip("Where the gun barrel is located")]
    public Transform barrelLocation;
    public bool isFiring = false;

    //[Header("Tags")]
    //[Tooltip("Bullet bank tag")]
    //public string bankTag = "Bullet Bank";

    ////bullet bank ref
    //private BulletBank bank;

    [Header("Audio")]
    public AudioSource bulletFireSound;

    [Header("VFX")]
    public GameObject muzzleFireVFX;

    //control vars
    private float lastShotTime = 0.0f; //time last bullet was fired

    // Use this for initialization
    void Start () {
        bulletFireSound.playOnAwake = false;
        //bank = GameObject.FindGameObjectWithTag(bankTag).GetComponent<BulletBank>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //attack use logic
    public void UseWeapon()
    {
        //check if input 
        if (Time.time > lastShotTime + timeBetweenShots)
        {
            //set last shot time to now
            lastShotTime = Time.time;

            //get a bullet
            GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            isFiring = true;
            //set the bullets position to this pos
            bullet.transform.position = transform.position + (transform.right * distanceToStart);
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = transform.rotation;
            //set up bullet
            bullet.GetComponent<PlayerStraightBullet>().SetupVars(bulletTravelSpeed, 0, false);

            //get a second bullet
            GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet2.transform.position = transform.position + (-transform.right * distanceToStart);
            //set the bullet's rotation to current rotation
            bullet2.transform.rotation = transform.rotation;
            //set up bullet
            bullet2.GetComponent<PlayerStraightBullet>().SetupVars(bulletTravelSpeed, 0, false);

            //play audio
            bulletFireSound.Play();
            Instantiate(muzzleFireVFX, transform.position, transform.rotation);


        }
    }
}
