using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tornado : MonoBehaviour {

    public float Knockback = 1.0f;

    /*
    public float force = 1.0f;
    private bool hit = false;
    private float timer = 0.0f;
    public float forceTime = 0.5f;

    public GameObject Player;

    Vector3 PlayerPos;
    */
    //public float radius = 1.0f;

    // Use this for initialization
    void Start () {        
		
	}
	
	// Update is called once per frame
	void Update () {

        /*
        if(hit == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                hit = false;
                Player.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            }

        }
        */
		
	}
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * force);
        }

    }
    */
    /*
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
    }
    */

    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Vector3 HitPos = col.contacts[0].point;
            Vector3 Pos = col.transform.position;
            Vector3 Temp = Pos - HitPos;
            Temp.Scale(new Vector3(1, 0, 1));

            col.rigidbody.DOJump(col.transform.position - Temp * -Knockback, 1.0f, 0, 0.5f, false);
            
            
            //PlayerPos = col.gameObject.transform.position;

            
            /*
            hit = true;
            timer = forceTime;
            col.rigidbody.AddForce(-transform.forward * force, ForceMode.VelocityChange);
            */
            /*
            col.rigidbody.AddForce(-transform.forward * force, ForceMode.VelocityChange);
                                    
            // calculate angle between collision and player
            Vector3 direction = col.contacts[0].point - transform.position;
            // get opposite
            direction = -direction.normalized;
            //add force
            col.rigidbody.AddForce(direction * force);
            */
            //col.rigidbody.AddForce(-transform.forward * force);
            //col.gameObject.transform.DOPunchPosition();

            //Vector3 explosionPos = transform.position;
            //col.rigidbody.AddExplosionForce(force, explosionPos, radius, 1.0f);
        }
    }
    
}
