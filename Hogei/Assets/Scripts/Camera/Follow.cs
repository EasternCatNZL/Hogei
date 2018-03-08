using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Follow : MonoBehaviour
{

    public Transform Target;
    public Vector3 FollowOffset;
    public Vector3 CameraDirection;
    public float Speed = 1f;
    public float AheadDistance;
    private Transform CameraTransform;
    private Vector3 CameraOffset;
    // Use this for initialization
    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if(Player)
        {
            transform.parent = Player.transform;
        }
        else
        {
            Debug.Log(System.DateTime.Now + ": Can not find the player object " + gameObject.name);
        }
        CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 MousePos = MouseTarget.GetWorldMousePos();
            Vector3 DesiredPos = Vector3.Lerp(Target.position, MousePos, 0.2f);
            Vector3 Dir = DesiredPos - transform.position;          
            Vector3 Velocity = Dir.normalized * Speed * Time.deltaTime;
            
            print(Velocity.ToString());
            transform.position = transform.position + Velocity;



            //transform.position += Vector3.Scale(CameraDirection, FollowOffset);
        }
    }
}
