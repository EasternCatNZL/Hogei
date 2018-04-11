using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OctoBehavior : EnemyBehavior {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;

    [Header("Octo heads")]
    [Tooltip("List of octo heads")]
    public List<GameObject> octoHeadList = new List<GameObject>();

    [Header("Timing vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 5.0f;
    [Tooltip("Head turn time")]
    public float headTurnTime = 1.0f;

    [Header("Attack vars")]
    [Tooltip("The amount of force to shot with")]
    public float shotForce = 15.0f;

    [Header("Rotation vars")]
    [Tooltip("Angles at which to shoot at")]
    public float[] angleArray = new float[0];
    [Tooltip("Speed of rotation for heads")]
    public float[] rotateSpeedArray = new float[0];
    [Tooltip("Rotate direction")]
    [Range(-1, 1)]
    public int[] rotateDirectionArray = new int[0];

    [Header("Control vars")]
    public bool useRotate = true; //use rotation speeds to rotate around point
    public bool useSetAngles = false; //use set angles for rotations
    private bool isAttacking = false; //checks if attacking

    private float attackStartTime = 0.0f; //the time attack sequence started

	// Use this for initialization
	void Start () {
		if(octoHeadList.Count != angleArray.Length)
        {
            Debug.LogError("Different number of heads to angles");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            AttackLogic();
        }
        
	}

    //attack logic
    private void AttackLogic()
    {
        if (useRotate)
        {
            RotateHeads();
        }
        if (Time.time > attackStartTime + timeBetweenAttacks)
        {
            AttackSequence();
        }
        if (isAttacking)
        {
            if (Time.time > attackStartTime + headTurnTime)
            {
                Attack();
            }
        }
    }

    //Sequence logic
    private void AttackSequence()
    {
        //set timing
        attackStartTime = Time.time;
        //set is attacking to true
        isAttacking = true;
    }

    //Turn heads
    private void TurnHeads()
    {
        //for each head
        for(int i = 0; i < octoHeadList.Count; i++)
        {
            octoHeadList[i].transform.DORotate(new Vector3(0.0f, angleArray[i], 0.0f), headTurnTime);
        }
    }

    //Rotate
    private void RotateHeads()
    {
        //for each head
        for (int i = 0; i < octoHeadList.Count; i++)
        {
            octoHeadList[i].transform.Rotate(transform.up, (rotateSpeedArray[i] * rotateDirectionArray[i]) * Time.deltaTime);
        }
    }

    //Fire shot
    private void Attack()
    {
        if (useSetAngles)
        {
            //rearrange when done
            Rearrange();
        }

        //for each head
        for (int i = 0; i < octoHeadList.Count; i++)
        {
            //get the current head to face the current angle
            //octoHeadList[i].transform.rotation = Quaternion.Euler(0.0f, angleArray[i], 0.0f);
            //create a clone of the bullet
            GameObject bulletClone = Instantiate(bulletObject, octoHeadList[i].transform.position, octoHeadList[i].transform.rotation);
            //launch the bullet using force
            bulletClone.GetComponent<Rigidbody>().AddForce(bulletClone.transform.forward * shotForce, ForceMode.Impulse);
        }




        //set is attacking to false;
        isAttacking = false;
    }

    //Rearrange ref order of octo
    private void Rearrange()
    {
        //using fisher yates card shuffler

        //get system random
        System.Random random = new System.Random();
        //for all elements
        for(int i = 0; i < octoHeadList.Count; i++)
        {
            int r = i + (int)(random.NextDouble() * (octoHeadList.Count - i));
            GameObject randOcto = octoHeadList[r];
            octoHeadList[r] = octoHeadList[i];
            octoHeadList[i] = randOcto;
        }

        TurnHeads();
    }
}
