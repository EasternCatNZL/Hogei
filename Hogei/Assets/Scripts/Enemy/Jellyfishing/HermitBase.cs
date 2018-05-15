using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitBase : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet speed")]
    public float bulletSpeed = 5.0f;
    [Tooltip("Bullet life time")]
    public float lifeTime = 4.0f;

    [Header("Attack vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 1.0f;

    //script refs
    public HermitMoveBehavior hermit;

    //control vars
    public bool isAttacking = false; //checks if attacked
    public bool isReadying = false; //checks if currently getting ready
    protected bool canShoot = false;

    protected int currentShot = 0; //the index of current shot


    protected float lastAttackTime = 0.0f; //the time last attack occured
    protected float lastShotTime = 0.0f; //the time last bubble was shot
    protected float lastReadyUpTime = 0.0f; //the time last ready for attack

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
