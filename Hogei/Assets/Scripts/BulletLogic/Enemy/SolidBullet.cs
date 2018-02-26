using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBullet : MonoBehaviour {

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //collision = deactivate
    private void OnCollisionEnter(Collision collision)
    {
        //any collision
        if (collision.gameObject.GetComponent<EntityHealth>())
        {
            collision.gameObject.GetComponent<EntityHealth>().DecreaseHealth(bulletDamage);

        }
        //Destroy(gameObject);
    }
}
