using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireFlicker : MonoBehaviour {

    private Light mLight;
    public Vector2 DelayRange = Vector2.zero;
    public float LowerLightValue = 0.5f;
    public float FlickerSpeed = 0.1f;
    private float DefaultIntensity;
    private float Delay;
    private float LastTime;
    private bool FlickerPlaying = false;
    private bool RestoreIntensity = false;

    // Use this for initialization
    void Start () {
        mLight = GetComponent<Light>();
        Delay = Random.Range(DelayRange.x, DelayRange.y);
        DefaultIntensity = mLight.intensity;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //if(Time.time - LastTime > Delay)
        //      {
        //          FlickerPlaying = true;
        //          Delay = Random.Range(DelayRange.x, DelayRange.y);
        //          LastTime = Time.time;
        //      }
        //      if(FlickerPlaying)
        //      {
        //          FlickerFire();
        //      }
        FlickerFireSine();
	}
    private void FlickerFireSine()
    {
        mLight.intensity = DefaultIntensity - LowerLightValue * Mathf.Sin(Time.time);
    }

    private void FlickerFire()
    {
        //Change the lights value based on the state
        if (!RestoreIntensity && mLight.intensity > LowerLightValue)
        {
            mLight.intensity -= FlickerSpeed * Time.deltaTime;
        }
        else if(RestoreIntensity)
        {
            mLight.intensity += FlickerSpeed * Time.deltaTime;
        }
        //Change the state of the flicker
        if(!RestoreIntensity && mLight.intensity <= LowerLightValue)
        {
            RestoreIntensity = true;
        }
        else if(RestoreIntensity && mLight.intensity >= DefaultIntensity)
        {
            RestoreIntensity = false;
            FlickerPlaying = false;
        }       
    }
}
