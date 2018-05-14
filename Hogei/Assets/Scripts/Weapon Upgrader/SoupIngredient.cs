using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoupIngredient : MonoBehaviour {

    public bool Spin = true;

    public AudioClip PickupSounds;

    public enum IngredientType
    {
        Lamb,
        HornedLamb,
        Whiskey,
        Cactus,
        Chicken
    }

    public IngredientType Type;
    public bool IsWeaponEffect = false;
    public Weapon.WeaponModifier WeaponMod;
    public float rotationSpeed = 90f;

    private void Update()
    {
        if(Spin) transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public Weapon.WeaponModifier GetModifier() { return WeaponMod; }

    public static int GetIngredientTypeCount() { return System.Enum.GetNames(typeof(IngredientType)).Length; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.GetInstance().AddIngredientInventory(this);
            if (PickupSounds) MusicManager.GetInstance().PlaySoundAtLocation(PickupSounds, transform.position);
            Destroy(gameObject);
        }
    }

}
