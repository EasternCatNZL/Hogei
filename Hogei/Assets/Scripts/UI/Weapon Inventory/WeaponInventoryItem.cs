using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponInventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public bool Locked = true;
    public WeaponInventory weaponInventory;
    public Weapon.WeaponTypes weaponType;
    [Header("Sprite Settings")]
    public Sprite WeaponIcon;
    public Sprite WeaponIconSelected;

    private Image Renderer;
    private Text DetailsText;

	// Use this for initialization
	void Start () {
        weaponInventory = GetComponentInParent<WeaponInventory>();
        Renderer = GetComponent<Image>();
        DetailsText = weaponInventory.GetDetailsText();
        Locked = weaponInventory.CheckUnlocked(weaponType);
	}

    public void OnPointerEnter(PointerEventData _Event)
    {
        OnSelect();
    }

    //when seleceted
    public void OnSelect()
    {
        Renderer.sprite = WeaponIconSelected;
        DetailsText.text = GetDescriptionText();
    }

    public void OnPointerExit(PointerEventData _Event)
    {
        OnDeselect();
    }

    //when deselected
    public void OnDeselect()
    {
        Renderer.sprite = WeaponIcon;
        DetailsText.text = "";
    }

    public void OnPointerClick(PointerEventData _Event)
    {
        OnInteract();
    }

    //on interact
    public void OnInteract()
    {
        weaponInventory.GetSelector().SetWeapon(weaponType);
        weaponInventory.CloseInventory();
    }

    private string GetDescriptionText()
    {
        string _text = "";
        //Add the description for the correct weapon
        switch (weaponType)
        {
            case Weapon.WeaponTypes.Stream:
                _text += "Shoots out a constant stream of bullets.";
                break;
            case Weapon.WeaponTypes.Explosive:
                _text += "Fires an explosive bullet, dealing damage in an area.";
                break;
            case Weapon.WeaponTypes.Fert:
                _text += "Launches a corn seed that explodes into a giant corn. Showering enemies in popcorn.";
                break;
            case Weapon.WeaponTypes.Home:
                _text += "Bullets fired from this weapon home in on the enemy.";
                break;
            case Weapon.WeaponTypes.Bloom://The Shotgun
                _text += "A spread of bullets shot at once. Effective against groups.";
                break;
            default:
                Debug.Log(gameObject + ": Weapon type doesn't exist(Weapon Inventory Item Getting Text)");
                break;
        }
        return _text;
    }


}
