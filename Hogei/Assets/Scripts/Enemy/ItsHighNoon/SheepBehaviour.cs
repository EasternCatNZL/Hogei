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
    [HideInInspector]
    public float timeChargeBegan = 0.0f; //time charge up began

    private Rigidbody myRigid; //the rigidbody attached to this object
    [HideInInspector]
    public GameObject target; //the target this object is attacking

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            if (Time.time > timeChargeBegan + chargeTime)
            {
                Move();
            }
            else
            {
                ChargeUp();
            }
        }
	}

    //behaviour during charge up
    private void ChargeUp()
    {
        //look at the target
        transform.LookAt(target.transform.position);
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
}
