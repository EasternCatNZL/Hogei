using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RamBehaviour : EnemyBehavior {

    [Header("Timing vars")]
    [Tooltip("Charge up time")]
    public float chargeTime = 2.0f;
    [Tooltip("Recover time")]
    public float recoverTime = 1.0f;
    [Tooltip("Time between bullets")]
    public float timeBetweenBullets = 0.2f;
    [Tooltip("Time between steps")]
    public float timeBetweenSteps = 0.5f;
    [Tooltip("Time check floor existed")]
    public float timeCheckFloor = 2.0f;

    [Header("Recoil values")]
    [Tooltip("Distance to jump back")]
    public float recoilDistance = 2.0f;
    [Tooltip("Jump power")]
    public float jumpPower = 1.0f;

    [Header("Attack vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("The speed at which sheep charges")]
    public float chargeSpeed = 10.0f;
    [Tooltip("Acceleration")]
    public float accel = 5.0f;
    [Tooltip("Max charge speed")]
    public float maxChargeSpeed = 20.0f;
    [Tooltip("The damage sheep does on collision")]
    public float damage = 3.0f;

    [Header("Physics rays")]
    public float rayLength = 3.0f;

    [Header("Tags")]
    public string targetTag = "Player";
    public string dungeonTag = "Dungeon";
    public string bulletTag = "Bullet";

    //control vars
    private bool isCharging = false; //checks if ram is currently charging up
    private bool isMoving = false; //checks if ram is currently moving
    private bool isRecovering = false; //checks if ram is recovering from a collision
    private bool isPaused = false; //checks if pause has been called
    private bool onFloor = true; //checks if ram is colliding with the floor
    [HideInInspector]
    public float timeChargeBegan = 0.0f; //time charge up began
    [HideInInspector]
    public float currentSpeed = 0.0f; //the current speed of the object
    private float timeLastShot = 0.0f; //time of last shot
    private float timeLastStep = 0.0f; //time between speed ups
    private float timeLastFloorCheck = 0.0f; //time floor was last checked for
    private float timeRecoverBegan = 0.0f; //time recover began
    private float pauseStartTime = 0.0f; //time pause started
    private float pauseEndTime = 0.0f; //time pause ended

    private Rigidbody myRigid; //the rigidbody attached to this object
    [HideInInspector]
    public GameObject target; //the target this object is attacking

    //Booleans
    private bool DoJumped = false;

    //script refs
    private EnemyState state;
    private Animator Anim;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        if (GetComponent<EnemyState>())
        {
            state = GetComponent<EnemyState>();
        }
        else
        {
            Debug.Log("No EnemyState script attached to " + gameObject.name);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive && !isPaused)
        {
            Anim.SetBool("Alert", true);
            AdjustStates();
            if (isCharging)
            {
                //look at the target
                Look();
                if (Time.time > timeChargeBegan + chargeTime + (pauseEndTime - pauseStartTime))
                {
                    Move();
                    timeLastStep = Time.time;
                }
            }
            else if (isMoving)
            {
                //Move();
                if(Time.time > timeLastStep + timeBetweenSteps)
                {
                    Move();
                }
                Poop();
            }
            else if (isRecovering)
            {
                if (Time.time > timeRecoverBegan + recoverTime + (pauseEndTime - pauseStartTime))
                {
                    ChargeUp();
                }
            }
            //print(myRigid.velocity);
        }
    }

    //Adjust state
    public void AdjustStates()
    {
        if (state.isSlowed)
        {
            currentSpeed = chargeSpeed * state.slowModifier;
        }
        else
        {
            currentSpeed = chargeSpeed;
        }
    }

    //behaviour during charge up
    public void ChargeUp()
    {
        Anim.SetTrigger("Rear");
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
        if (target != null)
        {
            //look at the target
            transform.LookAt(target.transform.position);
            //remove any x and z change
            Quaternion newRotation = new Quaternion();
            newRotation.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            transform.rotation = newRotation;
        }
    }

    //move
    private void Move()
    {
        ////Quaternion fix = new Quaternion();
        //fix.eulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
        //transform.rotation = fix;
        Anim.SetTrigger("Charge");
        //change conditions
        isCharging = false;
        isMoving = true;

        //check floor exists
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, rayLength))
        {
            //check if ray hit floor
            if (hit.collider.CompareTag(dungeonTag))
            {
                //move
                myRigid.velocity = (transform.forward * currentSpeed) /** Time.deltaTime*/;
            }
            else
            {
               // myRigid.velocity = Vector3.zero;
                isMoving = false;
            }
        }

        //reset pause timers
        pauseStartTime = 0.0f;
        pauseEndTime = 0.0f;
    }

    //bullets
    private void Poop()
    {
        //check timing
        if(Time.time > timeLastShot + timeBetweenBullets)
        {
            //set last shot time to now
            timeLastShot = Time.time;
            //spawn a bullet
            GameObject bulletClone = Instantiate(bulletObject, transform.position, transform.rotation);
            //setup vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(0.0f);
        }
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

    //On death logic
    public override void AmDead()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if not active
        if (!isActive)
        {
            //check is bullet
            if (collision.gameObject.CompareTag(bulletTag))
            {
                Anim.SetBool("Alert", true);
                //activate
                isActive = true;
                //set target
                target = GameObject.FindGameObjectWithTag(targetTag);
            }
        }

        //make sure collision isnt with floor
        //check that is moving
        if (isMoving && !collision.gameObject.CompareTag(dungeonTag))
        {
            //any collision
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(damage);
                //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
            }
            Anim.SetTrigger("Land");
            //remove velocity
            myRigid.velocity = Vector3.zero;
            //get a location behind self
            Vector3 Dir = transform.position - collision.gameObject.transform.position;
            Dir.y = 1f;
            Dir.Normalize();
            Dir *= jumpPower;
            GetComponent<Rigidbody>().AddForce(Dir, ForceMode.Impulse);
            //Vector3 jumpBackLocation = transform.position + ((Dir.normalized) * recoilDistance);
            //transform.DOJump(jumpBackLocation, jumpPower, 1, recoverTime / 2, false);
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
