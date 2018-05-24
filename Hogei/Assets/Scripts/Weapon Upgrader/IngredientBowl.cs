﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBowl : MonoBehaviour {

    [Header("Ingredient Settings")]
    public SoupIngredient.IngredientType IngredientType;
    public GameObject IngredientPrefab;

    [Header("Soup Settings")]
    public SoupManager SoupManager;

    [Header("Text Settings")]
    public Text TextDisplay;

    public int IngredientAmount = 0;
    private PlayerManager PlayMgt;

    void Start()
    {
        PlayMgt = PlayerManager.GetInstance();
        IngredientAmount = PlayMgt.GetIngredientAmount(IngredientType);
        if(IngredientAmount <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if (TextDisplay)
        {
            Weapon.WeaponModifier WeaponMod = IngredientPrefab.GetComponent<SoupIngredient>().WeaponMod;
            if(WeaponMod.Value < -1 & WeaponMod.Value < 0) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":    " + WeaponMod.Value;
            else if (WeaponMod.Value > -1 && WeaponMod.Value < 0) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":   " + WeaponMod.Value + "%";
            else if (WeaponMod.Value > 0 && WeaponMod.Value < 1) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":    +" + WeaponMod.Value +"%";
            else if(WeaponMod.Value >= 1) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":    +" + WeaponMod.Value;

        }
    }

    private void OnMouseExit()
    {
        SoupManager.UpdateUI();
    }

    //In-built function called when collider is clicked down on
    void OnMouseDown()
    {
        if (IngredientAmount > 0)
        {
            GameObject Ingred = Instantiate(IngredientPrefab);
            SoupManager.GetComponent<ItemGrabbing>().SetHeldItem(Ingred);
            IngredientAmount -= 1;
        }
    }
}
