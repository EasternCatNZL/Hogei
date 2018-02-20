using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Input")]
    [Tooltip("Keyboard input key for weapon fire")]
    [Range(0, 1)]
    public int mouseInputKey = 0;
    [Tooltip("Switch weapon <-")]
    public KeyCode prevWeaponInput = KeyCode.Q;
    [Tooltip("Switch weapon ->")]
    public KeyCode nextWeaponInput = KeyCode.E;

    public float HealthDecrease = 1.0f;

    //control vars
    private int currentWeaponIndex = 0;
    public int numWeapons = 0;
    public bool peaShootStrengthened = false;

    //script refs
    private WhatCanIDO canDo;
    private PeaShooter peaShooter;
    private PlayerStreamShot streamShot;
    private PlayerHomingShot homingShot;

    //Component
    private Animator Anim;

    // Use this for initialization
    void Start () {
        if (GetComponent<WhatCanIDO>())
        {
            canDo = GetComponent<WhatCanIDO>();
        }
        else
        {
            Debug.LogError("canDo can not be assigned. WhatCanIDO script not present on " + name);
        }
        Anim = GetComponent<Animator>();
        SetupWeapons();
    }
	
	// Update is called once per frame
	void Update () {
        if (canDo)
        {
            if (canDo.canShoot)
            {
                UseWeapon();
                SwitchWeapon();
            }
        }
	}

    //setup weapon script releationships 
    private void SetupWeapons()
    {
        peaShooter = GetComponentInChildren<PeaShooter>();
        if (peaShooter)
        {
            numWeapons++;
        }
        streamShot = GetComponentInChildren<PlayerStreamShot>();
        if (streamShot)
        {
            numWeapons++;
        }
        homingShot = GetComponentInChildren<PlayerHomingShot>();
        if (homingShot)
        {
            numWeapons++;
        }
    }

    //attack use logic
    private void UseWeapon()
    {
        //check if input 
        if (CheckMouseInputWeapon())
        {
            Anim.SetBool("IsShooting", true);
            GetComponent<EntityHealth>().DecreaseHealth(HealthDecrease);
            //try to use current weapon
            switch (currentWeaponIndex)
            {
                case 0:
                    StartCoroutine(peaShooter.UseWeapon(peaShootStrengthened));
                    //streamShot.UseWeapon();
                    //homingShot.UseWeapon();
                    break;
                case 1:
                    streamShot.UseWeapon();
                    break;
                case 2:
                    homingShot.UseWeapon();
                    break;
            }
        }
        else
        {
            Anim.SetBool("IsShooting", false);
        }
    }

    //switch weapon
    private void SwitchWeapon()
    {
        //switch to prev weapon
        if (Input.GetKeyDown(prevWeaponInput)){
            //decrement the current index
            currentWeaponIndex--;
            //if index becomes -1
            if(currentWeaponIndex <= 0)
            {
                //set weapon index to last weapon
                currentWeaponIndex = numWeapons - 1;
            }
        }
        //switch to next weapon
        else if (Input.GetKeyDown(nextWeaponInput))
        {
            //increment the current index
            currentWeaponIndex++;
            //if weapon becomes larger than number of weapons
            if(currentWeaponIndex >= numWeapons)
            {
                //set weapon to first weapon
                currentWeaponIndex = 0;
            }
        }
    }

    public int GetWeaponIndex()
    {
        return currentWeaponIndex;
    }

    //keyboard input check for firing weapon <- to avoid clunkiness in code
    private bool CheckMouseInputWeapon()
    {
        bool valid = false;
        if (Input.GetMouseButton(mouseInputKey))
        {
            valid = true;
        }
        return valid;
    }
}
