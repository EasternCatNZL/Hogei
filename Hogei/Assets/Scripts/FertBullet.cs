using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertBullet : MonoBehaviour {

    public GameObject ExplosiveCorn;
    public Vector3 CornSpawnOffset;

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
            Vector3 Direction = transform.position - col.contacts[0].point;
            Instantiate(ExplosiveCorn, transform.position + CornSpawnOffset, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
        
    }
}
