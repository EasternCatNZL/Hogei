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

    [Header("Sprite Settings")]
    public Sprite SelectorSprite;
    public Sprite SelectorSpriteSelected;
    public Sprite SelectorNoneSprite;
    public Sprite SelectorNoneSpriteSelected;
    [Header("Weapon Sprites")]
    public SpriteRenderer WeaponIcon;

    public Sprite StreamSprite;
    public Sprite ShotSprite;
    public Sprite HomeSprite;
    public Sprite ExplosiveSprite;

    private SpriteRenderer Renderer;
    private Weapon.WeaponTypes WeaponSelected;
    private WeaponInventory weaponInventory;

    private void Start()
    {
        GameObject Finder = GameObject.Find("WeaponInventory");
        Renderer = GetComponent<SpriteRenderer>();
        if (Finder != null)
        {
            weaponInventory = Finder.GetComponent<WeaponInventory>();
        }
        if (PrimarySelector) WeaponSelected = PlayerManager.GetInstance().GetPrimary();
        if (SecondarySelector) WeaponSelected = PlayerManager.GetInstance().GetSecondary();
    }

    private void OnMouseExit()
    {
        OnDeselected();
    }

    //when no longer hovered over
    public void OnDeselected()
    {
        if (WeaponSelected == Weapon.WeaponTypes.None || WeaponSelected == null)
        {
            Renderer.sprite = SelectorNoneSprite;
        }
        else
        {
            Renderer.sprite = SelectorSprite;
            if (WeaponIcon.sprite == null)
            {
                UpdateWeaponIcon();
            }
        }
        SoupManager.UpdateUI();
    }

    private void OnMouseEnter()
    {
        OnSelected();
    }

    //when hovered over
    public void OnSelected()
    {
        if (WeaponSelected == Weapon.WeaponTypes.None || WeaponSelected == null)
        {
            Renderer.sprite = SelectorNoneSpriteSelected;
        }
        else
        {
            Renderer.sprite = SelectorSpriteSelected;
        }
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

    private void UpdateWeaponIcon()
    {
        if(WeaponSelected == Weapon.WeaponTypes.None || WeaponSelected == null)
        {
            Renderer.sprite = SelectorNoneSprite;
        }
        else
        {
            Renderer.sprite = SelectorSprite;
        }
        switch(WeaponSelected)
        {
            case Weapon.WeaponTypes.Stream:
                WeaponIcon.sprite = StreamSprite;
                break;
            case Weapon.WeaponTypes.Explosive:
                WeaponIcon.sprite = ExplosiveSprite;
                break;
            case Weapon.WeaponTypes.Fert:
                Debug.Log(gameObject.name + ": Sorry but the fertiliser gun is no longer in production.");
                break;
            case Weapon.WeaponTypes.Home:
                WeaponIcon.sprite = HomeSprite;
                break;
            case Weapon.WeaponTypes.Bloom://The Shotgun
                WeaponIcon.sprite = ShotSprite;
                break;
            default:
                WeaponIcon.sprite = null;
                break;
        }
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
        UpdateWeaponIcon();

    }
    public Weapon.WeaponTypes GetWeapon() { return WeaponSelected; }
}
