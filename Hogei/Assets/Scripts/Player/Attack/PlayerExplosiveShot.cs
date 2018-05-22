using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosiveShot : Weapon {

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

    [Header("Positioning vars")]
    [Tooltip("How far out to position bullet start from center")]
    public float distanceToStart = 0.1f;
    [Tooltip("Where the gun barrel is located")]
    public Transform barrelLocation;
    public bool isFiring = false;

    [Header("VFX")]
    public GameObject muzzleFireVFX;

    [Header("Audio")]
    public AudioClip bulletFireSound;

    //control vars
    private float lastShotTime = 0.0f; //time last bullet was fired

    // Use this for initialization
    void Start () {
        Type = WeaponTypes.Explosive;
        OriginalTimeBetweenShots = timeBetweenShots;
        OriginalAngleVariance = angleVariance;
        OriginalBulletTravelSpeed = bulletTravelSpeed;
        OriginalBulletDamage = BulletDamage;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void UseWeapon()
    {
        if (Time.time > lastShotTime + timeBetweenShots)
        {
            //Play VFX
            if (muzzleFireVFX)
            {
                //Instantiate(muzzleFireVFX, barrelLocation.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, -90f, 0f)));
                Instantiate(muzzleFireVFX, barrelLocation);
            }
            //set last shot time to now
            lastShotTime = Time.time;

            ////get random variance
            //float random = UnityEngine.Random.Range(-angleVariance, angleVariance);

            //get a bullet
            GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            isFiring = true;
            //set the bullets position to this pos
            bullet.transform.position = barrelLocation.position + (transform.right * distanceToStart);
            //set the bullet's rotation with some variance
            bullet.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y /*+ random*/, 0.0f);
            //set up bullet
            bullet.GetComponent<PlayerExplosiveBullet>().SetupVars(bulletTravelSpeed, 0, false, BulletDamage);

            //get a second bullet
            GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet2.transform.position = barrelLocation.position + (-transform.right * distanceToStart);
            //set the bullet's rotation to current rotation
            bullet2.transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y /*+ random*/, 0.0f);
            //set up bullet
            bullet2.GetComponent<PlayerExplosiveBullet>().SetupVars(bulletTravelSpeed, 0, false, BulletDamage);

            //bulletFireSound.pitch = UnityEngine.Random.Range(PitchVarianceRange.x, PitchVarianceRange.y);
            //bulletFireSound.volume = FireVolume;
            //play audio
            MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
            SoundSettings.Pitch = 1f;
            SoundSettings.SpatialBlend = 0f;
            //SoundSettings.Volume = HitSoundVol;
            MusicManager.GetInstance().PlaySoundAtLocation(bulletFireSound, transform.position, SoundSettings);
        }
    }

    public override void ApplyUpgrade(SoupUpgrade _Upgrade)
    {
        foreach (WeaponModifier Mod in _Upgrade.WeaponModifiers)
        {
            switch (Mod.Effect)
            {
                case WeaponEffects.Spread:
                    angleVariance = OriginalAngleVariance * Mod.Value;
                    break;
                case WeaponEffects.Damage:
                    BulletDamage = OriginalBulletDamage + (int)Mod.Value;
                    break;
                case WeaponEffects.Bullet:
                    break;
                case WeaponEffects.Split:
                    break;
                case WeaponEffects.Firerate:
                    timeBetweenShots = OriginalTimeBetweenShots * Mod.Value;
                    break;
                default:
                    break;
            }
        }
    }
}
