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

    [Header("Lifetime")]
    [Tooltip("Lifetime of the bullet")]
    public float lifeTime = 5.0f;

    [Header("Tags")]
    public string debugTag = "Debugger";

    //control vars
    private Rigidbody myRigid;
    private bool isActive = false;
    private float travelSpeed = 3.0f;
    private float startTime = 0.0f;
    private float pauseStartTime = 0.0f;
    private float pauseEndTime = 0.0f;
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
        travelSpeed = speed;
        startPos = transform.position;
    }

    //deactivate func
    private void Deactivate()
    {
        Destroy(gameObject);
    }

    //collision = deactivate
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.name);
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
            GameObject vfxClone = Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    //Pause events
    void OnPause()
    {
        isActive = false;
        pauseStartTime += Time.time;
    }

    void OnUnpause()
    {
        isActive = true;
        pauseEndTime += Time.time;
    }
}
