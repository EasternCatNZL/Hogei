using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

    public bool IsOpen;

    public GameObject LeftGate;
    public GameObject RightGate;

    [Header("Sound Settings")]
    //public AudioSource OpenSound;
    public AudioClip openClip;

    private bool LockGateTimer;
    private float LastTime;
    private float LockTime = 1f;

	// Use this for initialization
	void Start () {
        if(IsOpen)
        {
            UnlockGate(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(LockGateTimer && Time.time - LastTime > LockTime)
        {
            RightGate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            LeftGate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            LockGateTimer = false;
        }
	}

    public void LockGate()
    {
        //Set right gate values
        JointSpring NewSpring = new JointSpring();
        NewSpring.spring = 1000;
        NewSpring.damper = 0;
        NewSpring.targetPosition = 0;
        RightGate.GetComponent<HingeJoint>().spring = NewSpring;
        //Set left gate values
        LeftGate.GetComponent<HingeJoint>().spring = NewSpring;
        //Setup the timer to lock the gate
        LockGateTimer = true;
        LastTime = Time.time;
    }

    public void UnlockGate()
    {
        //Set right gate values
        JointSpring NewSpring = new JointSpring();
        NewSpring.spring = 2;
        NewSpring.damper = 1;
        NewSpring.targetPosition = -90;
        RightGate.GetComponent<HingeJoint>().spring = NewSpring;
        //Set left gate values
        NewSpring.targetPosition = 90;
        LeftGate.GetComponent<HingeJoint>().spring = NewSpring;
        //Unfreeze rigidbody positions
        RightGate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        LeftGate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //Play Sound
        MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
        SoundSettings.Pitch = 1f;
        SoundSettings.SpatialBlend = 0f;
        MusicManager.GetInstance().PlaySoundAtLocation(openClip, transform.position, SoundSettings);
    }

    public void UnlockGate(bool _PlaySound)
    {
        //Set right gate values
        JointSpring NewSpring = new JointSpring();
        NewSpring.spring = 2;
        NewSpring.damper = 1;
        NewSpring.targetPosition = -90;
        RightGate.GetComponent<HingeJoint>().spring = NewSpring;
        //Set left gate values
        NewSpring.targetPosition = 90;
        LeftGate.GetComponent<HingeJoint>().spring = NewSpring;
        //Unfreeze rigidbody positions
        RightGate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        LeftGate.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //Play Sound
        if (_PlaySound)
        {
            MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
            SoundSettings.Pitch = 1f;
            SoundSettings.SpatialBlend = 0f;
            MusicManager.GetInstance().PlaySoundAtLocation(openClip, transform.position, SoundSettings);
        }
    }
}
