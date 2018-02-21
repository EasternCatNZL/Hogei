using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSurprise : MonoBehaviour {

    [Header("Spawn locations")]
    [Tooltip("Array of spawn locations")]
    public Transform[] spawnLocationArray = new Transform[0];

    [Header("Timing vars")]
    [Tooltip("Time after spawning till attack begins")]
    public float attackBeginDelay = 2.0f;

    [Header("Cactus object")]
    [Tooltip("The cactus object")]
    public GameObject cactusObject;

    [Header("Tags")]
    public string targetTag = "Player";

    //control vars
    private bool isActivated = false; //check if group has been activated
    private bool isPaused = false; //check if pause has been called
    private float timeActivated = 0.0f; //the time trigger was activated
    private float pauseStartTime = 0.0f; //time pause start was called
    private float pauseEndTime = 0.0f; //time pause end was called

    private GameObject[] cactusArray;

	// Use this for initialization
	void Start () {
        cactusArray = new GameObject[spawnLocationArray.Length];
	}
	
	// Update is called once per frame
	void Update () {
        if (isActivated && !isPaused)
        {
            if (Time.time > timeActivated + attackBeginDelay + (pauseEndTime - pauseStartTime))
            {
                ActivateCactus();
            }
        }
	}

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    //Setup function
    private void Setup()
    {
        //for the number of spawn points
        for (int i = 0; i < spawnLocationArray.Length; i++)
        {
            //spawn in a cactus object
            GameObject cactusClone = Instantiate(cactusObject, spawnLocationArray[i].position, spawnLocationArray[i].rotation);
            cactusArray[i] = cactusClone;
            //cactusClone.GetComponent<CactusRandomSpray>().isActive = true;
        }
        //set activated time to now
        timeActivated = Time.time;
    }

    //Activate cactus
    private void ActivateCactus()
    {
        //for all cactus
        for (int i = 0; i < cactusArray.Length; i++)
        {
            cactusArray[i].GetComponent<CactusRandomSpray>().isActive = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //check other
        if (other.gameObject.CompareTag(targetTag) && !isActivated)
        {
            Setup();
            //change has setup to true
            isActivated = true;
        }
    }

    //pause funcs
    void OnPause()
    {
        isPaused = true;
        pauseStartTime += Time.time;
    }

    void OnUnpause()
    {
        isPaused = false;
        pauseEndTime += Time.time;
    }
}
