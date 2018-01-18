using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetingBullet : MonoBehaviour {

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Speed")]
    [Tooltip("Travel speed of bullet")]
    public float travelSpeed = 2.0f;

    //set up vars
    [HideInInspector]
    public Vector3 setupDestination = new Vector3(0, 0, 0);
    [HideInInspector]
    public float setupDestinationDistance = 0.0f;
    [HideInInspector]
    public float setupTime = 0.0f;
    [HideInInspector]
    public float startDelay = 0.0f;
    [HideInInspector]
    public string targetTag = "Player";

    //control vars
    private float startTime = 0.0f;
    public bool isStarting = false;
    private bool isActive = false;

    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (isStarting)
        {
            SetUp();
        }
    }

    //sets up vars for bullet behaviour
    public void SetupVars(float dist, float setTime, float delay, float speed, string tag)
    {
        setupDestinationDistance = dist;
        setupTime = setTime;
        startDelay = delay;
        travelSpeed = speed;
        isStarting = true;
        targetTag = tag;

        print("Variables set");
    }

    //func called when setup ready
    private void SetUp()
    {
        isStarting = false;
        setupDestination = transform.position + (transform.forward * setupDestinationDistance);
        transform.DOMove(setupDestination, setupTime, false);
        //play audio
        //bulletFireSound.Play();
        //transform.DOMove(new Vector3(2, 1, 3), 2, false);
        StartCoroutine(BeginMove());

        print("Set up");
    }

    private IEnumerator BeginMove()
    {
        print("Beggining move");

        //waits for setup to finish
        yield return new WaitForSecondsRealtime(startDelay);
        //gets a new rotation
        Quaternion newRotation = new Quaternion();

        //set facing target
        transform.LookAt(GameObject.FindGameObjectWithTag(targetTag).transform.position);

        //start moving
        myRigid.velocity = transform.forward * travelSpeed;
        //play audio
        //bulletChangeDirectionSound.Play();
    }
}
