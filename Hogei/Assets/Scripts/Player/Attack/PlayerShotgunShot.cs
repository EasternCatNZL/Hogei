using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotgunShot : Weapon {

    [Header("Bloom vars")]
    [Tooltip("Number of bullets in bloom")]
    public int numBulletsInBloom;

    [Header("Speed variation")]
    [Tooltip("+- speed variation to bullets")]
    public float bulletSpeedVariation = 2.0f;

    // Use this for initialization
    void Start () {
        Type = WeaponTypes.Bloom;
        OriginalTimeBetweenShots = timeBetweenShots;
        OriginalAngleVariance = angleVariance;
        OriginalBulletTravelSpeed = bulletTravelSpeed;
        OriginalBulletDamage = BulletDamage;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //attack use logic
    public override void UseWeapon()
    {
        //check timing
        if(Time.time > lastShotTime + timeBetweenShots)
        {
            //Play VFX
            if (muzzleFireVFX)
            {
                //Instantiate(muzzleFireVFX, barrelLocation.position, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, -90f, 0f)));
                Instantiate(muzzleFireVFX, barrelLocation);
            }

            //set last shot time to now
            lastShotTime = Time.time;

            //for all shots in bloom
            for(int i = 0; i < numBulletsInBloom; i++)
            {
                //get random angle within variance
                float randomAngle = Random.Range(-angleVariance, angleVariance);
                float randomSpeed = bulletTravelSpeed + Random.Range(-bulletSpeedVariation, bulletSpeedVariation);

                //spawn bullet
                GameObject bulletClone = Instantiate(bulletObject, barrelLocation.position, Quaternion.Euler(0.0f, randomAngle + transform.rotation.eulerAngles.y, 0.0f));
                bulletClone.GetComponent<PlayerStraightBullet>().SetupVars(randomSpeed, 0, false, BulletDamage, bulletLifeTime);

                if (bulletFireSound)
                {
                    //play audio
                    MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
                    SoundSettings.Pitch = 1f;
                    SoundSettings.SpatialBlend = 0f;
                    //SoundSettings.Volume = HitSoundVol;
                    MusicManager.GetInstance().PlaySoundAtLocation(bulletFireSound, transform.position, SoundSettings);
                }
                
            }
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
