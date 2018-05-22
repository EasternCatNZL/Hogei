using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("Input")]
    //[Tooltip("Keyboard input key for weapon fire")]
    //[Range(0, 1)]
    //public int mouseInputKey = 0;
    [Tooltip("Switch weapon <-")]
    public KeyCode prevWeaponInput = KeyCode.Q;
    [Tooltip("Switch weapon ->")]
    public KeyCode nextWeaponInput = KeyCode.E;
    [Tooltip("Input axis")]
    public string attackInputAxis = "Attack";
    public string attackInputAxisController = "ControllerAttack";

    //public float HealthDecrease = 1.0f;

    public bool isShooting = false;

    

    //control vars
    private int currentWeaponIndex = 0;
    private int numWeapons = 0;
    public bool peaShootStrengthened = false;

    //script refs
    private WhatCanIDO canDo;
    private PeaShooter peaShooter;
    private PlayerStreamShot streamShot;
    private PlayerHomingShot homingShot;

    private Weapon PrimaryWeapon;
    private Weapon SecondaryWeapon;

    private WeaponWheel WW;

    //Component
    private Animator Anim;

    //Singleton
    private static GameObject PlayerInstance;

    // Use this for initialization
    void Start () {
        if(PlayerInstance == null)
        {
            PlayerInstance = gameObject;
        }
        else
        {
            Debug.Log("Player character already exists destroying self " + gameObject.name);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if (GetComponent<WhatCanIDO>())
        {
            canDo = GetComponent<WhatCanIDO>();
        }
        else
        {
            Debug.LogError("canDo can not be assigned. WhatCanIDO script not present on " + name);
        }
        Anim = GetComponent<Animator>();
        //check if weapon wheel exists
        if (GameObject.FindGameObjectWithTag("WeaponWheel"))
        {
            WW = GameObject.FindGameObjectWithTag("WeaponWheel").GetComponent<WeaponWheel>();
        }
        
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
    public void SetupWeapons()
    {
        numWeapons = 0;

        PrimaryWeapon = PlayerManager.GetInstance().GetPrimary();
        SecondaryWeapon = PlayerManager.GetInstance().GetSecondary();
        if (PrimaryWeapon != null) numWeapons++;
        if (SecondaryWeapon != null) numWeapons++;
    }

    //attack use logic
    private void UseWeapon()
    {
        //check if input 
        if (CheckInput())
        {
            Anim.SetBool("IsShooting", true);
            isShooting = true;
            //try to use current weapon
            switch (currentWeaponIndex)
            {
                case 0:
                    if(PrimaryWeapon) PrimaryWeapon.UseWeapon();
                    break;
                case 1:
                    if(SecondaryWeapon) SecondaryWeapon.UseWeapon();
                    break;
                default:
                    Debug.Log("Something broke with the weapon switching...");
                    break;
            }
        }
        else
        {
            Anim.SetBool("IsShooting", false);
            isShooting = false;
        }
    }

    //switch weapon
    private void SwitchWeapon()
    {
        //switch to prev weapon
        if (Input.GetKeyDown(prevWeaponInput)){
            //decrement the current index
            currentWeaponIndex--;
            WW.NextWeapon();
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
            WW.NextWeapon();
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
    private bool CheckInput()
    {
        bool valid = false;
        //check what player is using
        if (canDo.useKeyboard)
        {
            if (/*Luminosity.IO.InputManager.GetAxisRaw(attackInputAxis) != 0*/ Luminosity.IO.InputManager.GetButton(attackInputAxis) )
            {
                valid = true;
            }
        }
        else if (canDo.useController)
        {
            if(Luminosity.IO.InputManager.GetAxisRaw(attackInputAxisController) != 0)
            {
                valid = true;
            }
        }

        return valid;
    }

    public void ClearWeapons()
    {
        PrimaryWeapon = null;
        SecondaryWeapon = null;
        numWeapons = 0;
    }
}
