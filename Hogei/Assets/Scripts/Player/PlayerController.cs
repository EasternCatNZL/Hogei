using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    [Header("Animation")]
    public Transform UpperBody;
    public Transform LowerBody;
    [Tooltip("How high the angle between the lower and upper body can be.")]
    public float LowerBodyAngleAllowance = 45f;
    Vector3 UpperBodyAim;
    Vector3 LowerBodyAim;
    public Transform DebuggerAnimSphere;

    [Header("Movement")]
    public Transform MovementAlignment;
    Vector3 MovementDirection;
    Vector3 Movement;

    private Rigidbody Rigid;
    private Animator myAnim;

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
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MovementUpdate();
        if(LowerBody)LowerBodyUpdate();
        if (UpperBody) UpperBodyUpdate();
        //Debugging
        if (UpperBody) Debug.DrawRay(UpperBody.position, UpperBody.up * 3, Color.red);
        if (LowerBody)
        {
            Debug.DrawRay(LowerBody.position, LowerBodyAim * 3, Color.blue);
            Debug.DrawRay(LowerBody.position, MovementDirection * 3, Color.green);
        }
    }

    void UpperBodyUpdate()
    {
        //Aim the upper body
        Vector3 AimDirection = MouseTarget.GetWorldMousePos() - UpperBody.position;
        if (Vector3.Dot(-UpperBody.forward, AimDirection) < 0.0f)
        {
            UpperBody.Rotate(Vector3.Angle(UpperBody.up, AimDirection), 0.0f, 0.0f);
        }
        if (Vector3.Dot(-UpperBody.forward, AimDirection) > 0.0f)
        {
            UpperBody.Rotate(-Vector3.Angle(UpperBody.up, AimDirection), 0.0f, 0.0f);
        }
        UpperBodyAim = UpperBody.up;
    }

    void LowerBodyUpdate()
    {
        //Aim the lower body
        float AngleDiff = Vector3.Angle(UpperBodyAim, LowerBodyAim);
        if (Mathf.Abs(AngleDiff) > LowerBodyAngleAllowance)
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
        if (AngleDot == 0f)//Idle Animation
        {
            myAnim.SetBool("IsMoving", false);
            if (DebuggerAnimSphere)
            {
                DebuggerAnimSphere.position = LowerBody.position;
            }
        }
        else
        {
            myAnim.SetBool("IsMoving", true);
        }
        if (AngleDiff < 45f)//Run Forward Animation
        {
            myAnim.SetTrigger("Forward");
            if (DebuggerAnimSphere)
            {
                DebuggerAnimSphere.position = LowerBody.position + LowerBody.forward * 2f;
            }
        }
        else if (AngleDiff > 45f && AngleDiff < 135f)
        {
            if (AngleDot > 0.5f)//Run Right Animation
            {
                myAnim.SetTrigger("Right");
                if (DebuggerAnimSphere)
                {
                    DebuggerAnimSphere.position = LowerBody.position + LowerBody.right * 2f;
                }
            }
            else if (AngleDot < -0.5f)//Run Left Animation
            {
                myAnim.SetTrigger("Left");
                if (DebuggerAnimSphere)
                {
                    DebuggerAnimSphere.position = LowerBody.position + -LowerBody.right * 2f;
                }
            }

        }
        else if (AngleDiff > 135f)//Run Backwards Animation
        {
            myAnim.SetTrigger("Backward");
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
        if (Luminosity.IO.InputManager.GetAxisRaw("Horizontal") > 0f)
        {
            Movement += MovementAlignment.right * Speed * SpeedModifier;
            MovementDirection += MovementAlignment.right;
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw("Horizontal") < 0f)
        {
            Movement -= MovementAlignment.right * Speed * SpeedModifier;
            MovementDirection -= MovementAlignment.right;
        }
        if (Luminosity.IO.InputManager.GetAxisRaw("Vertical") > 0f)
        {
            Movement += MovementAlignment.forward * Speed * SpeedModifier;
            MovementDirection += MovementAlignment.forward;
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw("Vertical") < 0f)
        {
            Movement -= MovementAlignment.forward * Speed * SpeedModifier;
            MovementDirection -= MovementAlignment.forward;
        }
        Rigid.MovePosition(transform.position + Movement * Time.deltaTime);
    }
}
