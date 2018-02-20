using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSurprise : MonoBehaviour {

    [Header("Spawn locations")]
    [Tooltip("Array of spawn locations")]
    public Transform[] spawnLocationArray = new Transform[0];

    [Header("Cactus object")]
    [Tooltip("The cactus object")]
    public GameObject cactusObject;

    [Header("Tags")]
    public string targetTag = "Player";

    //control vars
    private bool isActivated = false;

    private GameObject[] cactusArray = new GameObject[0];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
