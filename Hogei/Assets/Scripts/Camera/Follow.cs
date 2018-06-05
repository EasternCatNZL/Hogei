using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Follow : MonoBehaviour
{

    public Transform Target;
    private GameObject Player;
    [Header("Camera Settings")]
    public float CameraDistance;
    public float CameraAngle;
    public float SceneAdjustAngle = -90.0f;
    public Vector3 CameraDirection;
    public float AheadDistance = 1f;
    public float MooseDistance = 0f;
    public float LerpDuration = 1f;
    [Header("Camera Shake Settings")]
    public float ShakeDuration = 1f;
    public float ShakeStrength = 1f;
    public int ShakeVibrate = 10;
    public float ShakeRandomness = 90f;
    [Header("Debuging")]
    public Transform DebugObject;
    private Transform CameraTransform;
    private Vector3 CameraOffset;

    [Header("Input axis")]
    public string rightStickX = "CHorizontalAim";
    public string rightStickY = "CVerticalAim";

    [Header("Transform alignment")]
    public Transform alignment;

    public bool StopFollowing = false;
    // Use this for initialization
    void Start()
    {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        if(Target) transform.position = Target.position;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target != null && !StopFollowing)
        {
            if (Player.GetComponent<WhatCanIDO>().useKeyboard)
            {
                CameraMouseFollow();
            }
            else if (Player.GetComponent<WhatCanIDO>().useController)
            {
                CameraControllerFollow();
            }

            if (Player)
            {
                if (Player.GetComponent<EntityHealth>().isHit == true)
                {
                    transform.DOShakePosition(1, 0.5f);
                    Player.GetComponent<EntityHealth>().isHit = false;
                }
            }
            else
            {
                Player = GameObject.FindGameObjectWithTag("Player");
            }
            //if (DebugObject) DebugObject.position = Vector3.Lerp(Target.position, MousePos, AheadDistance);
            //Adjust the camera
            AdjustCamera();
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                Target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }

    //camera mouse follow
    private void CameraMouseFollow()
    {
        Vector3 MousePos = MouseTarget.GetWorldMousePos();
        Debug.DrawLine(Target.position, Target.position + (MousePos - Target.position).normalized * AheadDistance, Color.green);
        //Vector3 DesiredPos = Vector3.Lerp(Target.position, MousePos, AheadDistance);
        float MouseDistance = Vector3.Distance(Target.position, MousePos);
        //MooseDistance = MouseDistance;
        Vector3 Dir = (MousePos - Target.position).normalized;
        Vector3 DesiredPos = Vector3.zero;
        DesiredPos = Target.position + Vector3.ClampMagnitude(Dir * MouseDistance, AheadDistance);

        //Move the camera towards the desired position
        transform.DOMove(DesiredPos, LerpDuration);
    }

    //camera controller aim follow
    private void CameraControllerFollow()
    {
        //print("X Axis input: " + Luminosity.IO.InputManager.GetAxisRaw(rightStickX));
        //print("Y Axis input: " + Luminosity.IO.InputManager.GetAxisRaw(rightStickY));

        //get direction from sticks
        Vector3 direction = Quaternion.AngleAxis(alignment.rotation.eulerAngles.y, Vector3.up) * new Vector3(Luminosity.IO.InputManager.GetAxis(rightStickX), 0.0f, Luminosity.IO.InputManager.GetAxis(rightStickY));


        //set desired pos
        Vector3 DesiredPos =  (Target.position + (direction * AheadDistance));

        //Move the camera towards the desired position
        transform.DOMove(DesiredPos, LerpDuration);
    }

    void AdjustCamera()
    {
        transform.rotation = Quaternion.Euler(new Vector3(CameraAngle, alignment.rotation.eulerAngles.y, 0f));
        CameraTransform.localPosition = new Vector3(0f, 0f, -CameraDistance);
        
    }

    void CameraShake()
    {
        transform.DOShakePosition(1f, 1f, 10, 0f);
    }

    public void SetStopFollowing(bool _State) { StopFollowing = _State; }
}
