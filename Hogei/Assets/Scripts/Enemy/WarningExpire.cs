using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningExpire : MonoBehaviour {

    float timer = 0.0f;
    float WarningTime = 0.5f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > WarningTime)
        {
            Destroy(gameObject);
        }
	}
}
