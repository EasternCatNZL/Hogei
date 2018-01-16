using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReversalShotBehaviour : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeed = 5.0f;
    [Tooltip("Scale per Bullet asorbed")]
    public float scaleUpValue = 0.1f;
    [Tooltip("Duration of growth per bullet")]
    public float growTime = 0.5f;

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Tags")]
    [Tooltip("Bullet tag")]
    public string bulletTag = "Bullet";

    [Header("Lifetime")]
    [Tooltip("The amount of time in secs object should exist")]
    public float chargeTime = 15.0f;
    //[Tooltip("The start time of the object")]
    private float startTime;

    //control vars
    [Header("TESTING")]
    public float scale = 1.0f;
    public bool canAsorb = true; //true when asorbing bullets to grow bigger
    public bool canDamage = false; //true if bullet is active and can damage
    public bool hasLaunched = false; //check if bullet has been fired
    private Rigidbody myRigid;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > startTime + chargeTime)
        {
            if (!hasLaunched)
            {
                FireShot();
            }
            //just to make sure collisions with enemy bullets doesnt slow it down because physics magic stuff
            else
            {
                myRigid.velocity = transform.forward * bulletTravelSpeed;
            }
        }
        else
        {
            ChangeScale();
        }
    }

    //should only collide with enemy bullet layer
    private void OnCollisionEnter(Collision collision)
    {
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //remove it
            Destroy(collision.gameObject);
            if (canAsorb)
            {
                //print("Scaling up");
                //increase the scale
                scale += scaleUpValue;
            }
        }
        else if (canDamage)
        {
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
            }
            //destroy self when done
            Destroy(gameObject);
        }
    }

    //do even if clearer spawned ontop of bullets
    private void OnCollisionStay(Collision collision)
    {
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //remove it
            Destroy(collision.gameObject);
        }
        else if (canDamage)
        {
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
            }
            //destroy self when done
            Destroy(gameObject);
        }
    }

    //change scale
    private void ChangeScale()
    {
        //grow object
        //transform.DOScale(new Vector3(scale, scale, scale), growTime);
        transform.localScale = new Vector3(scale, scale, scale);
        //increase size of collider
        GetComponent<SphereCollider>().radius = scale / 2;
        //repos vertical to not clip into ground excessively
        transform.position = new Vector3(transform.position.x ,scale, transform.position.z);
    }

    //launch shot
    private void FireShot()
    {
        //Alter bullet properties
        canAsorb = false;
        canDamage = true;
        //increase bullet damage by scale
        bulletDamage += scale;
        //unlock rigidbody restrictions
        myRigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        //begin movement
        myRigid.velocity = transform.forward * bulletTravelSpeed;
        //change to has launched
        hasLaunched = true;
    }
}
