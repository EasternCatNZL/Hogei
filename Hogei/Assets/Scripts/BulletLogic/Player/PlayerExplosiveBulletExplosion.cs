using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosiveBulletExplosion : BulletBehavior {



	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > lifeTime + startTime)
        {
            Destroy(gameObject);
        }
	}

        //collision = deactivate
    protected override void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            //any collision
            if (collision.gameObject.GetComponent<EntityHealth>())
            {
                //decrease the health
                collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);
            }
        }
    }
}
