using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularWaterCurrent : GameEntity {

    [Header("Force vars")]
    [Tooltip("Amount of force applied")]
    public float flowForce = 1.0f;
    [Tooltip("Amount of inward force applied")]
    public float inwardForce = 1.0f;
    [Tooltip("Max speed")]
    public float maxSpeed = 4.0f;
    [Tooltip("Direction of applied force")]
    [Range(-1, 1)]
    public int direction = 1;

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
        FlowAroundCenter();
	}

    //Flow around center
    private void FlowAroundCenter()
    {
        //for all objects in list
        for(int i = 0; i < objectList.Count; i++)
        {
            //check if object exists <- remove if destroyed without leaving trigger
            if (!objectList[i])
            {
                objectList.Remove(objectList[i]);
            }
            else
            {
                //get direction vector from object to center
                Vector3 direction = transform.position - objectList[i].transform.position;
                //remove y change
                direction.y = 0.0f;
                //Get perpendicular vector 
                Vector3 perpVector = Vector3.Cross(direction, Vector3.up).normalized;
                //apply a force to object in direction of perpendicular vector
                objectList[i].GetComponent<Rigidbody>().AddForce(perpVector * flowForce /** Time.deltaTime*/);
                //apply some inward force to prevent being flung out of current
                objectList[i].GetComponent<Rigidbody>().AddForce(direction * inwardForce /** Time.deltaTime*/);
                //if speed has reached max, damp
                if (objectList[i].GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
                {
                    objectList[i].GetComponent<Rigidbody>().velocity = objectList[i].GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
                }
            }
        }
    }

    //Add/Remove objects to list when entering and exiting
    void OnTriggerEnter(Collider other)
    {
        //if enemy
        if (effectEnemy && other.gameObject.CompareTag(enemyTag))
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
