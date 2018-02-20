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

    [Header("Particle effect")]
    [Tooltip("Particle emitted by bullet on impact")]
    public GameObject particleObject;

    [Header("Lifetime")]
    [Tooltip("Lifetime of the bullet")]
    public float lifeTime = 5.0f;

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
    private float setupStartTime = 0.0f;
    private float pauseStartTime = 0.0f;
    private float pauseEndTime = 0.0f;

    public bool isStarting = false;
    private bool isActive = false;
    private bool isSettingUp = false;
    private bool isReady = false;
    private bool isMoving = false;
    private bool isPaused = false;

    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        //check if not paused first
        if (!isPaused)
        {
            if (isStarting)
            {
                SetUp();
            }
            else if (isSettingUp && transform.position == setupDestination)
            {
                Aim();
            }
            else if (isReady && Time.time > startTime + startDelay)
            {
                Move();
            }
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
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
        //get destination
        setupDestination = transform.position + (transform.forward * setupDestinationDistance);

        //print("Variables set");
    }

    //func called when setup ready
    private void SetUp()
    {
        //change check vars
        isStarting = false;
        isSettingUp = true;
        //set timing
        setupStartTime = Time.time;

        //move
        transform.DOMove(setupDestination, setupTime, false);

        //play audio
        //bulletFireSound.Play();
    }

    //private IEnumerator BeginMove()
    //{
    //    print("Beggining move");

    //    //waits for setup to finish
    //    yield return new WaitForSecondsRealtime(startDelay);
    //    //gets a new rotation
    //    //Quaternion newRotation = new Quaternion();

    //    //set facing target
    //    transform.LookAt(GameObject.FindGameObjectWithTag(targetTag).transform.position);

    //    //start moving
    //    myRigid.velocity = transform.forward * travelSpeed;
    //    //play audio
    //    //bulletChangeDirectionSound.Play();
    //}

    private void Aim()
    {
        //change check vars
        isSettingUp = false;
        isReady = true;
        //set facing target
        transform.LookAt(GameObject.FindGameObjectWithTag(targetTag).transform.position);
        //set timing
        startTime = Time.time;
    }

    private void Move()
    {
        //change check vars
        isReady = false;
        isMoving = true;
        //start moving
        myRigid.velocity = transform.forward * travelSpeed;
    }

    //Pause events
    void OnPause()
    {
        isPaused = true;
        pauseStartTime += Time.time;
        //suspend current action and prepare required vars to resume
        if (isSettingUp)
        {
            //kill tween
            DOTween.Kill(transform);
            //transform.DOMove(transform.position, 0.0f, false);
            //alter setup time 
            setupTime = (setupStartTime + setupTime) - Time.time;
        }
        else if (isReady){
            //set pause start time, needed to adjust the delay
            pauseStartTime = Time.time;
        }
        else if (isMoving)
        {
            myRigid.velocity = Vector3.zero;
        }
    }

    private void OnUnpause()
    {
        isPaused = false;
        pauseEndTime += Time.time;
        //continue suspended action
        if (isSettingUp)
        {
            SetUp();
        }
        else if (isReady)
        {
            startDelay += Time.time - pauseStartTime;
        }
        else if (isMoving)
        {
            myRigid.velocity = transform.forward * travelSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
            //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
        }
        //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
        //Deactivate();
        Destroy(gameObject);
    }
}
