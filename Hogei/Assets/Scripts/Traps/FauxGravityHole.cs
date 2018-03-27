using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityHole : MonoBehaviour {

    [Header("Faux gravity force")]
    [Tooltip("Amount of force faux gravity exerts")]
    public float fauxForce = 10.0f;
    [Tooltip("Max force")]
    public float maxForce = 10.0f;
    [Tooltip("Min force")]
    public float minForce = 1.0f;

    [Header("Effected Objects Tags")]
    public string playerTag = "Player";
    public string enemyTag = "Enemy";

    [Header("Control Utils")]
    public bool effectEnemy = true;

    //lists
    public List<GameObject> objectList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ApplyFauxGravity();
	}

    //Apply faux gravity force to all objects in list
    private void ApplyFauxGravity()
    {
        //for all objects in list
        for(int i = 0; i < objectList.Count; i++)
        {
            //get the direction from object to center
            Vector3 directionToCenter = transform.position - objectList[i].transform.position;
            //get distance
            float distance = directionToCenter.magnitude;
            //remove y change
            directionToCenter.y = 0.0f;
            //get the amount of force to apply
            float forceToApply = Mathf.Clamp(fauxForce - distance, minForce, maxForce);

            //apply directional force to object
            objectList[i].GetComponent<Rigidbody>().AddForce(directionToCenter * forceToApply);
        }
    }

    //Add/Remove objects to list when entering and exiting
    void OnTriggerEnter(Collider other)
    {
        //if enemy
        if(effectEnemy && other.gameObject.CompareTag(enemyTag))
        {
            objectList.Add(other.gameObject);
        }
        //if player
        if (other.gameObject.CompareTag(playerTag))
        {
            objectList.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //check if that object existed in list
        if (objectList.Contains(other.gameObject))
        {
            objectList.Remove(other.gameObject);
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
