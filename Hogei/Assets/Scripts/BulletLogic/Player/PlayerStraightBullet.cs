using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStraightBullet : BulletBehavior {

    //script ref
    //private BulletBank bulletBank;

    [Header("Explosion vfx")]
    public GameObject explosionVFX;


    [Header("Tags")]
    public string debugTag = "Debugger";

    private Vector3 startPos = Vector3.zero;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            myRigid.velocity = transform.forward * travelSpeed;
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            myRigid.velocity = Vector3.zero;
        }
    }

    //set up func
    public void SetupVars(float speed, float travelDist, bool expire)
    {
        isActive = true;
        travelSpeed = speed;
        startPos = transform.position;
    }

    public void SetupVars(float speed, float travelDist, bool expire, int _bulletDamage)
    {
        isActive = true;
        travelSpeed = speed;
        startPos = transform.position;
        bulletDamage = _bulletDamage;
    }

    public void SetupVars(float speed, float travelDist, bool expire, int _bulletDamage, float life)
    {
        isActive = true;
        travelSpeed = speed;
        startPos = transform.position;
        bulletDamage = _bulletDamage;
        lifeTime = life;
    }

    //deactivate func
    private void Deactivate()
    {
        Destroy(gameObject);
    }

    //collision = deactivate
    protected override void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            //any collision
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                //check if debugger has instakill toggled
                if (GameObject.FindGameObjectWithTag(debugTag))
                {
                    if (GameObject.FindGameObjectWithTag(debugTag).GetComponent<DebugTools>().instakillOn)
                    {
                        collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(collision.gameObject.GetComponent<EntityHealth>().MaxHealth);
                    }
                }
                else
                {
                    collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
                }
            }
            //Deactivate();
            Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    //Pause events
    protected override void OnPause()
    {
        isActive = false;
        pauseStartTime += Time.time;
    }

    protected override void OnUnpause()
    {
        isActive = true;
        pauseEndTime += Time.time;
    }
}
