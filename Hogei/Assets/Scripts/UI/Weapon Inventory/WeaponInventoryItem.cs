using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponInventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public WeaponInventory weaponInventory;
    public Weapon.WeaponTypes weaponType;

    private Text DetailsText;

	// Use this for initialization
	void Start () {
        weaponInventory = GetComponentInParent<WeaponInventory>();
        DetailsText = weaponInventory.GetDetailsText();
	}

    public void OnPointerEnter(PointerEventData _Event)
    {
        DetailsText.text = weaponType.ToString();
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
	
	
}
