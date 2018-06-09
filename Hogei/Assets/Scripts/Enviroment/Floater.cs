using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {

    public bool UseLocalPosition = false;
    //spin
    public float DegreesPerSec = 0.0f;
    //up/down
    public float yAmp = 0.0f;
    public Vector2 ySpeedRange = Vector2.zero;
    //forward/backlwards
    public float xAmp = 0.0f;
    public Vector2 xSpeedRange = Vector2.zero;
    //left/right
    public float zAmp = 0.0f;
    public Vector2 zSpeedRange = Vector2.zero;
    //Speed variables
    private float ySpeed;
    private float xSpeed;
    private float zSpeed;

    Vector3 PositionOffset;
    Vector3 TempPos;

    // Use this for initialization
    void Start()
    {
        if (UseLocalPosition)
        {
            PositionOffset = transform.localPosition;
        }
        else
        {
            PositionOffset = transform.position;
        }
        //Get random speeds within the range
        ySpeed = Random.Range(ySpeedRange.x, ySpeedRange.y);
        xSpeed = Random.Range(xSpeedRange.x, xSpeedRange.y);
        zSpeed = Random.Range(zSpeedRange.x, zSpeedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (UseLocalPosition)
        {
            transform.Rotate(new Vector3(0.0f, Time.deltaTime * DegreesPerSec, 0.0f), Space.World);
            TempPos = PositionOffset;
            //Set new position based on sine wave
            TempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * ySpeed) * yAmp;
            TempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * xSpeed) * xAmp;
            TempPos.z += Mathf.Sin(Time.fixedTime * Mathf.PI * zSpeed) * zAmp;
            //Set new position
            transform.localPosition = TempPos;
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, Time.deltaTime * DegreesPerSec, 0.0f), Space.World);
            TempPos = PositionOffset;
            //Set new position based on sine wave
            TempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * ySpeed) * yAmp;
            TempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * xSpeed) * xAmp;
            TempPos.z += Mathf.Sin(Time.fixedTime * Mathf.PI * zSpeed) * zAmp;
            //Set new position
            transform.position = TempPos;
        }
        
    }
}
