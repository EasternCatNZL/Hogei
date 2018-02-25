using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

    public GameObject[] itemDrop;

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(GetComponent<EntityHealth>().CurrentHealth <= 0.0f)
        {
            Vector3 currentPosition = transform.position;
            foreach(GameObject item in itemDrop)
            {
                Instantiate(item, currentPosition + new Vector3(Random.Range(0.0f, 5.0f), 0.0f, Random.Range(0.0f, 5.0f)), Quaternion.identity);
            }
            Destroy(gameObject);
        }
		
	}
}
