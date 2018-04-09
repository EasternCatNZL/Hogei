using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupUpgrade : MonoBehaviour {

    public string Name = "";
    public Weapon.WeaponTypes WeaponToUpgrade;
    public List<Weapon.WeaponModifier> WeaponModifiers;

    void OnEnable()
    {
        if(WeaponModifiers == null) WeaponModifiers = new List<Weapon.WeaponModifier>();
    }

    public void AddModifier(SoupIngredient _NewModifier) { WeaponModifiers.Add(_NewModifier.GetModifier()); }

    public void SetWeapon(Weapon.WeaponTypes _NewWeapon) { WeaponToUpgrade = _NewWeapon; }
 }
