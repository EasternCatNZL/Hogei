using System.Collections;
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
        if (PrimarySelector) WeaponSelected = PlayerManager.GetInstance().GetPrimary();
        if (SecondarySelector) WeaponSelected = PlayerManager.GetInstance().GetSecondary();
    }

    private void OnMouseExit()
    {
        SoupManager.UpdateUI();
    }

    private void OnMouseEnter()
    {
        if (PrimarySelector)
        {
            DescriptionText.text = GetDescriptionText();
        }
        else if (SecondarySelector)
        {
            DescriptionText.text = GetDescriptionText();
        }
    }

    private void OnMouseDown()
    {
        weaponInventory.OpenInventory(this);
    }

    private string GetDescriptionText()
    {
        string _text = "";
        //Check if the weapon is primary or secondary
        if (PrimarySelector) _text = "Primary Weapon:\n";
        else _text = "Secondary Weapon:\n";
        //Add the description for the correct weapon
        switch (WeaponSelected)
        {
            case Weapon.WeaponTypes.Stream:
                _text += "Stream Shot";
                break;
            case Weapon.WeaponTypes.Explosive:
                _text += "Explosive Shot";
                break;
            case Weapon.WeaponTypes.Fert:
                _text += "Corn Shot";
                break;
            case Weapon.WeaponTypes.Home:
                _text += "Homing Shot";
                break;
            case Weapon.WeaponTypes.Bloom://The Shotgun
                _text += "Shotgun Shot";
                break;
            default:
                Debug.Log(gameObject + ": Weapon type doesn't exist(Weapon Selector Getting Text)");
                break;
        }
        return _text;
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
        else if (SecondarySelector)
        {
            PlayerManager.GetInstance().SetSecondary(WeaponSelected);
        }

    }
    public Weapon.WeaponTypes GetWeapon() { return WeaponSelected; }
}
