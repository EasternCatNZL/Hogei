using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSpawner : MonoBehaviour
{

    [System.Serializable]
    public struct SpawnGroup
    {
        public SpawnDetails[] spawnDetailsArray; //enemies to spawn 
        public float spawnTiming;
    }

    [System.Serializable]
    public struct SpawnDetails
    {
        [Tooltip("The enemy that will be spawned")]
        public GameObject enemyObject; //the enemy object
        [Tooltip("The vector position for the spawn relative to the parent gameobject")]
        public Vector3 spawnLocationLocal; //spawn location, local to parent
        [Tooltip("The transform that the enemies will spawn from (!Takes priority over the vector spawn position!)")]
        public Transform spawnTransform; //Transform of a spawn point
        [Tooltip("If less than 0 the transforms rotation will be used")]
        public float startRot; //starting rotation
    }
    
    public bool DestroySelf = true;
    [Header("Spawn groups")]
    public SpawnGroup[] spawnGroupsArray = new SpawnGroup[0];

    [Header("Gate ref")]
    [Tooltip("Ref to gate for this spawner")]
    public GateManager gate;
    public ExitTrigger anchor;

    [Header("Tags")]
    public string playerTag = "Player";

    //control vars
    public bool isTriggered = false; //checks to see if triggered

    private int currentGroupIndex = 0; //the current index of array

    private float lastSpawnTime = 0.0f; //the time last spawn occured


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (currentGroupIndex < spawnGroupsArray.Length && Time.time > lastSpawnTime + spawnGroupsArray[currentGroupIndex].spawnTiming)
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
            //Store the Spawn Details in a temp variables
            SpawnDetails TempDets = spawnGroupsArray[currentGroupIndex].spawnDetailsArray[i];
            //Get the spawn location
            Vector3 spawnLoc = Vector3.zero;

            if (TempDets.spawnTransform != null)//If there exists a transform use it to get the spawn position
            {
                spawnLoc = TempDets.spawnTransform.position;
            }
            else//Otherwise use the vector to set the position relative to the parent gameobject
            {
                spawnLoc = transform.position + TempDets.spawnLocationLocal;
            }
            //get a spawn location based on given position in details from self

            //get the rotation
            Quaternion rot = Quaternion.identity;

            if(TempDets.spawnTransform != null)//If the transform exists then use its rotation for the spawn rotation
            {
                rot = TempDets.spawnTransform.rotation;  
            }
            else//Otherwise use the start rotation
            {
                rot = Quaternion.Euler(0.0f, TempDets.startRot, 0.0f);
            }
            //spawn the object
            GameObject enemyClone = Instantiate(spawnGroupsArray[currentGroupIndex].spawnDetailsArray[i].enemyObject, spawnLoc, rot);

            //activate the enemy <- if spawning enemybehavior in child, check into child
            if (enemyClone.GetComponent<EnemyBehavior>())
            {
                enemyClone.GetComponent<EnemyBehavior>().isActive = true;
                enemyClone.GetComponent<EnemyBehavior>().Activate();
            }
            else if (enemyClone.GetComponentInChildren<EnemyBehavior>())
            {
                enemyClone.GetComponentInChildren<EnemyBehavior>().isActive = true;
            }

            //add to gate
            if (gate)
            {
                gate.enemyList.Add(enemyClone);
            }

        }
        //set timing
        lastSpawnTime = Time.time;
        //increment the current index
        currentGroupIndex++;
        //check if group index has exceeded group array size
        if (DestroySelf && currentGroupIndex >= spawnGroupsArray.Length)
        {
            if (anchor)
            {
                anchor.DropAnchor();
            }
            //destroy self
            Destroy(gameObject);
        }
    }

    //on trigger enter
    private void OnTriggerEnter(Collider other)
    {
        //check if not triggered, for player
        if (!isTriggered && other.gameObject.CompareTag(playerTag))
        {
            //set triggered to true
            isTriggered = true;
            //set timing
            lastSpawnTime = Time.time;
        }
    }
}
