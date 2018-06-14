using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SheepBehaviour : EnemyBehavior {

    [Header("Timing vars")]
    [Tooltip("Charge up time")]
    public float chargeTime = 2.0f;

    [Header("Attack vars")]
    [Tooltip("The speed at which sheep charges")]
    public float chargeSpeed = 10.0f;
    [Tooltip("The damage sheep does on collision")]
    public float damage = 3.0f;
    [Tooltip("Track?")]
    public bool doTrack = true;
    [Tooltip("Collision damage")]
    public float collisionSelfDamage = 10.0f;

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Speed of bullet")]
    public float bulletSpeed = 2.0f;

    [Header("Angle Control")]
    [Tooltip("Angle change per shot in spray")]
    public float angleChangePerShot = 60.0f;

    [Header("SFX Settings")]
    private AudioSource mySource;
    private bool LaunchSoundPlayed = false;
    public AudioClip LaunchSound;

    [Header("VFX Settings")]
    public ParticleSystem RocketFlames;

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";
    public string floorTag = "Dungeon";

    [Header("Debugging")]
    public Vector3 RigidVelocity;

    //control vars
   
    public bool isPaused = false; //checks if pause has been called
    private bool isQuiting = false; //checks if the application is quiting
    [HideInInspector]
    public float currentSpeed = 0.0f; //the current speed of the object
    public float timeChargeBegan = 0.0f; //time charge up began
    private float pauseStartTime = 0.0f; //time pause started
    private float pauseEndTime = 0.0f; //time pause ended

    private Rigidbody myRigid; //the rigidbody attached to this object
    [HideInInspector]
    public GameObject target; //the target this object is attacking

    //Booleans
    private bool isGameQuit = false;
    private bool DoJumped = false;
    //script refs
    private EnemyState state;
    private Animator myAnim;


    // Use this for initialization
    void Start () {
        myAnim = GetComponent<Animator>();
        myAnim.speed = Random.Range(0.9f, 1.1f);
        myRigid = GetComponent<Rigidbody>();
        mySource = GetComponent<AudioSource>();
        currentSpeed = chargeSpeed;
        if (GetComponent<EnemyState>())
        {
            state = GetComponent<EnemyState>();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            if (isActive)
            {
                AttackLogic();
            }
        }
	}

    public override void Activate()
    {
        timeChargeBegan = Time.time;
        target = PlayerManager.GetInstance().Player;
        base.Activate();

    }

    //Attack logic
    private void AttackLogic()
    {
        if (doTrack)
        {
            AdjustStates();
            if (Time.time > timeChargeBegan + chargeTime + (pauseEndTime - pauseStartTime))
            {
                Move();
            }
            else
            {
                ChargeUp();
            }
        }
        else
        {
            Move();
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
    private void ChargeUp()
    {
        //Trigger animation
        myAnim.SetTrigger("ChargeUp");
        //Jump
        if (!DoJumped)
        {
            transform.DOJump(transform.position, 0.5f, 1, 0.5f);
            DoJumped = true;
        }
        //look at the target
        transform.DOLookAt(target.transform.position, 0.5f);
    }

    //move
    private void Move()
    {
        //Set Animation Trigger
        myAnim.SetTrigger("Charge");
        //Play the Launch SFX if it isn't already
        if (!LaunchSoundPlayed && mySource)
        {
            if (!mySource.isPlaying)
            {
                mySource.clip = LaunchSound;
                mySource.Play();
                LaunchSoundPlayed = true;
            }
        }

        //Play the rocket VFX if it isn't already
        if ( RocketFlames)
        {
            if (!RocketFlames.isPlaying)
            {
                RocketFlames.Play();
            }
        }
        myRigid.velocity = (myRigid.velocity + transform.forward).normalized * currentSpeed;
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

    //Bullet explosion
    public void BulletExplosion()
    {
        //get a random starting angle
        float angle = Random.Range(0.0f, 360.0f);
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
            GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet.transform.position = transform.position + transform.up * 0.4f;
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = currentRotation;
            //setup the bullet and fire
            bullet.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);

            //change the angle between shots
            angle += angleChangePerShot;
            //add the amount angle changed to current angle total
            currentAngleTotal += angleChangePerShot;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //check collision not with bullet or floor
        if (!collision.gameObject.CompareTag(bulletTag) && !collision.gameObject.CompareTag(floorTag)){
            //any collision that is not with a bullet

            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(damage);
                //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
            }
            //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
            GetComponent<EntityHealth>().DecreaseHealth(collisionSelfDamage);
        }
    }

    void OnApplicationQuit()
    {
        isQuiting = true;
    }


    void OnDestroy()
    {
    }


    void OnPause()
    {
        isPaused = true;
        pauseStartTime += Time.time;
    }

    void OnUnpause()
    {
        isPaused = false;
        pauseEndTime += Time.time;
    }
}
