using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CornShake : MonoBehaviour {

    public float ShakeLength = 1f;
    public float ShakeIntensity = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        this.transform.DOShakeRotation(ShakeLength, ShakeIntensity);
        //Add item drop
    }
}
