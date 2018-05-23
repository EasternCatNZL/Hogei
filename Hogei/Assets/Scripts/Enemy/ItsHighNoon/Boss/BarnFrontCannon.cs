using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnFrontCannon : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Sheep object")]
    public GameObject sheepObject;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 15.0f;
    [Tooltip("Number of arcs")]
    public int numArcs = 2;

    [Header("Timing Vars")]
    [Tooltip("Time Between Sprays")]
    public float timeBetweenSprays = 5.0f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 4.0f;

    [Header("Tags")]
    public string playerTag = "Player";

    [Header("Control vars")]
    public bool isUsing = true; //check if this should be used
    public bool fireSheep = true;

    private float cannonShotTime = 0.0f; //time cannon last shot

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isUsing)
        {
            if (Time.time > cannonShotTime + timeBetweenSprays)
            {
                FireCannon();
            }
        }

    }

    //Fire cannon
    private void FireCannon()
    {
        //set timing
        cannonShotTime = Time.time;

        if (fireSheep)
        {
            //create sheep
            GameObject sheepClone = Instantiate(sheepObject, transform.position, transform.rotation);
            //set up vars on sheep behavior
            SheepBehaviour sheepBehave = sheepClone.GetComponent<SheepBehaviour>();
            sheepBehave.doTrack = false;
            sheepBehave.chargeSpeed = bulletSpeed;
            sheepBehave.isActive = true;
            sheepClone.GetComponent<EntityHealth>().CurrentHealth = 1;
        }
        //for the number of arcs, fire regular bullets
        for (int i = 1; i < numArcs; i++)
        {
            //get rotation
            Quaternion rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + angleChangePerShot * i, transform.rotation.eulerAngles.z);
            //create a bullet
            GameObject bulletOne = Instantiate(bulletObject, transform.position, rot);
            bulletOne.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);

            rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - angleChangePerShot * i, transform.rotation.eulerAngles.z);
            //create second bullet
            GameObject bulletTwo = Instantiate(bulletObject, transform.position, rot);
            bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        }
    }

    //Fire cannon aimed ver
    private void AimedFireCannon()
    {
        //set timing
        cannonShotTime = Time.time;

        //get direction to target
        Vector3 directionTo = GameObject.FindGameObjectWithTag(playerTag).transform.position - transform.position;
        //get rotation
        float startAngle = Vector3.Angle(transform.forward, directionTo);

        if (fireSheep)
        {
            //create sheep
            GameObject sheepClone = Instantiate(sheepObject, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - startAngle, transform.rotation.eulerAngles.z));
            //set up vars on sheep behavior
            SheepBehaviour sheepBehave = sheepClone.GetComponent<SheepBehaviour>();
            sheepBehave.doTrack = false;
            sheepBehave.chargeSpeed = bulletSpeed;
            sheepBehave.Activate();
            sheepBehave.isActive = true;
            sheepClone.GetComponent<EntityHealth>().CurrentHealth = 1;
        }
        //for the number of arcs, fire regular bullets
        for (int i = 1; i < numArcs; i++)
        {
            //get rotation
            Quaternion rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - startAngle + angleChangePerShot * i, transform.rotation.eulerAngles.z);
            //create a bullet
            GameObject bulletOne = Instantiate(bulletObject, transform.position, rot);
            bulletOne.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);

            rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - startAngle - angleChangePerShot * i, transform.rotation.eulerAngles.z);
            //create second bullet
            GameObject bulletTwo = Instantiate(bulletObject, transform.position, rot);
            bulletTwo.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
        }
    }
}
