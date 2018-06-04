using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponWheel : MonoBehaviour {

    public float TweenDuration = 1.0f;
    private Vector3 endValue;
    private Vector3 currentRotation;
    [Header("UI Settings")]
    public GameObject PrimaryIcon;
    public GameObject SecondaryIcon;
    public Sprite StreamSprite;
    public Sprite ShotSprite;
    public Sprite FertSprite;
    public Sprite HomeSprite;
    public Sprite ExplSprite;

	// Use this for initialization
	void Start () {
        Initialise();
	}

    public void Initialise()
    {
        Weapon.WeaponTypes _Pri = PlayerManager.GetInstance().GetPrimary();
        Weapon.WeaponTypes _Sec = PlayerManager.GetInstance().GetSecondary();
        //Get correct sprite for the Primary Icon
        Sprite SpriteToUse = null;
        switch(_Pri)
        {
            case Weapon.WeaponTypes.Stream:
                SpriteToUse = StreamSprite;
                break;
            case Weapon.WeaponTypes.Bloom:
                SpriteToUse = ShotSprite;
                break;
            case Weapon.WeaponTypes.Fert:
                SpriteToUse = FertSprite;
                break;
            case Weapon.WeaponTypes.Home:
                SpriteToUse = HomeSprite;
                break;
            case Weapon.WeaponTypes.Explosive:
                SpriteToUse = ExplSprite;
                break;
        }
        PrimaryIcon.GetComponent<SpriteRenderer>().sprite = SpriteToUse;
        //Get correct sprite for the Secondary Icon
        SpriteToUse = null;
        switch (_Sec)
        {
            case Weapon.WeaponTypes.Stream:
                SpriteToUse = StreamSprite;
                break;
            case Weapon.WeaponTypes.Bloom:
                SpriteToUse = ShotSprite;
                break;
            case Weapon.WeaponTypes.Fert:
                SpriteToUse = FertSprite;
                break;
            case Weapon.WeaponTypes.Home:
                SpriteToUse = HomeSprite;
                break;
            case Weapon.WeaponTypes.Explosive:
                SpriteToUse = ExplSprite;
                break;
        }
        SecondaryIcon.GetComponent<SpriteRenderer>().sprite = SpriteToUse;
    }
	
	// Update is called once per frame
	void Update () {

        	
	}

    public void NextWeapon()
    {
        transform.DOComplete();
        currentRotation = transform.localEulerAngles;
        endValue = currentRotation + new Vector3(0, 180, 0);
        transform.DORotate(endValue, TweenDuration, RotateMode.Fast);
        
    }
}
