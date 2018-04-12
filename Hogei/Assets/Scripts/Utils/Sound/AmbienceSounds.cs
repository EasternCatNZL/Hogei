using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSounds : MonoBehaviour {

    public AudioSource Source = null;
    [Tooltip("Delay to wait after the sounds has finished")]
    public Vector2 DelayRange = Vector2.zero;
    private float Delay = 0f;
    private float LastTime = 0f;
    	
	// Update is called once per frame
	void Update () {
		if(Time.time > LastTime + Delay)
        {
            Source.Play();
            LastTime = Time.time;
            Delay = Source.clip.length + Random.Range(DelayRange.x, DelayRange.y);
        }
	}
}
