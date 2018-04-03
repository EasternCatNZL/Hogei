using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnSheepRoll : MonoBehaviour {

    [Header("Rotate speed")]
    public float rotateSpeed = 60.0f;

	// Use this for initialization
	void Start () {
        //set rotation to something random
        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
	}
	
	// Update is called once per frame
	void Update () {
        //rotate around a world axis
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
