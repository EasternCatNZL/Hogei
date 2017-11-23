using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageEnemyExitCleanup : MonoBehaviour {

    [Header("Tags")]
    [Tooltip("Tag for enemies in the passage ways")]
    public string passageEnemyTag = "Enemy";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //if other is passageway enemy, destroy it
        if (other.gameObject.CompareTag(passageEnemyTag))
        {
            print("Enemy arrived");
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(passageEnemyTag))
        {
            print("Enemy arrived");
            Destroy(collision.gameObject);
        }
    }
}
