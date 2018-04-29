using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBowl : MonoBehaviour {

    [Header("Ingredient Settings")]
    public SoupIngredient.IngredientType IngredientType;
    public GameObject IngredientPrefab;

    [Header("Soup Settings")]
    public SoupManager SoupManager;

    public int IngredientAmount = 0;
    private PlayerManager PlayMgt;

    void Start()
    {
        PlayMgt = PlayerManager.GetInstance();
        IngredientAmount = PlayMgt.GetIngredientAmount(IngredientType);
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
