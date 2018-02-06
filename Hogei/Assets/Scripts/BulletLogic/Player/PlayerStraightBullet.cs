using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStraightBullet : MonoBehaviour {

    //script ref
    //private BulletBank bulletBank;

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Explosion vfx")]
    public GameObject explosionVFX;

    //control vars
    private Rigidbody myRigid;
    private bool isActive = false;
    private bool doExpire = false;
    private float travelSpeed = 3.0f;
    private float maxTravelDistance = 0;
    private Vector3 startPos = Vector3.zero;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            myRigid.velocity = transform.forward * travelSpeed;

            CheckExpire();
            
        }
        else
        {
            myRigid.velocity = Vector3.zero;
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

    //set up func
    public void SetupVars(float speed, float travelDist, bool expire)
    {
        isActive = true;
        maxTravelDistance = travelDist;
        doExpire = expire;
        travelSpeed = speed;
        startPos = transform.position;
    }

    ////ref func
    //public void SetBulletBank(BulletBank bank)
    //{
    //    bulletBank = bank;
    //}

    //check if bullet has reached it's max distance
    public void CheckExpire()
    {
        if (doExpire)
        {
            if (Vector3.Distance(transform.position, startPos) > maxTravelDistance)
            {
                Deactivate();
            }
        }
    }

    //deactivate func
    private void Deactivate()
    {
        /*
        //set active to false
        isActive = false;
        //reset values
        myRigid.velocity = Vector3.zero;
        travelSpeed = 0;
        maxTravelDistance = 0;
        doExpire = false;
        startPos = Vector3.zero;
        //return to queue
        bulletBank.ReturnPlayerStraightBullet(gameObject);
        transform.position = bulletBank.transform.position;
        */

        Destroy(gameObject);
    }

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
        }
        //Deactivate();
        GameObject vfxClone = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //Pause events
    void OnPause()
    {
        isActive = false;
    }

    void OnUnpause()
    {
        isActive = true;
    }
}
