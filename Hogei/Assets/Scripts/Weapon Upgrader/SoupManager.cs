using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoupManager : MonoBehaviour {



    public GameObject TheSoup;
    [Tooltip("The angle the soup rotates each second")]
    public float SoupRotationSpeed = 90f;
    [Header("Soup Settings")]
    public int MaxSoupSize = 4;
    public List<SoupIngredient> SoupIngredients;
    public Dictionary<Weapon.WeaponEffects, float> SoupUpgrades;
    public List<GameObject> IngredientPrefabs;
    [Header("UI Settings")]
    public Text UpgradeDescText;
    public TextMesh SoupCapacityText;

	// Use this for initialization
	void Start () {
        SoupIngredients = new List<SoupIngredient>();
        SoupUpgrades = new Dictionary<Weapon.WeaponEffects, float>();
        UpdateUI();
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
            PlayerManager.GetInstance().SetPrimarySoup(NewSoupUpgrade.GetComponent<SoupUpgrade>());
            PlayerManager.GetInstance().SetSecondarySoup(NewSoupUpgrade.GetComponent<SoupUpgrade>());
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
        if (SoupIngredients.Count < MaxSoupSize && !SoupIngredients.Contains(Obj))
        {         
            //Add ingredient to ingredients list
            SoupIngredients.Add(Obj);
            //Add the weapon mods to the upgrade list
            if(SoupUpgrades.ContainsKey(Obj.WeaponMod.Effect))
            {
                SoupUpgrades[Obj.WeaponMod.Effect] += Obj.WeaponMod.Value;
            }
            else
            {
                SoupUpgrades.Add(Obj.WeaponMod.Effect,Obj.WeaponMod.Value);
            }
            if (UpgradeDescText) UpdateUI();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    public void UpdateUI()
    {
        UpgradeDescText.text = "Soup Effects:";
        foreach (KeyValuePair<Weapon.WeaponEffects,float> _Upgrade in SoupUpgrades)
        {           
            string oldText = UpgradeDescText.text;
            string append = "\n" + _Upgrade.Key + " " + _Upgrade.Value;
            UpgradeDescText.text = oldText + append;
        }
        SoupCapacityText.text = SoupIngredients.Count + "/" + MaxSoupSize;
    }

    private void ClearDescriptionText()
    {
        UpgradeDescText.text = "";
    }

    private void SpawnIngredients()
    {

    }

    public void ClearSoup()
    {
        //Clear Soup Ingredients List
        for (int i = SoupIngredients.Count - 1; i >= 0; --i)
        {
            if (SoupIngredients[i].gameObject)
            {
                Destroy(SoupIngredients[i].gameObject);
            }
            
        }
        SoupIngredients.Clear();
        //Clear Soup Upgrades List
        SoupUpgrades.Clear();
    }
}
