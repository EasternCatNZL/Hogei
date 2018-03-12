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
    public Vector3 CameraDirection;
    public float AheadDistance = 1f;
    public float LerpDuration;
    [Header("Camera Shake Settings")]
    public float ShakeDuration = 1f;
    public float ShakeStrength = 1f;
    public int ShakeVibrate = 10;
    public float ShakeRandomness = 90f;
    [Header("Debuging")]
    public Transform DebugObject;
    private Transform CameraTransform;
    private Vector3 CameraOffset;
    // Use this for initialization
    void Start()
    {
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        transform.position = Target.position;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 MousePos = MouseTarget.GetWorldMousePos();
            Debug.DrawLine(Target.position, Target.position + (MousePos - Target.position).normalized * AheadDistance, Color.green);
            //Vector3 DesiredPos = Vector3.Lerp(Target.position, MousePos, AheadDistance);
            Vector3 DesiredPos = Target.position + (MousePos - Target.position).normalized * AheadDistance;
            Vector3 Dir = DesiredPos - transform.position;
            //Move the camera towards the desired position
            transform.DOMove(DesiredPos, LerpDuration);
            if(Player.GetComponent<PlayerAttack>().isShooting == true)
            {
                transform.DOShakePosition(1, 0.1f);            
            }
            if(DebugObject) DebugObject.position = Vector3.Lerp(Target.position, MousePos, AheadDistance);
            //Adjust the camera
            AdjustCamera();
        }
    }

    void AdjustCamera()
    {
        transform.rotation = Quaternion.Euler(new Vector3(CameraAngle, -90f, 0f));
        CameraTransform.localPosition = new Vector3(0f, 0f, -CameraDistance);
        
    }

    void CameraShake()
    {
        transform.DOShakePosition(1f, 1f, 10, 0f);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
       
    }
}
