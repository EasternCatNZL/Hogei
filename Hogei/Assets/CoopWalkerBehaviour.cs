using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Walking Chicken Coop
/// Uses several different attacks to elimminate the play
/// -Dive Attack: Lifts off into the air then crashes down into an aoe
/// -Chicken Swarm: Shoots live chickens at the player
/// -Slam Attack: Slams the ground and creates bullets
/// </summary>
public class CoopWalkerBehaviour : EnemyBehavior {


    private GameObject Target;
    [Header("Slam Attack Settings")]
    public GameObject SlamBullet;
    public float SlamBulletSpeed;
    public int SlamNumBullets;
    public Transform RightFoot;
    public Transform LeftFoot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Dive()
    {
        //Shoot Raycast at ground to get landing point
        RaycastHit _HitInfo;
        Ray _Ray = new Ray(transform.position, -transform.up);
        if(Physics.Raycast(_Ray, out _HitInfo, 100f))
        {

        }
    }

    void SlamEvent(int _FootIndex)//0 = RightFoot, 1 = LeftFoot
    {
        if(_FootIndex == 0) CircleSpray(SlamNumBullets, RightFoot.position);
        else if(_FootIndex == 1) CircleSpray(SlamNumBullets, LeftFoot.position);
    }

    void CircleSpray(int _NumBullets, Vector3 _SpawnPos)
    {
        //get a random starting angle
        float angle = Random.Range(0f, 360f);
        //Angle Change 
        float angleChange = 360 / _NumBullets;
        //reset the angle total
        float currentAngleTotal = 0.0f;
        //while current angle total not reached 360, keep spawning bullets
        while (currentAngleTotal < 360.0f)
        {
            //create a shot
            //get the current angle as a quaternion
            Quaternion currentRotation = new Quaternion();
            currentRotation.eulerAngles = new Vector3(0.0f, angle, 0.0f);
            //get a bullet from the bank
            GameObject bullet = Instantiate(SlamBullet, _SpawnPos, transform.rotation);
            //set the bullets position to this pos
            bullet.transform.position = _SpawnPos;
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = currentRotation;
            //setup the bullet and fire
            bullet.GetComponent<RegularStraightBullet>().SetupVars(SlamBulletSpeed);

            //change the angle between shots
            angle += angleChange;
            //add the amount angle changed to current angle total
            currentAngleTotal += angleChange;
        }
    }
}
