using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetupStraightBullet : MonoBehaviour {

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
    public float angleChange = 0.0f;

    //control vars
    private float startTime = 0.0f;
    private float setupStartTime = 0.0f;
    private float pauseStartTime = 0.0f;
    private float pauseEndTime = 0.0f;

    public bool isStarting = false;
    private bool isActive = false;
    private bool isSettingUp = false;
    //private bool isReady = false;
    private bool isMoving = false;
    private bool isPaused = false;

    [Header("Audio")]
    public AudioSource bulletFireSound;
    public AudioSource bulletChangeDirectionSound;

    //script ref
    //private BulletBank bulletBank;

    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        startTime = Time.time;
        //bulletFireSound.playOnAwake = false;
        //bulletChangeDirectionSound.playOnAwake = false;
        //SetUp();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if (isStarting)
            {
                SetUp();
            }
            else if (isSettingUp && transform.position == setupDestination)
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
    public void SetupVars(float dist , float setTime, float delay, float angle, float speed)
    {
        setupDestinationDistance = dist;
        setupTime = setTime;
        startDelay = delay;
        angleChange = angle;
        travelSpeed = speed;
        setupDestination = transform.position + (transform.forward * setupDestinationDistance);
        Turn();
        isStarting = true;
    }

    //func called when setup ready
    private void SetUp()
    {
        //change check vars
        isStarting = false;
        isSettingUp = true;

        //set timing
        setupStartTime = Time.time;

        transform.DOMove(setupDestination, setupTime, false);

        //play audio
        //bulletFireSound.Play();
        //transform.DOMove(new Vector3(2, 1, 3), 2, false);
        //StartCoroutine(BeginMove());
    }
    
    private void Turn()
    {
        //gets a new rotation
        Quaternion newRotation = new Quaternion();
        //alters rotation based own rotation + given rotation
        newRotation.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y + angleChange, 0.0f);
        transform.rotation = newRotation;
    }

    //private IEnumerator BeginMove()
    //{
    //    //waits for setup to finish
    //    yield return new WaitForSecondsRealtime(startDelay);
        
        
    //    //start moving
    //    myRigid.velocity = transform.forward * travelSpeed;
    //    //play audio
    //    //bulletChangeDirectionSound.Play();
    //}

    private void Move()
    {
        //change check vars
        isSettingUp = false;
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
        else if (isMoving)
        {
            myRigid.velocity = transform.forward * travelSpeed;
        }
    }

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
            //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
        }
        Instantiate(particleObject, transform.position, Quaternion.identity);
        //Deactivate();
        Destroy(gameObject);
    }
}
