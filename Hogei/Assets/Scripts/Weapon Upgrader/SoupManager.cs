using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoupManager : MonoBehaviour {

    public enum WeaponEffects
    {
        SPREAD,
        DAMAGE,
        BULLET,
        SPLIT,
        FIRERATE
    }

    [System.Serializable]
    public struct WeaponEffect
    {
        public WeaponEffects Effect;
        public float Value;
    }

    public GameObject TheSoup;
    [Tooltip("The angle the soup rotates each second")]
    public float SoupRotationSpeed = 90f;
    public List<SoupIngredient> SoupIngredients;
    [Header("UI Settings")]
    public Text UpgradeDescText;

	// Use this for initialization
	void Start () {
        SoupIngredients = new List<SoupIngredient>();
	}
	
	// Update is called once per frame
	void Update () {       
        TheSoup.transform.Rotate(new Vector3(0f, 0f, SoupRotationSpeed * Time.deltaTime));
	}

    private void OnTriggerEnter(Collider other)
    {
        SoupIngredient Obj = other.gameObject.GetComponent<SoupIngredient>();
        if (!SoupIngredients.Contains(Obj))
        {
            SoupIngredients.Add(Obj);
            UpdateDescriptionText();
        }
    }

    private void UpdateDescriptionText()
    {
        SoupIngredient Upgrade = SoupIngredients[SoupIngredients.Count - 1].GetComponent<SoupIngredient>();
        string newDesc = UpgradeDescText.text;
        string append = "\n" + Upgrade.WeaponEffect.Effect.ToString() + " " + Upgrade.WeaponEffect.Value;
        UpgradeDescText.text = newDesc + append;
    }
}
