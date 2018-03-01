using DG.Tweening;
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
        		
	}

    public void DropItem()
    {
        Vector3 currentPosition = transform.position;
        Instantiate(itemDrop[0], currentPosition, Quaternion.identity);
    }

    /*
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            DropItem();
        }
    }
    */

    public void OnDeathDrop()
    {
        if (GetComponent<EntityHealth>().CurrentHealth <= 0.0f)
        {
            Vector3 currentPosition = transform.position;
            foreach (GameObject item in itemDrop)
            {
                Instantiate(item, currentPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }       
}
