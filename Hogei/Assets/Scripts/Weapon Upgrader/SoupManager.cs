using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoupManager : MonoBehaviour
{
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
    [Header("VFX Settings")]
    public ParticleSystem CompleteVFX;

    // Use this for initialization
    void Start()
    {
        SoupIngredients = new List<SoupIngredient>();
        SoupUpgrades = new Dictionary<Weapon.WeaponEffects, float>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
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
            //Glitter and Sparkles
            if (SoupCapacityText) SoupCapacityText.gameObject.transform.DOShakeScale(0.3f);

            if (CompleteVFX && !CompleteVFX.isPlaying)
            {
                CompleteVFX.Play();
            }
        }
        else
        {
            Debug.Log("No ingredients in the pot to make soup");
        }


        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        SoupIngredient Obj = other.gameObject.GetComponent<SoupIngredient>();
        AddToSoup(Obj);
    }

    //add ingredient to soup
    public void AddToSoup(SoupIngredient Obj)
    {
        if (SoupIngredients.Count < MaxSoupSize)
        {
            if (Obj.Type == SoupIngredient.IngredientType.Chicken)
            {
                bool NoChicken = true;
                foreach (SoupIngredient _Ingr in SoupIngredients)
                {
                    if (_Ingr.Type == SoupIngredient.IngredientType.Chicken)
                    {
                        Obj.GetComponent<SoupIngredient>().SendBackToBowl();
                        NoChicken = false;
                        break;
                    }
                }
                if (NoChicken)
                {
                    //Add ingredient to ingredients list
                    SoupIngredients.Add(Obj);
                    //Add the weapon mods to the upgrade list
                    if (SoupUpgrades.ContainsKey(Obj.WeaponMod.Effect))
                    {
                        SoupUpgrades[Obj.WeaponMod.Effect] += Obj.WeaponMod.Value;
                    }
                    else
                    {
                        SoupUpgrades.Add(Obj.WeaponMod.Effect, Obj.WeaponMod.Value);
                    }
                    if (UpgradeDescText) UpdateUI();
                    if (SoupCapacityText) SoupCapacityText.gameObject.transform.DOShakeScale(0.3f);
                }
            }
            else if (!SoupIngredients.Contains(Obj))
            {
                //Add ingredient to ingredients list
                SoupIngredients.Add(Obj);
                //Add the weapon mods to the upgrade list
                if (SoupUpgrades.ContainsKey(Obj.WeaponMod.Effect))
                {
                    SoupUpgrades[Obj.WeaponMod.Effect] += Obj.WeaponMod.Value;
                }
                else
                {
                    SoupUpgrades.Add(Obj.WeaponMod.Effect, Obj.WeaponMod.Value);
                }
                if (UpgradeDescText) UpdateUI();
                if (SoupCapacityText) SoupCapacityText.gameObject.transform.DOShakeScale(0.3f);
            }
        }
        else
        {
            Obj.GetComponent<SoupIngredient>().SendBackToBowl();
        }
    }

    

    public void UpdateUI()
    {
        if(SoupIngredients.Count > 0)
        {
            UpgradeDescText.text = "Soup Effects:";
            foreach (KeyValuePair<Weapon.WeaponEffects, float> _Upgrade in SoupUpgrades)
            {
                string oldText = UpgradeDescText.text;
                string append = "\n" + _Upgrade.Key + " " + _Upgrade.Value;
                if(_Upgrade.Key != Weapon.WeaponEffects.Damage) append += "%";
                UpgradeDescText.text = oldText + append;
            }          
        }
        else if(PlayerManager.GetInstance().GetPrimarySoup() == null)//If there is no igredients in the soup
        {
            string _newText = "No Soup Upgrades";
            UpgradeDescText.text = _newText;
        }
        else if(PlayerManager.GetInstance().GetPrimarySoup() != null)
        {
            SoupUpgrade _Soup = PlayerManager.GetInstance().GetPrimarySoup();
            string _newText = "Current Soup Effects:";
            int TotalDamage = 0;
            float TotalAngle = 0;
            float TotalFirerate = 0; //Perventages
            float TotalSpeed = 0; //Percentages
            foreach (Weapon.WeaponModifier Mod in _Soup.WeaponModifiers)
            {
                switch (Mod.Effect)
                {
                    case Weapon.WeaponEffects.Damage:
                        TotalDamage += (int)Mod.Value;
                        break;
                    case Weapon.WeaponEffects.Spread:
                        TotalAngle += Mod.Value;
                        break;
                    case Weapon.WeaponEffects.Bullet:
                        break;
                    case Weapon.WeaponEffects.Split:
                        break;
                    case Weapon.WeaponEffects.Firerate:
                        TotalFirerate += Mod.Value;
                        break;
                    case Weapon.WeaponEffects.BulletSpeed:
                        TotalSpeed += Mod.Value;
                        break;
                    default:
                        break;
                }
            }
            print(TotalDamage + " " + TotalAngle + " " + TotalFirerate + " " + TotalSpeed);
            if(TotalDamage > 0)
            {
                _newText += "\nDamage " + TotalDamage;
            }
            if(TotalAngle != 0)
            {
                _newText += "\nSpread " + TotalAngle + "%";
            }
            if(TotalFirerate > 0)
            {
                _newText += "\nFirerate " + TotalFirerate + "%";
            }
            if(TotalSpeed > 0)
            {
                _newText += "\nBullet Speed " + TotalSpeed + "%";
            }
            UpgradeDescText.text = _newText;
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
