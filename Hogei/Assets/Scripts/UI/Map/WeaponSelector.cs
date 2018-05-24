﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour
{

    public bool PrimarySelector = false;
    public bool SecondarySelector = false;
    public Text DescriptionText;
    public SoupManager SoupManager;
    private Weapon.WeaponTypes WeaponSelected;
    private WeaponInventory weaponInventory;

    private void Start()
    {
        GameObject Finder = GameObject.Find("WeaponInventory");
        if (Finder != null)
        {
            weaponInventory = Finder.GetComponent<WeaponInventory>();
        }
    }

    private void OnMouseExit()
    {
        SoupManager.UpdateUI();
    }

    private void OnMouseEnter()
    {
        if (PrimarySelector)
        {
            DescriptionText.text = "Selects a primary weapon " + WeaponSelected.ToString();
        }
        else if(SecondarySelector)
        {
            DescriptionText.text = "Selects a secondary weapon " + WeaponSelected.ToString();
        }
    }

    private void OnMouseDown()
    {
        weaponInventory.OpenInventory(this);
    }

    public void SetWeapon(Weapon.WeaponTypes _NewWeapon)
    {
        WeaponSelected = _NewWeapon;
        if (PrimarySelector && SecondarySelector)
        {
            Debug.LogError(gameObject.name + ": YOU CANNOT HAVE A SELECTOR PICK THE PRIMARY AND SECONDARY WEAPON");
        }
        else if (PrimarySelector)
        {
            PlayerManager.GetInstance().SetPrimary(WeaponSelected);
        }
        else if(SecondarySelector)
        {
            PlayerManager.GetInstance().SetSecondary(WeaponSelected);
        }

    }
    public Weapon.WeaponTypes GetWeapon() { return WeaponSelected; }
}