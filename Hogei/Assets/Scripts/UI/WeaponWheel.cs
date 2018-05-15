using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponWheel : MonoBehaviour {

    public float TweenDuration = 1.0f;
    private Vector3 endValue;
    private Vector3 currentRotation;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        	
	}

    public void NextWeapon()
    {
        
        currentRotation = transform.localEulerAngles;
        endValue = currentRotation + new Vector3(0, 180, 0);
        transform.DORotate(endValue, TweenDuration, RotateMode.Fast);
        
    }
}
