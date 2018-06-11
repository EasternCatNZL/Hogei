using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour {

    public float TweenDuration = 1.0f;
    private Vector3 endValue;
    private Vector3 currentRotation;
    [Header("UI Settings")]
    //public GameObject PrimaryIcon;
    //public GameObject SecondaryIcon;
    public Image PrimaryWeaponIcon;
    public Image SecondaryWeaponIcon;
    public Sprite StreamSprite;
    public Sprite ShotSprite;
    public Sprite FertSprite;
    public Sprite HomeSprite;
    public Sprite ExplSprite;

    private Animator Anim;
    public Sprite PrimSprite;
    public Sprite SeconSprite;
    private Color PrimAlpha;
    private Color SecAlpha;
    private bool isPrimary;

	// Use this for initialization
	void Start () {
        Initialise();
    }

    public void Initialise()
    {
        Anim = gameObject.GetComponent<Animator>();
        isPrimary = true;
        Weapon.WeaponTypes _Pri = PlayerManager.GetInstance().GetPrimary();
        Weapon.WeaponTypes _Sec = PlayerManager.GetInstance().GetSecondary();

       // Weapon _Pri = PlayerManager.GetInstance().Player.GetComponent<PlayerAttack>().PrimaryWeapon;
       // Weapon _Sec = PlayerManager.GetInstance().Player.GetComponent<PlayerAttack>().SecondaryWeapon;        

        
        switch (_Pri)
        {
            case Weapon.WeaponTypes.Stream:
                PrimSprite = StreamSprite;
                break;
            case Weapon.WeaponTypes.Bloom:
                PrimSprite = ShotSprite;
                break;
            case Weapon.WeaponTypes.Fert:
                PrimSprite = FertSprite;
                break;
            case Weapon.WeaponTypes.Home:
                PrimSprite = HomeSprite;
                break;
            case Weapon.WeaponTypes.Explosive:
                PrimSprite = ExplSprite;
                break;
        }
        PrimAlpha = PrimaryWeaponIcon.color;
        PrimaryWeaponIcon.sprite = PrimSprite;
        //Get correct sprite for the Secondary Icon
        switch (_Sec)
        {
            case Weapon.WeaponTypes.Stream:
                SeconSprite = StreamSprite;
                break;
            case Weapon.WeaponTypes.Bloom:
                SeconSprite = ShotSprite;
                break;
            case Weapon.WeaponTypes.Fert:
                SeconSprite = FertSprite;
                break;
            case Weapon.WeaponTypes.Home:
                SeconSprite = HomeSprite;
                break;
            case Weapon.WeaponTypes.Explosive:
                SeconSprite = ExplSprite;
                break;
        }

        SecondaryWeaponIcon.sprite = SeconSprite;
        SecAlpha = SecondaryWeaponIcon.color;
        SecAlpha.a = 0.0f;
        SecondaryWeaponIcon.color = SecAlpha;
        
        

        /*
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
        */
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void NextWeapon()
    {
        
        if(isPrimary)
        {
            Anim.SetTrigger("WeaponChange");
            isPrimary = false;
        }
        else
        {
            isPrimary = true;
            Anim.SetTrigger("WeaponChange");
        }

        /*
        transform.DOComplete();
        currentRotation = transform.localEulerAngles;
        endValue = currentRotation + new Vector3(0, 180, 0);
        transform.DORotate(endValue, TweenDuration, RotateMode.Fast);
        */
    }

    public void SetAlphaToZero()
    {
        //if (PlayerManager.GetInstance().Player.GetComponent<PlayerAttack>().GetWeaponIndex() == 0) isPrimary = true;
        //if (isPrimary)
        //{
        //    PrimAlpha.a = 0.0f;
        //    PrimaryWeaponIcon.color = PrimAlpha;
        //}
        //else
        //{
        //    SecAlpha.a = 0.0f;
        //    SecondaryWeaponIcon.color = SecAlpha;
        //}
        PrimaryWeaponIcon.color = new Color(1f, 1f, 1f, 0f);
    }

    public void SetAlphaToOne()
    {
        //if (PlayerManager.GetInstance().Player.GetComponent<PlayerAttack>().GetWeaponIndex() == 0) isPrimary = true;
        //if (isPrimary)
        //{
        //    SecAlpha.a = 1.0f;
        //    SecondaryWeaponIcon.color = SecAlpha;
        //}
        //else
        //{
        //    PrimAlpha.a = 1.0f;
        //    PrimaryWeaponIcon.color = PrimAlpha;
        //}
    }

    public void SetWeaponIcon()
    {
        PrimaryWeaponIcon.color = new Color(1f, 1f, 1f, 1f);
        int WeaponIndex = PlayerManager.GetInstance().Player.GetComponent<PlayerAttack>().GetWeaponIndex();
        if(WeaponIndex == 0)
        {
            PrimaryWeaponIcon.sprite = PrimSprite;
        }
        else
        {
            PrimaryWeaponIcon.sprite = SeconSprite;
        }
    }
}
