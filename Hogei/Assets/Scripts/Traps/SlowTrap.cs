using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : GameEntity {

    [Tooltip("Percentage slowed by")]
    [Range(0f,1f)]
    public float SlowPercentage = 0.0f;

    [Tooltip("Lifetime if expires")]
    public float lifetime = 0.0f;

    [Tooltip("Effect enemies?")]
    public bool effectEnemy = false;
    [Tooltip("Expires?")]
    public bool expires = false;

    [Header("Tags")]
    public string playerTag = "Player";
    public string enemyTag = "Enemy";

    //control vars
    private float startTime = 0.0f; //the time this trap began existing

    private List<GameObject> entitiesInTrapList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (expires)
        {
            if(Time.time > lifetime + startTime)
            {
                RemoveTrapEffects();
                Destroy(gameObject);
            }
        }
	}

    private void RemoveTrapEffects()
    {
        //for all enemies that were in trap
        for(int i = 0; i < entitiesInTrapList.Count; i++)
        {
            if (entitiesInTrapList[i].CompareTag(playerTag))
            {
                entitiesInTrapList[i].GetComponent<PlayerController>().SetSpeedModifier(1f);
            }
            else if (entitiesInTrapList[i].CompareTag(enemyTag))
            {
                //check script exists
                if (entitiesInTrapList[i].GetComponent<EnemyState>())
                {
                    entitiesInTrapList[i].GetComponent<EnemyState>().isSlowed = false;
                    entitiesInTrapList[i].GetComponent<EnemyState>().slowModifier = 1.0f;
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {     
        if(other.gameObject.tag.Equals(playerTag))
        {
            other.gameObject.GetComponent<PlayerController>().SetSpeedModifier(1f - SlowPercentage);
            entitiesInTrapList.Add(other.gameObject);
        }
        else if (other.gameObject.CompareTag(enemyTag))
        {
            //check script exists
            if (other.gameObject.GetComponent<EnemyState>())
            {
                other.gameObject.GetComponent<EnemyState>().isSlowed = true;
                other.gameObject.GetComponent<EnemyState>().slowModifier = (1f - SlowPercentage);
                entitiesInTrapList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            other.gameObject.GetComponent<PlayerController>().SetSpeedModifier(1f);
            entitiesInTrapList.Remove(other.gameObject);
        }
        else if (other.gameObject.CompareTag(enemyTag))
        {
            //check script exists
            if (other.gameObject.GetComponent<EnemyState>())
            {
                other.gameObject.GetComponent<EnemyState>().isSlowed = false;
                other.gameObject.GetComponent<EnemyState>().slowModifier = 1.0f;
                entitiesInTrapList.Remove(other.gameObject);
            }
        }
    }
}
