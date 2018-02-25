using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGlobe : MonoBehaviour {

    GameObject Player;
    public float HealthIncrease = 1.0f;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Player.GetComponent<EntityHealth>().IncreaseHealth(HealthIncrease);
            Destroy(gameObject);
        }
    }
}
