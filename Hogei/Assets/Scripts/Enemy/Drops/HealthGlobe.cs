using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthGlobe : MonoBehaviour {

    GameObject Player;
    public float HealthIncrease = 1.0f;
    public float rotationSpeed = 1.0f * Time.deltaTime;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        transform.DOJump(transform.position, 0.8f, 1, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * rotationSpeed);
		
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
