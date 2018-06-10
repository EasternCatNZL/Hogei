using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("Input")]
    //[Tooltip("Keyboard input key for weapon fire")]
    //[Range(0, 1)]
    //public int mouseInputKey = 0;
    //[Tooltip("Switch weapon <-")]
    //public KeyCode prevWeaponInput = KeyCode.Q;
    //[Tooltip("Switch weapon ->")]
    //public KeyCode nextWeaponInput = KeyCode.E;
    public string kNextWeaponInput = "Switch";
    public string cNextWeaponInput = "CSwitch";
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

    public Weapon PrimaryWeapon;
    public Weapon SecondaryWeapon;

    private WeaponWheel WW;

    //Component
    private Animator Anim;

    //Singleton
    private static GameObject PlayerInstance;

    // Use this for initialization
    void Start()
    {
        if (PlayerInstance == null)
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

        //SetupWeapons();

    }

    // Update is called once per frame
    void Update()
    {
        if (canDo)
        {
            if (canDo.canShoot)
            {
                UseWeapon();
                SwitchWeapon();
            }
        }
        //print(currentWeaponIndex);
        //print(numWeapons);
    }

    //setup weapon script releationships 
    public void SetupWeapons(Weapon.WeaponTypes _PriType, Weapon.WeaponTypes _SecType, SoupUpgrade _PriUpg, SoupUpgrade _SecUpg)
    {
        numWeapons = 0;
        //Set the primary weapon
        switch (_PriType)
        {
            case Weapon.WeaponTypes.Stream:
                PrimaryWeapon = GetComponentInChildren<PlayerStreamShot>();
                break;
            case Weapon.WeaponTypes.Fert:
                PrimaryWeapon = GetComponentInChildren<FertilizerShot>();
                break;
            case Weapon.WeaponTypes.Home:
                PrimaryWeapon = GetComponentInChildren<PlayerHomingShot>();
                break;
            case Weapon.WeaponTypes.Bloom:
                PrimaryWeapon = GetComponentInChildren<PlayerShotgunShot>();
                break;
            case Weapon.WeaponTypes.Explosive:
                PrimaryWeapon = GetComponentInChildren<PlayerExplosiveShot>();
                break;
            default:
                PrimaryWeapon = null;
                Debug.LogWarning(gameObject.name + ": GIVEN WEAPON TYPES WERE NOT VALID FOR PRIMARY: " + _PriType.ToString());
                break;
        }
        switch (_SecType)
        {
            case Weapon.WeaponTypes.Stream:
                SecondaryWeapon = GetComponentInChildren<PlayerStreamShot>();
                break;
            case Weapon.WeaponTypes.Fert:
                SecondaryWeapon = GetComponentInChildren<FertilizerShot>();
                break;
            case Weapon.WeaponTypes.Home:
                SecondaryWeapon = GetComponentInChildren<PlayerHomingShot>();
                break;
            case Weapon.WeaponTypes.Explosive:
                SecondaryWeapon = GetComponentInChildren<PlayerExplosiveShot>();
                break;
            case Weapon.WeaponTypes.Bloom:
                SecondaryWeapon = GetComponentInChildren<PlayerShotgunShot>();
                break;
            default:
                SecondaryWeapon = null;
                Debug.LogWarning(gameObject.name + ": GIVEN WEAPON TYPES WERE NOT VALID FOR SECONDARY: " + _SecType.ToString());
                break;
        }
        //Apply Upgrades
        if (PrimaryWeapon && _PriUpg) PrimaryWeapon.ApplyUpgrade(_PriUpg);
        if (SecondaryWeapon && _SecUpg) SecondaryWeapon.ApplyUpgrade(_SecUpg);
        //Increment Weapon Count
        if (PrimaryWeapon != null)
        {
            numWeapons++;
        }
        if (SecondaryWeapon != null)
        {
            numWeapons++;
        }
        //Swap the primary for the secondary if there is only a secondary weapon
        if (SecondaryWeapon != null && PrimaryWeapon == null)
        {
            PrimaryWeapon = SecondaryWeapon;
            SecondaryWeapon = null;
        }
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
                    if (PrimaryWeapon) PrimaryWeapon.UseWeapon();
                    break;
                case 1:
                    if (SecondaryWeapon) SecondaryWeapon.UseWeapon();
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
        if (canDo.useKeyboard)
        {
            if (Luminosity.IO.InputManager.GetButtonDown(kNextWeaponInput))
            {
                if (WW) WW.NextWeapon(); //Change the weapon wheel UI
                else
                {
                    GameObject temp = GameObject.FindGameObjectWithTag("WeaponWheel");
                    if (temp) WW = temp.GetComponent<WeaponWheel>();
                }
                if (currentWeaponIndex == 0)
                {
                    currentWeaponIndex = 1;
                }
                else if (currentWeaponIndex == 1)
                {
                    currentWeaponIndex = 0;
                }
            }
        }
        else if (canDo.useController)
        {
            if (Luminosity.IO.InputManager.GetButtonDown(cNextWeaponInput))
            {
                if (WW) WW.NextWeapon(); //Change the weapon wheel UI
                else
                {
                    GameObject temp = GameObject.FindGameObjectWithTag("WeaponWheel");
                    if (temp) WW = temp.GetComponent<WeaponWheel>();
                }
                if (currentWeaponIndex == 0)
                {
                    currentWeaponIndex = 1;
                }
                else if (currentWeaponIndex == 1)
                {
                    currentWeaponIndex = 0;
                }
            }
        }


        /*
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
        */
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
            if (/*Luminosity.IO.InputManager.GetAxisRaw(attackInputAxis) != 0*/ Luminosity.IO.InputManager.GetButton(attackInputAxis))
            {
                valid = true;
            }
        }
        else if (canDo.useController)
        {
            if (Luminosity.IO.InputManager.GetAxisRaw(attackInputAxisController) != 0)
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
