using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePathBullet : BulletBehavior {

    [Header("Sine wave")]
    [Tooltip("Frequency of sine movement")]
    public float frequency = 20.0f;
    [Tooltip("Size of sine movement")]
    public float magnitude = 0.5f;

    //control vars
    private Vector3 linePos; //position relative to line
    private Vector3 directionAxis; //the line bullet travels on

    // Use this for initialization
    void Start () {
        isActive = true;
        startTime = Time.time;
        myRigid = GetComponent<Rigidbody>();
        directionAxis = transform.forward;
        linePos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            WaveMove();
            if (Time.time > startTime + lifeTime + (pauseEndTime - pauseStartTime))
            {
                Destroy(gameObject);
            }
        }
    }

    //Wave movement
    private void WaveMove()
    {
        //move along line
        linePos += transform.right * Time.deltaTime * travelSpeed;
        //move along frequency
        transform.position = linePos + directionAxis * Mathf.Sin((Time.time - startTime) * frequency) * magnitude;
    }
}
