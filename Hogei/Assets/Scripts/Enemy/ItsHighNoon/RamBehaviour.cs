using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RamBehaviour : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Charge up time")]
    public float chargeTime = 2.0f;
    [Tooltip("Recover time")]
    public float recoverTime = 1.0f;

    [Header("Recoil values")]
    [Tooltip("Distance to jump back")]
    public float recoilDistance = 2.0f;
    [Tooltip("Jump power")]
    public float jumpPower = 1.0f;

    [Header("Attack vars")]
    [Tooltip("The speed at which sheep charges")]
    public float chargeSpeed = 10.0f;
    [Tooltip("The damage sheep does on collision")]
    public float damage = 3.0f;

    [Header("Tags")]
    public string targetTag = "Player";

    //control vars
    [HideInInspector]
    public bool isTriggered = false; //checks to see if trigger has been triggered
    private bool isCharging = false; //checks if ram is currently charging up
    private bool isMoving = false; //checks if ram is currently moving
    private bool isRecovering = false; //checks if ram is recovering from a collision
    private bool isPaused = false; //checks if pause has been called
    [HideInInspector]
    public float timeChargeBegan = 0.0f; //time charge up began
    private float timeRecoverBegan = 0.0f; //time recover began
    private float pauseStartTime = 0.0f; //time pause started
    private float pauseEndTime = 0.0f; //time pause ended

    private Rigidbody myRigid; //the rigidbody attached to this object
    [HideInInspector]
    public GameObject target; //the target this object is attacking

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isTriggered && !isPaused)
        {
            if (isCharging)
            {
                //look at the target
                Look();
                if (Time.time > timeChargeBegan + chargeTime + (pauseEndTime - pauseStartTime))
                {
                    Move();
                }
            }
            else if (isRecovering)
            {
                if (Time.time > timeRecoverBegan + recoverTime + (pauseEndTime - pauseStartTime))
                {
                    ChargeUp();
                }
            }
            print(myRigid.velocity);
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

    //behaviour during charge up
    public void ChargeUp()
    {
        //change conditions
        isRecovering = false;
        isCharging = true;

        timeChargeBegan = Time.time;



        //reset pause timers
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;
    }

    //look at
    private void Look()
    {
        //look at the target
        transform.LookAt(target.transform.position);
        //remove any x and z change
        //Quaternion newRotation = new Quaternion();
        //newRotation.eulerAngles = new Vector3(0.0f, transform.rotation.y, 0.0f);
        //transform.rotation = newRotation;
    }

    //move
    private void Move()
    {
        //change conditions
        isCharging = false;
        isMoving = true;
        //move
        myRigid.velocity = transform.forward * chargeSpeed;

        //reset pause timers
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;
    }

    //recovery
    private void Recover()
    {
        //change conditions
        isMoving = false;
        isRecovering = true;

        timeRecoverBegan = Time.time;

        //reset pause timers
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make sure collision isnt with floor
        //check that is moving
        if (isMoving)
        {
            //any collision
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(damage);
                //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
            }

            //remove velocity
            myRigid.velocity = Vector3.zero;
            //get a location behind self
            Vector3 jumpBackLocation = transform.position + (-transform.forward * recoilDistance);
            transform.DOJump(jumpBackLocation, jumpPower, 1, recoverTime / 2, false);
            Recover();
        }
        
    }

    void OnPause()
    {
        isPaused = true;
        pauseStartTime += Time.time;
        if (isMoving)
        {
            myRigid.velocity = Vector3.zero;
        }
    }

    void OnUnpause()
    {
        isPaused = false;
        pauseEndTime += Time.time;
        if (isMoving)
        {
            Move();
        }
    }
}
