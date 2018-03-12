using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreenEraser : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Bullet tag")]
    public string bulletTag = "Bullet";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //when entering or exiting, destroy 
    //should only collide with enemy bullet layer
    private void OnCollisionEnter(Collision collision)
    {
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
        //check if other object is a bullet
        if (collision.gameObject.CompareTag(bulletTag))
        {
            //remove it
            Destroy(collision.gameObject);
        }
    }
}
