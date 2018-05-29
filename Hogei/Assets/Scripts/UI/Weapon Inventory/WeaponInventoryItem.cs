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

    private Text DetailsText;

	// Use this for initialization
	void Start () {
        weaponInventory = GetComponentInParent<WeaponInventory>();
        DetailsText = weaponInventory.GetDetailsText();
        Locked = weaponInventory.CheckUnlocked(weaponType);
	}

    public void OnPointerEnter(PointerEventData _Event)
    {
        DetailsText.text = GetDescriptionText();
    }

    public void OnPointerExit(PointerEventData _Event)
    {
        DetailsText.text = "";
    }

    public void OnPointerClick(PointerEventData _Event)
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
                _text += "Fire an explosive bullet, dealing damage in an area.";
                break;
            case Weapon.WeaponTypes.Fert:
                _text += "Launches a corn seed that explodes into a giant corn. Showering enemies in popcorn.";
                break;
            case Weapon.WeaponTypes.Home:
                _text += "Bullet fired from this weapon home in on enemies.";
                break;
            case Weapon.WeaponTypes.Bloom://The Shotgun
                _text += "A group of bullets a shot at once. Effect against groups.";
                break;
            default:
                Debug.Log(gameObject + ": Weapon type doesn't exist(Weapon Inventory Item Getting Text)");
                break;
        }
        return _text;
    }


}
