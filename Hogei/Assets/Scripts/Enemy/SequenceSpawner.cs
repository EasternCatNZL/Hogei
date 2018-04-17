using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSpawner : MonoBehaviour {

    [System.Serializable]
    public struct SpawnGroup
    {
        public SpawnDetails[] spawnDetailsArray; //enemies to spawn 
        public float spawnTiming;
    }

    [System.Serializable]
    public struct SpawnDetails
    {
        public GameObject enemyObject; //the enemy object
        public Vector3 spawnLocationLocal; //spawn location, local to parent
        public float startRot; //starting rotation
    }

    [Header("Spawn groups")]
    public SpawnGroup[] spawnGroupsArray = new SpawnGroup[0];

    [Header("Tags")]
    public string playerTag = "Player";

    //control vars
    private bool isTriggered = false; //checks to see if triggered

    private int currentGroupIndex = 0; //the current index of array

    private float lastSpawnTime = 0.0f; //the time last spawn occured


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            if (Time.time > lastSpawnTime + spawnGroupsArray[currentGroupIndex].spawnTiming)
            {
                SpawnSet();
            }
        }

	}

    //Spawn enemies in sequence based on timer
    private void SpawnSet()
    {
        //for all in current spawn group
        for (int i = 0; i < spawnGroupsArray[currentGroupIndex].spawnDetailsArray.Length; i++)
        {
            //get a spawn location based on given position in details from self
            Vector3 spawnLoc = transform.position + spawnGroupsArray[currentGroupIndex].spawnDetailsArray[i].spawnLocationLocal;
            //get the rotation
            Quaternion rot = Quaternion.Euler(0.0f, spawnGroupsArray[currentGroupIndex].spawnDetailsArray[i].startRot, 0.0f);
            //spawn the object
            GameObject enemyClone = Instantiate(spawnGroupsArray[currentGroupIndex].spawnDetailsArray[i].enemyObject, spawnLoc, rot);

            //activate the enemy
            enemyClone.GetComponent<EnemyBehavior>().isActive = true;
        }
        //set timing
        lastSpawnTime = Time.time;
        //increment the current index
        currentGroupIndex++;
        //check if group index has exceeded group array size
        if(currentGroupIndex >= spawnGroupsArray.Length)
        {
            //destroy self
            Destroy(gameObject);
        }
    }

    //on trigger enter
    private void OnTriggerEnter(Collider other)
    {
        //check if not triggered, for player
        if(!isTriggered && other.gameObject.CompareTag(playerTag))
        {
            //set triggered to true
            isTriggered = true;
            //set timing
            lastSpawnTime = Time.time;
        }
    }
}
