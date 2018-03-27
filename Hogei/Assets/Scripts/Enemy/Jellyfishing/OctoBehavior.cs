using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OctoBehavior : MonoBehaviour {

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
    [Tooltip("Angles at which to shoot at")]
    public float[] angleArray = new float[0];

    //control vars
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
		if(Time.time > attackStartTime + timeBetweenAttacks)
        {
            AttackSequence();
        }
        if (isAttacking)
        {
            if(Time.time > attackStartTime + headTurnTime)
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

    //Fire shot
    private void Attack()
    {
        //for each head
        for(int i = 0; i < octoHeadList.Count; i++)
        {
            //get the current head to face the current angle
            octoHeadList[i].transform.rotation = Quaternion.Euler(0.0f, angleArray[i], 0.0f);
            //create a clone of the bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, octoHeadList[i].transform.rotation);
            //launch the bullet using force
            bulletClone.GetComponent<Rigidbody>().AddForce(bulletClone.transform.forward * shotForce, ForceMode.Impulse);
        }

        //rearrange when done
        Rearrange();

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
    }
}
