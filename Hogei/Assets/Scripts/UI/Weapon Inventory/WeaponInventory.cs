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

    private void Start()
    {
        transform.DOMove(ClosePosition.position, TransitionDuration);
    }

    public void OpenInventory(WeaponSelector _Selector)
    {
        TargetSelector = _Selector;
        WeaponInventoryUI.transform.DOMove(OpenPosition.position, TransitionDuration);
    }

    public void CloseInventory()
    {
        WeaponInventoryUI.transform.DOMove(ClosePosition.position, TransitionDuration);
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
}
