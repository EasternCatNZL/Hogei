using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClearer : MonoBehaviour {

    [Header("Clearer object")]
    [Tooltip("The object who's collision box is used for clearing")]
    public GameObject clearerObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseAbility()
    {
        Instantiate(clearerObject, transform.position, transform.rotation);
    }
}
