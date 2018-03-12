using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClearerBehaviour : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Bullet tag")]
    public string bulletTag = "Bullet";

    [Header("Lifetime")]
    [Tooltip("The amount of time in secs object should exist")]
    public float lifeTime;
    //[Tooltip("The start time of the object")]
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
	}

    //should only collide with enemy bullet layer
    private void OnCollisionEnter(Collision collision)
    {
        print("Entered: " + collision.gameObject.name);
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //remove it
            Destroy(collision.gameObject);
        }
    }

    //do even if clearer spawned ontop of bullets
    private void OnCollisionExit(Collision collision)
    {
        print("Exited: " + collision.gameObject.name);
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //remove it
            Destroy(collision.gameObject);
        }
    }

    //should only collide with enemy bullet layer
    private void OnTriggerEnter(Collider collision)
    {
        print("Entered: " + collision.gameObject.name);
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //remove it
            Destroy(collision.gameObject);
        }
    }

    //do even if clearer spawned ontop of bullets
    private void OnTriggerExit(Collider collision)
    {
        print("Exited: " + collision.gameObject.name);
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //remove it
            Destroy(collision.gameObject);
        }
    }
}
