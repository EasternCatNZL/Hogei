using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertBullet : MonoBehaviour {

    public GameObject ExplosiveCorn;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        
        if(col.transform.tag == ("Dungeon"))
        {
            Instantiate(ExplosiveCorn, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
        
    }
}
