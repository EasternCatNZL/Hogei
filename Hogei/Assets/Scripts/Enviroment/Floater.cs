using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {

    //spin
    public float DegreesPerSec = 0.0f;
    //up/down
    public float yAmp = 0.0f;
    public float ySpeed = 0.0f;
    //forward/backlwards
    public float xAmp = 0.0f;
    public float xSpeed = 0.0f;
    //left/right
    public float zAmp = 0.0f;
    public float zSpeed = 0.0f;

    Vector3 PositionOffset;
    Vector3 TempPos;

    // Use this for initialization
    void Start()
    {
        PositionOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(new Vector3(0.0f, Time.deltaTime * DegreesPerSec, 0.0f), Space.World);
        TempPos = PositionOffset;
        TempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * ySpeed) * yAmp;
        TempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * xSpeed) * xAmp;
        TempPos.z += Mathf.Sin(Time.fixedTime * Mathf.PI * zSpeed) * zAmp;
        transform.position = TempPos;
        
    }
}
