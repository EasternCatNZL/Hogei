using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckyEnclosedSpray : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Side cannon speed")]
    public float sideCannonSpeed = 10.0f;
    [Tooltip("Main cannon speed")]
    public float mainCannonSpeed = 15.0f;
    [Tooltip("Num shots in spray")]
    public int numShotsInSpray = 8;

    [Header("Transform refs")]
    public Transform mainDuck;
    public Transform leftDuck;
    public Transform rightDuck;

    [Header("Timing vars")]
    [Tooltip("Time between side cannon shots")]
    public float timeBetweenSideCannon = 0.1f;
    [Tooltip("Time between main cannon shots")]
    public float timeBetweenMainCannon = 1.0f;

    [Header("Angle control")]
    [Tooltip("Starting angle left")]
    public float startingAngleLeft = 0.0f;
    [Tooltip("Starting angle right")]
    public float startingAngleRight = 0.0f;
    [Tooltip("Side cannon max angle")]
    [Range(0.0f, 360.0f)]
    public float sideCannonMaxAngle = 50.0f;
    [Tooltip("Side cannon min angle")]
    [Range(0.0f, 360.0f)]
    public float sideCannonMinAngle = 30.0f;
    [Tooltip("Angle change per shot")]
    [Range(0.0f, 360.0f)]
    public float angleChangePerShot = 1.0f;
    [Tooltip("Angle change between shots")]
    [Range(0.0f, 360.0f)]
    public float angleChangeBetweenShots = 2.0f;
    [Tooltip("Direction Left")]
    [Range(-1, 1)]
    public int directionLeft = -1;
    [Tooltip("Direction Right")]
    [Range(-1, 1)]
    public int directionRight = 1;
    [Tooltip("Main cannon max angle")]
    [Range(0.0f, 360.0f)]
    public float mainMaxAngleVariance = 20.0f;
    [Tooltip("Main cannon min angle")]
    [Range(0.0f, 360.0f)]
    public float mainMinAngleVariance = 0.0f;
    [Tooltip("Direction main")]
    [Range(-1, 1)]
    public int directionMain = 1;

    //control vars
    private float timeSideCannonLastShot = 0.0f; // time of last shot for side cannons
    private float timeMainCannonLastShot = 0.0f; //time of last shot for main cannons
    private float leftCurrentAngle = 0.0f; //the current angle of left cannon
    private float rightCurrentAngle = 0.0f; //the current angle of right cannon

	// Use this for initialization
	void Start () {
        leftCurrentAngle = startingAngleLeft;
        rightCurrentAngle = startingAngleRight;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timeSideCannonLastShot + timeBetweenSideCannon)
        {
            SideCannonSpray();
        }
        if(Time.time > timeMainCannonLastShot + timeBetweenMainCannon)
        {
            MainCannon();
        }
	}

    //spray for two side cannons
    private void SideCannonSpray()
    {
        //set timing
        timeSideCannonLastShot = Time.time;
        LeftCannon();
        RightCannon();
    }

    //logic for two side cannons
    private void LeftCannon()
    {
        //for the num shots in spray
        for(int i = 0; i < numShotsInSpray; i++)
        {
            //create a bullet, aligned to current angle at pos of left cannon
            GameObject bulletClone = Instantiate(bulletObject, leftDuck.position, Quaternion.Euler(0.0f, leftCurrentAngle + (angleChangeBetweenShots * i), 0.0f));
            //set up bullet vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(sideCannonSpeed);
            //increment the angle
            //leftCurrentAngle += angleChangePerShot;
            Mathf.Clamp(leftCurrentAngle += (angleChangePerShot * directionLeft), -sideCannonMinAngle, -sideCannonMaxAngle);
        }


        //if angle has reached edge, change direction
        if(leftCurrentAngle >= -sideCannonMinAngle || leftCurrentAngle <= -sideCannonMaxAngle)
        {
            directionLeft *= -1;
        }
    }

    private void RightCannon()
    {
        //for the num shots in spray
        for (int i = 0; i < numShotsInSpray; i++)
        {
            //create a bullet, aligned to current angle at pos of left cannon
            GameObject bulletClone = Instantiate(bulletObject, rightDuck.position, Quaternion.Euler(0.0f, rightCurrentAngle - (angleChangeBetweenShots * i), 0.0f));
            //set up bullet vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(sideCannonSpeed);
            //increment the angle
            //leftCurrentAngle += angleChangePerShot;
            Mathf.Clamp(rightCurrentAngle += (angleChangePerShot * directionRight), sideCannonMinAngle, sideCannonMaxAngle);
        }


        //if angle has reached edge, change direction
        if (rightCurrentAngle <= sideCannonMinAngle || rightCurrentAngle >= sideCannonMaxAngle)
        {
            directionRight *= -1;
        }
    }

    //logic for main cannon
    private void MainCannon()
    {
        //set timing
        timeMainCannonLastShot = Time.time;
        //get a random angle with side
        float randomAngle = Random.Range(mainMinAngleVariance, mainMaxAngleVariance) * directionMain;
        //create a clone of the bullet, using random angle
        GameObject bulletClone = Instantiate(bulletObject, mainDuck.position, Quaternion.Euler(0.0f, randomAngle, 0.0f));
        //set up bullet vars
        bulletClone.GetComponent<RegularStraightBullet>().SetupVars(mainCannonSpeed);
        //change direction
        directionMain *= -1;
    }
}
