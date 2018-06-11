using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    private Vector3 OriginalScale;

    void Start()
    {
        PlayMgt = PlayerManager.GetInstance();
        IngredientAmount = PlayMgt.GetIngredientAmount(IngredientType);
        OriginalScale = transform.localScale;
        if (IngredientAmount <= 0)
        {
            Hide();
        }
    }

    private void OnMouseOver()
    {
        Highlighted();
    }

    //when highlighted
    public void Highlighted()
    {
        if (TextDisplay)
        {
            Weapon.WeaponModifier WeaponMod = IngredientPrefab.GetComponent<SoupIngredient>().WeaponMod;
            if (WeaponMod.Value < -1 & WeaponMod.Value < 0) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":    " + WeaponMod.Value;
            else if (WeaponMod.Value > -1 && WeaponMod.Value < 0) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":   " + WeaponMod.Value + "%";
            else if (WeaponMod.Value > 0 && WeaponMod.Value < 1) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":    +" + WeaponMod.Value + "%";
            else if (WeaponMod.Value >= 1) TextDisplay.text = IngredientType.ToString() + "\n" + WeaponMod.Effect + ":    +" + WeaponMod.Value;

        }
    }

    private void OnMouseExit()
    {
        SoupManager.UpdateUI();
    }

    //In-built function called when collider is clicked down on
    void OnMouseDown()
    {
        IngredientSelected();
    }

    //when object seleceted
    public void IngredientSelected()
    {
        if (IngredientAmount > 0)
        {
            GameObject Ingred = Instantiate(IngredientPrefab);
            SoupManager.GetComponent<ItemGrabbing>().SetHeldItem(Ingred);
            IngredientAmount -= 1;
            Ingred.GetComponent<SoupIngredient>().myBowl = this;
        }
        if (IngredientAmount <= 0)
        {
            Hide();
        }
    }

    //Add straight to soup
    public void AddIngredientToSoup()
    {
        //if ingredients exists
        if(IngredientAmount > 0)
        {
            //create copy of soup ingredient
            GameObject ingreClone = Instantiate(IngredientPrefab);
            //add ingredient to soup
            SoupManager.AddToSoup(ingreClone.GetComponent<SoupIngredient>());
            //decrement the ingredient amount
            IngredientAmount--;
        }
        //if all ingredients used
        if (IngredientAmount <= 0)
        {
            Hide();
        }
    }

    public void Show()
    {
        transform.DOScale(OriginalScale, 0.5f);
        GetComponent<MeshCollider>().enabled = true;
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, 0.5f);
        GetComponent<MeshCollider>().enabled = false;
    }
}
