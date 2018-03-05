using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    [Header("Animation")]
    public Transform UpperBody;
    public Transform LowerBody;
    Vector3 UpperBodyAim;
    Vector3 LowerBodyAim;
    public Transform DebuggerAnimSphere;

    [Header("Movement")]
    public Transform MovementAlignment;
    Vector3 MovementDirection;
    Vector3 Movement;

    private Rigidbody Rigid;

    [Header("Speed")]
    public float Speed;
    public float SpeedModifier;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    void Init()
    {
        UpperBodyAim = transform.forward;
        LowerBodyAim = UpperBodyAim;
        Rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(UpperBody.position, UpperBodyAim * 3, Color.red);
        Debug.DrawRay(LowerBody.position, LowerBodyAim * 3, Color.blue);
        Debug.DrawRay(LowerBody.position, MovementDirection * 3, Color.green);
        MovementUpdate();
        UpperBodyUpdate();
        LowerBodyUpdate();
    }

    void UpperBodyUpdate()
    {
        //Aim the upper body
        Vector3 AimDirection = MouseTarget.GetWorldMousePos() - UpperBody.position;
        if (Vector3.Dot(UpperBody.right, AimDirection) < 0.0f)
        {
            UpperBody.Rotate(0.0f, -Vector3.Angle(UpperBody.forward, AimDirection), 0.0f);
        }
        if (Vector3.Dot(UpperBody.right, AimDirection) > 0.0f)
        {
            UpperBody.Rotate(0.0f, Vector3.Angle(UpperBody.forward, AimDirection), 0.0f);
        }
        UpperBodyAim = UpperBody.forward;
    }

    void LowerBodyUpdate()
    {
        //Aim the lower body
        float AngleDiff = Vector3.Angle(UpperBodyAim, LowerBodyAim);
        if (Mathf.Abs(AngleDiff) > 60f)
        {
            if (Vector3.Dot(LowerBody.right, UpperBodyAim) < 0.0f)
            {
                LowerBody.Rotate(0.0f, -Vector3.Angle(LowerBody.forward, UpperBodyAim), 0.0f);
            }
            if (Vector3.Dot(LowerBody.right, UpperBodyAim) > 0.0f)
            {
                LowerBody.Rotate(0.0f, Vector3.Angle(LowerBody.forward, UpperBodyAim), 0.0f);
            }
        }
        LowerBodyAim = LowerBody.forward;

        //Calculate Animation
        AngleDiff = Vector3.Angle(LowerBodyAim, MovementDirection);
        AngleDiff = Mathf.Abs(AngleDiff);
        float AngleDot = Vector3.Dot(LowerBody.right, MovementDirection);
        print("Dot: " + AngleDot + " Diff: " + AngleDiff);
        if (AngleDot == 0f)//Idle Animation
        {
            Debug.Log("Idle Animation");
            if (DebuggerAnimSphere)
            {
                DebuggerAnimSphere.position = LowerBody.position;
            }
        }
        else if (AngleDiff < 45f)//Run Forward Animation
        {
            Debug.Log("Forward Animation");
            if (DebuggerAnimSphere)
            {
                DebuggerAnimSphere.position = LowerBody.position + LowerBody.forward * 2f;
            }
        }
        else if (AngleDiff > 45f && AngleDiff < 135f)
        {
            if (AngleDot > 0.5f)//Run Right Animation
            {
                Debug.Log("Right Animation");
                if (DebuggerAnimSphere)
                {
                    DebuggerAnimSphere.position = LowerBody.position + LowerBody.right * 2f;
                }
            }
            else if (AngleDot < -0.5f)//Run Left Animation
            {
                Debug.Log("Left Animation");
                if (DebuggerAnimSphere)
                {
                    DebuggerAnimSphere.position = LowerBody.position + -LowerBody.right * 2f;
                }
            }

        }
        else if (AngleDiff > 135f)//Run Backwards Animation
        {
            Debug.Log("Backward Animation");
            if (DebuggerAnimSphere)
            {
                DebuggerAnimSphere.position = LowerBody.position + -LowerBody.forward * 2f;
            }
        }

    }

    void MovementUpdate()
    {
        Movement = Vector3.zero;
        MovementDirection = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Movement += MovementAlignment.right * Speed * SpeedModifier;
            MovementDirection += MovementAlignment.right;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Movement -= MovementAlignment.right * Speed * SpeedModifier;
            MovementDirection -= MovementAlignment.right;
        }
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Movement += MovementAlignment.forward * Speed * SpeedModifier;
            MovementDirection += MovementAlignment.forward;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Movement -= MovementAlignment.forward * Speed * SpeedModifier;
            MovementDirection -= MovementAlignment.forward;
        }
        Rigid.MovePosition(transform.position + Movement);
    }
}
