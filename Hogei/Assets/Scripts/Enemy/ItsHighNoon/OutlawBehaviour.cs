using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OutlawBehaviour : EnemyBehavior {

    [Header("Attack vars")]
    [Tooltip("The bullet game object enemy fires")]
    public GameObject bulletObject;
    [Tooltip("Speed of the bullet")]
    public float bulletSpeed = 5.0f;
    [Tooltip("Amount of damage this unit is doing")]
    public float damage = 1.0f;
    [Tooltip("Number of shots in round")]
    public int numShotsInRound = 3;
    [Tooltip("How far away the gunslinger can attack")]
    public float MaxRange = 1000f;

    [Header("Timing vars")]
    [Tooltip("Time between shots in round")]
    public float timeBetweenShots = 0.1f;
    [Tooltip("Time between rounds")]
    public float timeBetweenRounds = 1.0f;

    [Header("Sound Settigs")]
    public AudioClip ShotSound = null;
    [Range(0f,1f)]
    public float ShotSoundVolume = 1f;
    public Vector2 PitchVarianceRange = Vector2.zero;
    public float ShotSpatialBlend = 0.5f;

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";

    [Header("Animation tags")]
    public string attackTrigger = "DoAttack";

    //set up vars
    [HideInInspector]
    public float setupDistance = 0.0f;
    [HideInInspector]
    public float setupTime = 0.0f;

    //control vars
    protected int currentShotInRound = 0; //the current shot in a round
    protected float timeTillNextShot = 0.0f; //the time till the next shot should be fired
    protected float timeLastShot = 0.0f; //the time the last shot was fired
    protected float pauseStartTime = 0.0f; //the time pause was called
    protected float pauseEndTime = 0.0f; //the time end pause was called
    protected float tempSetupTime = 0.0f; //setup time recalculated when coming out of pause
    protected float setupStartTime = 0.0f; //time setup began
    [HideInInspector]
    public bool isMoving = false; //check if object is currently moving
    protected bool isPaused = false; //checks if pause has been called

    private Vector3 locationToSetup = Vector3.zero;
    [HideInInspector]
    public GameObject target; //the target this object is attacking

    //animator
    private Animator anim; //anim attached to this object

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPaused)
        {
            //check if setup has finished
            if (isActive)
            {
                AttackBehaviour();
                //if target null, try and search for target
                if (!target)
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                }
                Vector3 Dist = target.transform.position - transform.position;
                if (Dist.magnitude > MaxRange)
                {                   
                    isActive = false;
                }
            }
            else if (transform.position == locationToSetup)
            {
                isActive = true;
                isMoving = false;
            }
        }

	}

    //setup vars
    public void SetupVars(float setupDist, float time)
    {
        setupDistance = setupDist;
        setupTime = time;
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    //move
    public void MoveToSetupLocation()
    {
        //get the location to setup to
        locationToSetup = transform.position + (transform.forward * setupDistance);
        //tween to location
        transform.DOMove(locationToSetup, setupTime);
        //set setup start time to now
        setupStartTime = Time.time;
    }

    //Behaviour when setup has completed
    private void AttackBehaviour()
    {
        if (target)
        {
            //face the target
            //transform.LookAt(target.transform.position);
            //try to attack
            Attack();
        }
    }

    //Attack behaviour <- runs on timer
    private void Attack()
    {
        //check timing
        if(Time.time > timeLastShot + timeTillNextShot + (pauseEndTime - pauseStartTime))
        {
            if (ShotSound)
            {
                float pitch = Random.Range(PitchVarianceRange.x, PitchVarianceRange.y);
                MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
                SoundSettings.Pitch = pitch;
                SoundSettings.Volume = ShotSoundVolume;
                SoundSettings.SpatialBlend = ShotSpatialBlend;
                MusicManager.GetInstance().PlaySoundAtLocation(ShotSound, transform.position, SoundSettings);
            }
            
            //create a shot
            GameObject bulletClone = Instantiate(bulletObject, transform.position + transform.up, transform.rotation);
            //point the bullet at player
            bulletClone.transform.LookAt(target.transform.position);
            //setup the bullet vars
            bulletClone.GetComponent<RegularStraightBullet>().SetupVars(bulletSpeed);
            //increment the current bullet count
            currentShotInRound++;

            //fire animator
            anim.SetTrigger(attackTrigger);
            //set time till next shot
            if (currentShotInRound >= numShotsInRound)
            {
                timeTillNextShot = timeBetweenRounds;
                currentShotInRound = 0;
            }
            else
            {
                timeTillNextShot = timeBetweenShots;
            }
            //set time last shot to now
            timeLastShot = Time.time;
            //reset pause time
            pauseStartTime = 0.0f;
            pauseEndTime = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if not active
        if (!isActive)
        {
            //check is bullet
            if (collision.gameObject.CompareTag(bulletTag))
            {
                //activate
                isActive = true;
            }
        }
    }

    //pause funcs
    protected override void OnPause()
    {
        isPaused = true;
        pauseStartTime = Time.time;
        //if currently moving
        if (isMoving)
        {
            //kill tween
            DOTween.Kill(transform);
            //get tempoary setup time
            tempSetupTime = (setupStartTime + setupTime) - Time.time;
        }
    }

    protected override void OnUnpause()
    {
        isPaused = false;
        pauseEndTime = Time.time;
        //if currently moving
        if (isMoving)
        {
            //resume the tween using temp setup time
            transform.DOMove(locationToSetup, tempSetupTime);
        }
    }
}
