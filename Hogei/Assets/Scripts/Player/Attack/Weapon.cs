using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    [Header("Bullet vars")]
    [Tooltip("Bullet object")]
    public GameObject bulletObject;
    [Tooltip("Bullet Damange")]
    public int BulletDamage = 1;
    public int OriginalBulletDamage;
    [Tooltip("Bullet travel speed")]
    public float bulletTravelSpeed = 10.0f;
    public float OriginalBulletTravelSpeed;
    [Header("Timing vars")]
    [Tooltip("The amount of time between shots")]
    public float timeBetweenShots = 2.0f;
    [Tooltip("Starting time between shots")]
    public float OriginalTimeBetweenShots;
    [Header("Angle variance")]
    [Tooltip("Angle variance in shot")]
    public float angleVariance = 2.0f;
    public float OriginalAngleVariance;
    [Header("Bullet life time")]
    public float bulletLifeTime = 5.0f;

    [Header("Positioning vars")]
    [Tooltip("How far out to position bullet start from center")]
    public float distanceToStart = 0.1f;
    [Tooltip("Where the gun barrel is located")]
    public Transform barrelLocation;
    public bool isFiring = false;

    [Header("Audio")]
    public AudioClip bulletFireSound;
    [Range(0, 1)]
    public float FireVolume = 1f;
    public Vector2 PitchVarianceRange;

    [Header("VFX")]
    public GameObject muzzleFireVFX;

    //control vars
    protected float lastShotTime = 0.0f; //time last bullet was fired

    public enum WeaponTypes
    {
        None,
        Stream,
        Fert,
        Lighting,
        Explosive,
        Bloom,
        Home
    }

    public enum WeaponEffects
    {
        Spread,
        Damage,
        Bullet,
        Split,
        Firerate,
        BulletSpeed
    }

    [System.Serializable]
    public struct WeaponModifier
    {
        public WeaponEffects Effect;
        public float Value;
    }

    public struct WeaponStats
    {
        public int BulletDamage;
        public float TimeBetweenShots;
        public float angleVariance;
        public float BulletSpeed;
    }

    public WeaponTypes Type;

    private SoupUpgrade Upgrade;

    public abstract void UseWeapon();
    public abstract void ApplyUpgrade(SoupUpgrade _Upgrade);

    public virtual void SetUpgrade(SoupUpgrade _NewUpgrade) { Upgrade = _NewUpgrade; }
    public virtual SoupUpgrade GetUpgrade() { return Upgrade; }
}
