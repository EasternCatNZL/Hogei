using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RedirectPulse : MonoBehaviour {

    public Transform Target;
    public float RedirectSpeed = 3f;
    public float ScaleEndValue;
    public float ScaleDuration;

	// Use this for initialization
	void Start () {
        if(!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        Pulse();
	}
	
    public void Pulse()
    {
        transform.DOScale(ScaleEndValue, ScaleDuration);
        Destroy(gameObject, ScaleDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.LookAt(Target);
        other.GetComponent<RegularStraightBullet>().travelSpeed = RedirectSpeed;
    }
}
