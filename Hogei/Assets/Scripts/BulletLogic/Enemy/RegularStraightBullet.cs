using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularStraightBullet : MonoBehaviour {

    [Header("Speed")]
    [Tooltip("Speed of bullet")]
    public float travelSpeed = 3.0f;

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    //script ref
    //private BulletBank bulletBank;

    private Rigidbody myRigid;
    private bool isActive = false;

	// Use this for initialization
	void Start () {
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            myRigid.velocity = transform.forward * travelSpeed;
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
    public void SetupVars(float speed)
    {
        isActive = true;
        travelSpeed = speed;
        //bulletFireSound.Play();
    }

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
            
        }
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
