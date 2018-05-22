using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosiveBullet : BulletBehavior {

    [Header("Explosion object")]
    public GameObject explosionObject;

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

    public void SetupVars(float speed, float travelDist, bool expire, int _bulletDamage)
    {
        isActive = true;
        travelSpeed = speed;
        bulletDamage = _bulletDamage;
    }

    //create explosion
    private void CreateExplosion()
    {
        //create object in place of bullet collision
        GameObject explosion = Instantiate(explosionObject, transform.position, transform.rotation);
    }

    //collision = deactivate
    protected override void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            //any collision
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                ////check if debugger has instakill toggled
                //if (GameObject.FindGameObjectWithTag(debugTag))
                //{
                //    if (GameObject.FindGameObjectWithTag(debugTag).GetComponent<DebugTools>().instakillOn)
                //    {
                //        collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(collision.gameObject.GetComponent<EntityHealth>().MaxHealth);
                //    }
                //}
                //else
                //{
                    collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
                //}
            }
            print(collision.gameObject.name);
            CreateExplosion();
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
