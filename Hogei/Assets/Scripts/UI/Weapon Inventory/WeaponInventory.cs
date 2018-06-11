using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponInventory : MonoBehaviour {

    public GameObject WeaponInventoryUI;
    public Text DetailsText;
    private WeaponSelector TargetSelector; //Ref to the WeaponSelector calling the weapon inventory
    [Header("Open/Close Transitions Settings")]
    public Transform OpenPosition;
    public Transform ClosePosition;
    public float TransitionDuration = 1f;
    private bool Opened = false;
    private bool Active = true;

    [Header("Menus")]
    public ControllerMenuNavigator navigator;
    public ControllerIndexedMenu openBoxMenu;
    public ControllerIndexedMenu weaponSelectorMenu;

    private void Start()
    {
        transform.DOMove(ClosePosition.position, TransitionDuration);
    }

    public void OpenInventory(WeaponSelector _Selector)
    {
        if (Active && !Opened)
        {
            TargetSelector = _Selector;
            WeaponInventoryUI.transform.DOMove(OpenPosition.position, TransitionDuration);
            Opened = true;
            //change menu
            navigator.SetMenu(weaponSelectorMenu);
        }
    }

    public void CloseInventory()
    {
        WeaponInventoryUI.transform.DOMove(ClosePosition.position, TransitionDuration);
        Opened = false;
        //change menu
        navigator.SetMenu(openBoxMenu);
    }

    public Text GetDetailsText() { return DetailsText; }
    public WeaponSelector GetSelector() { return TargetSelector; }
    public bool CheckUnlocked(Weapon.WeaponTypes _Type)
    {
        Dictionary<Weapon.WeaponTypes, bool> Unlocks = PlayerManager.GetInstance().GetWeaponUnlocks();
        if (Unlocks.ContainsKey(_Type))
        {
            return Unlocks[_Type];
        }
        else
        {
            Unlocks.Add(_Type, false);
            return Unlocks[_Type];
        }
    }

    public void SetActive(bool _NewState) { Active = _NewState; }
}
