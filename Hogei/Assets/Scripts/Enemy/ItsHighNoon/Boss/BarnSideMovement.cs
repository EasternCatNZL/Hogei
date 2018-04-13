using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BarnSideMovement : MonoBehaviour {

    [Header("Speed vars")]
    [Tooltip("Max speed barn can move at")]
    public float barnMaxMoveSpeed = 20.0f;
    [Tooltip("Min speed barn can move at")]
    public float barnMinMoveSpeed = 15.0f;

    [Header("Rotation control")]
    [Tooltip("Max angle rotation limit")]
    [Range(0.0f, 360.0f)]
    public float angleRotationLimitMax = 80.0f;
    [Tooltip("Min angle rotation limit")]
    [Range(1.0f, 360.0f)]
    public float angleRotationLimitMin = 40.0f;
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
        float rotateSpeed = Mathf.Clamp(Random.Range(barnMinMoveSpeed, barnMaxMoveSpeed), barnMinMoveSpeed, barnMaxMoveSpeed);
        //trun axis to move barn
        transform.Rotate(transform.up, (rotateSpeed * direction) * Time.deltaTime);
        ChangeDirections();
    }

    //Change directions
    private void ChangeDirections()
    {
        //check too far left
        if(transform.rotation.eulerAngles.y <= angleRotationLimitMin)
        {
            //move positive
            direction = 1;
            //tilt barn
            TiltBarn(-direction);
        }
        //check too far right
        else if (transform.rotation.eulerAngles.y >= angleRotationLimitMax)
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
