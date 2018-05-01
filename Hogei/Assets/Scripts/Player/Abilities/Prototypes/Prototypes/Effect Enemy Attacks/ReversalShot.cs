using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversalShot : MonoBehaviour {

    [Header("Clearer object")]
    [Tooltip("The shot object")]
    public GameObject shotObject;

    [Header("Distance")]
    [Tooltip("Distance ahead to spawn shot")]
    public float distance = 4.0f;
    [Tooltip("Vertical repositioning")]
    public float verticalRepos = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseAbility()
    {
        //get a vector just ahead of player to spawn shield
        Vector3 bitAhead = transform.position + (transform.forward * distance);
        //alter vertical
        bitAhead += new Vector3(0.0f, verticalRepos, 0.0f);
        Instantiate(shotObject, bitAhead, transform.rotation);
    }
}
