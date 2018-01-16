using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectShield : MonoBehaviour {

    [Header("Clearer object")]
    [Tooltip("The shield object")]
    public GameObject shieldObject;

    [Header("Distance")]
    [Tooltip("Distance ahead to spawn shield")]
    public float distance = 4.0f;

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
        GameObject shieldClone = Instantiate(shieldObject, bitAhead, transform.rotation);
    }
}
