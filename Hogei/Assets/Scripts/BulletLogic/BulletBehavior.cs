using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : GameEntity {

    [Header("Speed")]
    [Tooltip("Speed of bullet")]
    public float travelSpeed = 3.0f;

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Lifetime")]
    [Tooltip("Lifetime of the bullet")]
    public float lifeTime = 5.0f;

    [Header("Particle effect")]
    [Tooltip("Particle emitted by bullet on impact")]
    public GameObject particleObject;

    //control vars
    protected float startTime = 0.0f;
    protected Rigidbody myRigid;
    protected bool isActive = true;
    protected bool isPaused = false;
    protected float pauseStartTime = 0.0f;
    protected float pauseEndTime = 0.0f;


 //   // Use this for initialization
 //   void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    protected void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    protected void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    //collision = deactivate
    protected virtual void OnTriggerEnter(Collider collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);

        }
        //create particle
        if (particleObject)
        {
            Instantiate(particleObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    protected virtual void OnPause()
    {

    }

    protected virtual void OnUnpause()
    {

    }
}
