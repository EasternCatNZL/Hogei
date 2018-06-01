using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosiveShot : Weapon {

    private static bool Initialised = false;
    private static WeaponStats OriginalStats;

    // Use this for initialization
    void Start () {
        Type = WeaponTypes.Explosive;
        if (!Initialised)
        {
            Debug.Log(gameObject.transform.parent.name + "->" + gameObject.name + "->" + this.GetType() + ": Initialising this weapon.");
            OriginalStats.BulletDamage = BulletDamage;
            OriginalStats.angleVariance = angleVariance;
            OriginalStats.BulletSpeed = bulletTravelSpeed;
            OriginalStats.TimeBetweenShots = timeBetweenShots;
            Initialised = true;
        }
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
        int TotalDamage = 0;
        float TotalAngle = 0;
        float TotalFirerate = 0; //Perventages
        float TotalSpeed = 0; //Percentages
        //Collect all effects into totals
        foreach (WeaponModifier Mod in _Upgrade.WeaponModifiers)
        {
            switch (Mod.Effect)
            {
                case WeaponEffects.Damage:
                    TotalDamage += (int)Mod.Value;
                    break;
                case WeaponEffects.Spread:
                    TotalAngle += Mod.Value;
                    break;
                case WeaponEffects.Bullet:
                    break;
                case WeaponEffects.Split:
                    break;
                case WeaponEffects.Firerate:
                    TotalFirerate += Mod.Value;
                    break;
                case WeaponEffects.BulletSpeed:
                    TotalSpeed += Mod.Value;
                    break;
                default:
                    break;
            }
        }
        //Apply the effects to the weapon
        BulletDamage = OriginalStats.BulletDamage + TotalDamage;
        angleVariance = OriginalStats.angleVariance + (OriginalStats.angleVariance * TotalAngle);
        timeBetweenShots = OriginalStats.TimeBetweenShots - (OriginalStats.TimeBetweenShots * TotalFirerate);
        bulletTravelSpeed = OriginalStats.BulletSpeed + (OriginalStats.BulletSpeed * TotalSpeed);
    }
}
