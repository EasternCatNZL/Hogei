using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BarnSideMovement : MonoBehaviour {

    [Header("Speed vars")]
    [Tooltip("Max speed barn can move at")]
    public float barnMaxMoveSpeed = 20.0f;
    [Tooltip("Barn accel speed")]
    public float barnAccelSpeed = 2.0f;

    [Header("Rotation control")]
    [Tooltip("Angle rotation limit")]
    [Range(0.0f, 360.0f)]
    public float angleRotationLimit = 40.0f;
    [Tooltip("Angle rotation limit leeway")]
    public float angleRotationLeeway = 10.0f;
    //[Tooltip("Max angle rotation limit")]
    //[Range(0.0f, 360.0f)]
    //public float angleRotationLimitMax = 80.0f;
    //[Tooltip("Min angle rotation limit")]
    //[Range(1.0f, 360.0f)]
    //public float angleRotationLimitMin = 40.0f;
    [Tooltip("Starting direction")]
    [Range(-1, 1)]
    public int startDirection = 1;

    [Header("Tilt control")]
    [Tooltip("Tilt amount")]
    public float tiltAmount = 10.0f;
    [Tooltip("Tilt time")]
    public float tiltTime = 5.0f;

    [Header("Transform refs")]
    [Tooltip("Barn object")]
    public Transform barn;

    [Header("Control vars")]
    [Range(-1, 1)]
    public int direction = 1; //direction rotation is moving in
    public bool isUsing = true; //check if should be using

    //control vars
    private float currentSpeed = 0.0f; //the current speed of motion

	// Use this for initialization
	void Start () {
        direction = startDirection;
        TiltBarn(-direction);
	}
	
	// Update is called once per frame
	void Update () {
        MoveBarn();
        //ChangeDirections();
	}

    //Movement logic
    private void MoveBarn()
    {
        //get the speed of rotation
        currentSpeed += (barnAccelSpeed * Time.deltaTime) * direction;
        currentSpeed = Mathf.Clamp(currentSpeed, -barnMaxMoveSpeed, barnMaxMoveSpeed);
        //trun axis to move barn
        transform.Rotate(transform.up, currentSpeed * Time.deltaTime);
        ChangeDirections();
    }

    //Change directions
    private void ChangeDirections()
    {
        //check too far left
        if(direction == -1
            && transform.rotation.eulerAngles.y - 360.0f <= -angleRotationLimit
            && transform.rotation.eulerAngles.y - 360.0f >= -angleRotationLimit - angleRotationLeeway)
        {
            //move positive
            direction = 1;
            //tilt barn
            TiltBarn(-direction);
        }
        //check too far right
        else if (direction == 1
            && transform.rotation.eulerAngles.y >= angleRotationLimit
            && transform.rotation.eulerAngles.y <= angleRotationLeeway + angleRotationLimit)
        {
            //move negative
            direction = -1;
            //tilt barn
            TiltBarn(-direction);
        }
    }

    //Tilt barn <- do when changing directions
    private void TiltBarn(int direction)
    {
        barn.DORotate(new Vector3(0.0f, transform.rotation.eulerAngles.y, tiltAmount * direction), tiltTime);
    }
}
