using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoupManager : MonoBehaviour {



    public GameObject TheSoup;
    [Tooltip("The angle the soup rotates each second")]
    public float SoupRotationSpeed = 90f;
    public List<SoupIngredient> SoupIngredients;
    public Dictionary<SoupIngredient.IngredientType, int> SoupUpgrades;
    public List<GameObject> IngredientPrefabs;
    [Header("Spawn Positions")]
    public Transform IngredientSpawn;
    public Transform SoupSpawn;
    [Header("UI Settings")]
    public Text UpgradeDescText;

	// Use this for initialization
	void Start () {
        SoupIngredients = new List<SoupIngredient>();
        SoupUpgrades = new Dictionary<SoupIngredient.IngredientType, int>();
    }
	
	// Update is called once per frame
	void Update () {       
        TheSoup.transform.Rotate(new Vector3(0f, 0f, SoupRotationSpeed * Time.deltaTime));
	}

    public void CompleteSoup()
    {
        if (SoupIngredients.Count > 0)
        {
            GameObject NewSoupUpgrade = new GameObject();
            NewSoupUpgrade.name = "SoupUpgrade";
            NewSoupUpgrade.transform.parent = PlayerManager.GetInstance().gameObject.transform;
            NewSoupUpgrade.AddComponent<SoupUpgrade>();
            foreach (SoupIngredient Effect in SoupIngredients)
            {
                NewSoupUpgrade.GetComponent<SoupUpgrade>().AddModifier(Effect);
            }
            PlayerManager.GetInstance().AddSoupInventory(NewSoupUpgrade.GetComponent<SoupUpgrade>());
            ClearSoup();
        }
        else
        {
            Debug.Log("No ingredients in the pot to make soup");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SoupIngredient Obj = other.gameObject.GetComponent<SoupIngredient>();
        if (!SoupIngredients.Contains(Obj))
        {
            SoupIngredients.Add(Obj);

            if(SoupUpgrades.ContainsKey(Obj.Type))
            {
                SoupUpgrades[Obj.Type] += 1;
            }
            else
            {
                SoupUpgrades.Add(Obj.Type, 1);
            }
            if (UpgradeDescText) UpdateDescriptionText();
        }
    }

    private void UpdateDescriptionText()
    {
        UpgradeDescText.text = "";
        foreach (KeyValuePair<SoupIngredient.IngredientType,int> Upgrade in SoupUpgrades)
        {
            SoupIngredient Ingred = IngredientPrefabs[(int)Upgrade.Key].GetComponent<SoupIngredient>();
            string oldText = UpgradeDescText.text;
            string append = "\n" + Ingred.WeaponMod.Effect.ToString() + " " + Ingred.WeaponMod.Value * Upgrade.Value;
            UpgradeDescText.text = oldText + append;
        }
    }

    private void ClearDescriptionText()
    {
        UpgradeDescText.text = "";
    }

    private void SpawnIngredients()
    {

    }

    private void ClearSoup()
    {
        for(int i = SoupIngredients.Count - 1; i >= 0; --i)
        {
            Destroy(SoupIngredients[i].gameObject);
        }
        SoupIngredients.Clear();
    }
}
