using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGroupTrigger : MonoBehaviour {

    [Header("Enemy Group")]
    [Tooltip("Enemy array")]
    public GameObject[] enemyGroupArray = new GameObject[0];

    [Header("Tags")]
    public string targetTag = "Player";

    //control vars
    private bool hasSetup = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //trigger enter
    private void OnTriggerEnter(Collider other)
    {
        //check other
        if (other.gameObject.CompareTag(targetTag) && !hasSetup)
        {
            //for all units in array
            for (int i = 0; i < enemyGroupArray.Length; i++)
            {
                enemyGroupArray[i].GetComponent<ChickenBehavior>().SetUp(other.gameObject);
                hasSetup = true;
            }
        }
    }
}
