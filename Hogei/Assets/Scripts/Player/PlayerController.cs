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

    [Header("Movement")]
    public Transform MovementAlignment;
    Vector3 MovementDirection;
    Vector3 Movement;

    [Header("Aim")]
    public Transform AimIndicator;

    private Rigidbody Rigid;
    private Animator myAnim;

    [Header("Speed")]
    public float Speed;
    public float SpeedModifier;

    [Header("Input axis")]
    public string leftStickX = "CHorizontal";
    public string leftStickY = "CVertical";
    public string rightStickX = "CHorizontalAim";
    public string rightStickY = "CVerticalAim";

    [Header("Dead zone var")]
    public float deadZone = 0.5f;

    private bool DebuggingMode = false;

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

    /// <summary>
    /// Update function called once a frame.
    /// Used so that calculated rotations can override the animations.
    /// </summary>
    void LateUpdate()
    {
        //movement
        if (GetComponent<WhatCanIDO>().useKeyboard)
        {
            MovementUpdate();
        }
        else if (GetComponent<WhatCanIDO>().useController)
        {
            ControllerMovementUpdate();
        }
        //lowerbody
        if(LowerBody)LowerBodyUpdate();
        //upperbody
        if (UpperBody)
        {
            if (GetComponent<WhatCanIDO>().useKeyboard)
            {
                UpperBodyUpdate();
            }
            else if (GetComponent<WhatCanIDO>().useController)
            {
                ControllerUpperBodyUpdate();
            }
        }
        //aim indicator
        if (AimIndicator)
        {
            if (GetComponent<WhatCanIDO>().useKeyboard)
            {
                AimIndicatorUpdate();
            }
            else if (GetComponent<WhatCanIDO>().useController)
            {
                ControllerAimIndicatorUpdate();
            }

        }
        //Debugging
        if (DebuggingMode)
        {
            if (UpperBody) Debug.DrawRay(UpperBody.position, UpperBody.up * 3, Color.red);
            if (LowerBody)
            {
                Debug.DrawRay(LowerBody.position, LowerBodyAim * 3, Color.blue);
                Debug.DrawRay(LowerBody.position, MovementDirection * 3, Color.green);
            }
        }
    }

    /// <summary>
    /// Handles the rotation of the upper body
    /// </summary>
    void UpperBodyUpdate()
    {
        //Aim the upper body
        Vector3 _AimDirection = MouseTarget.GetWorldMousePos() - UpperBody.position;
        if (Vector3.Dot(-UpperBody.forward, _AimDirection) < 0.0f)
        {
            UpperBody.Rotate(Vector3.Angle(UpperBody.up, _AimDirection), 0.0f, 0.0f);
        }
        if (Vector3.Dot(-UpperBody.forward, _AimDirection) > 0.0f)
        {
            UpperBody.Rotate(-Vector3.Angle(UpperBody.up, _AimDirection), 0.0f, 0.0f);
        }

        UpperBodyAim = UpperBody.up;
    }

    //controller input for upper body
    private void ControllerUpperBodyUpdate()
    {
        //get direction from stick
        Vector3 direction = new Vector3(Luminosity.IO.InputManager.GetAxis(rightStickX), 0.0f, Luminosity.IO.InputManager.GetAxisRaw(rightStickY)/* + MovementAlignment.rotation.eulerAngles.y*/);
        //only work if meaningful
        if(direction.sqrMagnitude > deadZone)
        {
            //apply rotation
            float angle = Mathf.Atan2(Luminosity.IO.InputManager.GetAxisRaw(rightStickX), Luminosity.IO.InputManager.GetAxisRaw(rightStickY)) * Mathf.Rad2Deg;

            if (Vector3.Dot(-UpperBody.forward, direction) < 0.0f)
            {
                UpperBody.Rotate(Vector3.Angle(UpperBody.up, direction) - MovementAlignment.rotation.eulerAngles.y, 0.0f, 0.0f);
            }
            if (Vector3.Dot(-UpperBody.forward, direction) > 0.0f)
            {
                UpperBody.Rotate(-Vector3.Angle(UpperBody.up, direction) - MovementAlignment.rotation.eulerAngles.y, 0.0f, 0.0f);
            }

            //UpperBody.rotation = Quaternion.Euler(0.0f, MovementAlignment.rotation.eulerAngles.y + angle, 0.0f);

            UpperBodyAim = UpperBody.up;
        }
    }

    /// <summary>
    /// Handles the rotation of the aiming indicator
    /// </summary>
    void AimIndicatorUpdate()
    {
        Vector3 _AimDirection = MouseTarget.GetWorldMousePos() - UpperBody.position;
        if (Vector3.Dot(AimIndicator.right, _AimDirection) < 0.0f)
        {
            AimIndicator.Rotate(0, 0, Vector3.Angle(AimIndicator.up, _AimDirection));
        }
        if (Vector3.Dot(AimIndicator.right, _AimDirection) > 0.0f)
        {
            AimIndicator.Rotate(0, 0, -Vector3.Angle(AimIndicator.up, _AimDirection));
        }
    }

    //aim indicator update for controller
    void ControllerAimIndicatorUpdate()
    {
        //get direction
        Vector3 direction = new Vector3(Luminosity.IO.InputManager.GetAxisRaw(rightStickX), 0.0f,  Luminosity.IO.InputManager.GetAxisRaw(rightStickY)/* + MovementAlignment.rotation.eulerAngles.y*/);
        //only do if meaningful
        //print(direction.sqrMagnitude);
        if(direction.sqrMagnitude > deadZone)
        {
            //apply rotation
            float angle = Mathf.Atan2(Luminosity.IO.InputManager.GetAxisRaw(rightStickX), Luminosity.IO.InputManager.GetAxisRaw(rightStickY)) * Mathf.Rad2Deg;

            if (Vector3.Dot(AimIndicator.right, direction) < 0.0f)
            {
                AimIndicator.Rotate(0, 0, Vector3.Angle(AimIndicator.up, direction) - MovementAlignment.rotation.eulerAngles.y);
            }
            if (Vector3.Dot(AimIndicator.right, direction) > 0.0f)
            {
                AimIndicator.Rotate(0, 0, -Vector3.Angle(AimIndicator.up, direction) - MovementAlignment.rotation.eulerAngles.y);
            }

            //AimIndicator.Rotate(0, 0, Vector3.Angle(AimIndicator.up, direction));

        }

    }

    /// <summary>
    /// Handles the rotation and animations of the lower body
    /// </summary>
    void LowerBodyUpdate()
    {
        //Aim the lower body
        float AngleDiff = Vector3.Angle(UpperBodyAim, LowerBodyAim);
        if (Mathf.Abs(AngleDiff) > LowerBodyAngleAllowance)
        {
            if (Vector3.Dot(LowerBody.right, UpperBodyAim) < 0.0f)
            {
                myAnim.SetTrigger("TurnLeft");
                LowerBody.Rotate(0.0f, -Vector3.Angle(LowerBody.forward, UpperBodyAim), 0.0f);
            }
            else if (Vector3.Dot(LowerBody.right, UpperBodyAim) > 0.0f)
            {
                myAnim.SetTrigger("TurnRight");
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
            myAnim.SetBool("Right", false);
            myAnim.SetBool("Left", false);
            myAnim.SetBool("Backward", false);
            myAnim.SetBool("Forward", false);
            myAnim.SetBool("IsMoving", false);
        }
        else
        {
            myAnim.SetBool("IsMoving", true);
        }
        if (AngleDiff < 45f)//Run Forward Animation
        {
            myAnim.SetBool("Right", false);
            myAnim.SetBool("Left", false);
            myAnim.SetBool("Backward", false);
            myAnim.SetBool("Forward", true);
        }
        else if (AngleDiff > 45f && AngleDiff < 135f)
        {
            if (AngleDot > 0.5f)//Run Right Animation
            {
                myAnim.SetBool("Backward", false);
                myAnim.SetBool("Forward", false);
                myAnim.SetBool("Left", false);
                myAnim.SetBool("Right", true);
            }
            else if (AngleDot < -0.5f)//Run Left Animation
            {
                myAnim.SetBool("Backward", false);
                myAnim.SetBool("Forward", false);
                myAnim.SetBool("Right", false);
                myAnim.SetBool("Left", true);               
            }

        }
        else if (AngleDiff > 135f)//Run Backwards Animation
        {
            myAnim.SetBool("Right", false);
            myAnim.SetBool("Left", false);
            myAnim.SetBool("Forward", false);
            myAnim.SetBool("Backward", true);
        }

    }

    /// <summary>
    /// Handles the movement of the player character in the world
    /// </summary>
    void MovementUpdate()
    {
        Movement = Vector3.zero;
        MovementDirection = Vector3.zero;
        if (Luminosity.IO.InputManager.GetAxisRaw("Horizontal") > 0f)
        {
            Movement += MovementAlignment.right;
            MovementDirection += MovementAlignment.right;
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw("Horizontal") < 0f)
        {
            Movement -= MovementAlignment.right;
            MovementDirection -= MovementAlignment.right;
        }
        if (Luminosity.IO.InputManager.GetAxisRaw("Vertical") > 0f)
        {
            Movement += MovementAlignment.forward;
            MovementDirection += MovementAlignment.forward;
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw("Vertical") < 0f)
        {
            Movement -= MovementAlignment.forward;
            MovementDirection -= MovementAlignment.forward;
        }
        Movement.Normalize();
        Movement *= Speed * SpeedModifier;
        Rigid.MovePosition(transform.position + Movement * Time.deltaTime);
    }

    //controller movement logic
    void ControllerMovementUpdate()
    {
        Movement = Vector3.zero;

        //get movement input from sticks
        if (Luminosity.IO.InputManager.GetAxisRaw(leftStickX) > deadZone)
        {
            Movement += MovementAlignment.right * Speed * SpeedModifier * Luminosity.IO.InputManager.GetAxisRaw(leftStickX);
            MovementDirection += MovementAlignment.right;
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw(leftStickX) < -deadZone)
        {
            Movement += MovementAlignment.right * Speed * SpeedModifier * Luminosity.IO.InputManager.GetAxisRaw(leftStickX);
            MovementDirection -= MovementAlignment.right;
        }
        if (Luminosity.IO.InputManager.GetAxisRaw(leftStickY) > deadZone)
        {
            Movement += MovementAlignment.forward * Speed * SpeedModifier * Luminosity.IO.InputManager.GetAxisRaw(leftStickY);
            MovementDirection += MovementAlignment.forward;
        }
        else if (Luminosity.IO.InputManager.GetAxisRaw(leftStickY) < -deadZone)
        {
            Movement += MovementAlignment.forward * Speed * SpeedModifier * Luminosity.IO.InputManager.GetAxisRaw(leftStickY);
            MovementDirection -= MovementAlignment.forward;
        }

        Rigid.MovePosition(transform.position + Movement * Time.deltaTime);
    }

    public void SetSpeedModifier(float _NewMod) { SpeedModifier = _NewMod; }
}
