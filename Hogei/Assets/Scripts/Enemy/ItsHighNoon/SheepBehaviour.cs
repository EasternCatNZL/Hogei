using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour {

    [Header("Timing vars")]
    [Tooltip("Charge up time")]
    public float chargeTime = 2.0f;

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
    private bool isPaused = false; //checks if pause has been called
    [HideInInspector]
    public float timeChargeBegan = 0.0f; //time charge up began
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
            if (Time.time > timeChargeBegan + chargeTime + (pauseEndTime - pauseStartTime))
            {
                Move();
            }
            else
            {
                ChargeUp();
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

    //behaviour during charge up
    private void ChargeUp()
    {
        //look at the target
        transform.LookAt(target.transform.position);
        //remove any x and z change
        Quaternion newRotation = new Quaternion();
        newRotation.eulerAngles = new Vector3(0.0f, transform.rotation.y, 0.0f);
        transform.rotation = newRotation;
    }

    //move
    private void Move()
    {
        myRigid.velocity = transform.forward * chargeSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(damage);
            //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
        }
        //GameObject particle = Instantiate(particleObject, transform.position, Quaternion.identity);
        //Deactivate();
        Destroy(gameObject);
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
