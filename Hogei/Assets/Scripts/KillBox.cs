using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<EntityHealth>())
        {
            other.GetComponent<EntityHealth>().DecreaseHealth(1000);
        }
        else if (!other.CompareTag("Dungeon"))
        {
            Destroy(collision.gameObject);
        }

    }
}
