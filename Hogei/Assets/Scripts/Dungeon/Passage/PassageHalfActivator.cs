using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageHalfActivator : MonoBehaviour {

    [Header("Enemy Spawning Timing Vars")]
    [Tooltip("The delay between an enemy spawning")]
    public float delayBetweenSpawn = 0.2f;

    [Header("Enemys")]
    [Tooltip("The enemy to spawn in")]
    public GameObject enemyObject;
    [Tooltip("The number of the enemy to spawn in")]
    public int numberOfEnemiesToSpawn = 3;

    [Header("Spawn and Exit Points")]
    [Tooltip("Spawn points to spawn enemy in from")]
    public Transform[] spawnPointsArray = new Transform[0];
    [Tooltip("Exit points for enemy clean up")]
    public Transform[] exitPointsArray = new Transform[0];
    [Tooltip("Arrays of waypoints")]
    public GameObject[] waypointHolderArray = new GameObject[0];

    [Header("Tags")]
    [Tooltip("Player tag")]
    public string playerTag = "Player";

    //script refs
    PassageManager passageManager;

    //control vars
    private bool isSpawningEnemies = false; //checks if this half should be spawning in its enemies
    private int numSpawnedEnemies = 0;
    private float lastSpawnTime = 0.0f; //the time that the spawner was activated to begin spawning

	// Use this for initialization
	void Start () {
        passageManager = GetComponentInParent<PassageManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isSpawningEnemies)
        {
            SpawnEnemies();
        }
	}

    //Spawn enemies from spawn points and give them way points to follow
    private void SpawnEnemies()
    {
        //check delay has passed to spawn next enemy
        if(Time.time > lastSpawnTime + delayBetweenSpawn)
        {
            //for each spawn point
            for (int j = 0; j < spawnPointsArray.Length; j++)
            {
                //spawn an enemy in at the spawn point
                GameObject enemyClone = Instantiate(enemyObject, spawnPointsArray[j].transform.position, spawnPointsArray[j].transform.rotation);
                //DEBUG: name enemy for management
                enemyClone.name = enemyObject.name + " Spawner " + j.ToString() + " No." + numSpawnedEnemies.ToString();
                //assign all waypoints onto enemy
                EnemyWaypointManager thisEnemyWaypointManager = enemyClone.GetComponent<EnemyWaypointManager>();
                //first point is spawn point
                thisEnemyWaypointManager.waypointList.Add(spawnPointsArray[j]);
                //next is all waypoints that correspond to this starting point
                for (int k = 0; k < waypointHolderArray[j].GetComponent<WaypointHolder>().waypoints.Length; k++)
                {
                    thisEnemyWaypointManager.waypointList.Add(waypointHolderArray[j].GetComponent<WaypointHolder>().waypoints[k]);
                }
                //last is the exit point
                thisEnemyWaypointManager.waypointList.Add(exitPointsArray[j]);
                //set last spawn time to now
                lastSpawnTime = Time.time;
            }
            //increment number of spawned enemies
            numSpawnedEnemies++;
            //if num of spawned enemies = num of enemies to spawn, stop
            if (numSpawnedEnemies == numberOfEnemiesToSpawn)
            {
                isSpawningEnemies = false;
            }
        }
    }

    //when player enters
    private void OnTriggerEnter(Collider other)
    {
        //only do if passage has yet to be activated
        if (!passageManager.GetIsActivated())
        {
            //check if other is player
            if (other.gameObject.CompareTag(playerTag))
            {
                //set passage to activated
                passageManager.ActivatePassage(true);
                //set spawn time to now
                lastSpawnTime = Time.time;
                //set is spawning to true
                isSpawningEnemies = true;
            }
        }
    }
}
