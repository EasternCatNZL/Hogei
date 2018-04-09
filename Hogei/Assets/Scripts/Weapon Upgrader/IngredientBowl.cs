using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBowl : MonoBehaviour {

    [Header("Ingredient Settings")]
    public SoupIngredient.IngredientType IngredientType;
    public GameObject IngredientPrefab;

    [Header("Soup Settings")]
    public SoupManager SoupManager;

    private PlayerManager PlayMgt;

    void Start()
    {
        PlayMgt = PlayerManager.GetInstance();
    }

    //Unity function called when collider is clicked down on
    void OnMouseDown()
    {
        GameObject Ingred = Instantiate(IngredientPrefab);
        SoupManager.GetComponent<ItemGrabbing>().SetHeldItem(Ingred);
    }
}
