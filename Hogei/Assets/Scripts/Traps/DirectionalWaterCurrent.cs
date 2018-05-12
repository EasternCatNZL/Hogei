using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalWaterCurrent : GameEntity
{

    [Header("Force vars")]
    [Tooltip("Amount of force applied")]
    public float flowForce = 1.0f;
    [Tooltip("Max speed")]
    public float maxSpeed = 4.0f;

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
        FlowForward();
	}

    //Flow along direction
    private void FlowForward()
    {
        //for all objects in list
        for (int i = 0; i < objectList.Count; i++)
        {
            //check if object exists <- remove if destroyed without leaving trigger
            if (!objectList[i])
            {
                objectList.Remove(objectList[i]);
            }
            else
            {
                //apply a force to object in direction of perpendicular vector
                objectList[i].GetComponent<Rigidbody>().AddForce(transform.forward * flowForce /** Time.deltaTime*/);
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
