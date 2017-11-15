using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponImageManager : MonoBehaviour {

    [Header("Weapon Sprites")]
    [Tooltip("Array of weapon sprites")]
    public Sprite[] weaponImageArray = new Sprite[0];

    [Header("Image refs")]
    public Image currentWeaponImage;
    public Image prevWeaponImage;
    public Image nextWeaponImage;

    [Header("Tags")]
    public string playerTag = "Player";

    //script refs
    private PlayerAttack pAttack;

	// Use this for initialization
	void Start () {
        //pAttack = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!pAttack)
        {
            pAttack = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerAttack>();
        }
        ManageWeaponImages();
	}

    //Manage the images based on player current weapon
    private void ManageWeaponImages()
    {
        //get the index
        int currentWeapon = pAttack.GetWeaponIndex();
        //set the current weapon to this index
        currentWeaponImage.sprite = weaponImageArray[currentWeapon];
        //set the prev
        if (currentWeapon == 0)
        {
            prevWeaponImage.sprite = weaponImageArray[weaponImageArray.Length - 1];
        }
        else
        {
            prevWeaponImage.sprite = weaponImageArray[currentWeapon - 1];
        }
        //set the next
        if (currentWeapon == weaponImageArray.Length - 1)
        {
            nextWeaponImage.sprite = weaponImageArray[0];
        }
        else
        {
            nextWeaponImage.sprite = weaponImageArray[currentWeapon + 1];
        }
    }
}
