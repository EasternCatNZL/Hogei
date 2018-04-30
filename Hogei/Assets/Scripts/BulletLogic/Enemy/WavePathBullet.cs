using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePathBullet : MonoBehaviour {

    [Header("Speed")]
    [Tooltip("Speed of bullet")]
    public float travelSpeed = 3.0f;

    [Header("Sine wave")]
    [Tooltip("Frequency of sine movement")]
    public float frequency = 20.0f;
    [Tooltip("Size of sine movement")]
    public float magnitude = 0.5f;

    [Header("Damage")]
    [Tooltip("Damage dealt by bullet")]
    public float bulletDamage = 1.0f;

    [Header("Lifetime")]
    [Tooltip("Lifetime of the bullet")]
    public float lifeTime = 5.0f;

    //control vars
    private float startTime = 0.0f;
    private float pauseStartTime = 0.0f;
    private float pauseEndTime = 0.0f;

    private Rigidbody myRigid;

    private bool isActive = false;

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
