using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingShot : Weapon {

    [Tooltip("Max time bullet has homing propeties")]
    public float maxHomingTime = 4.0f;
    [Tooltip("Delay time before bullet begins homing propeties")]
    public float homingStartDelay = 1.0f;

    // Use this for initialization
    void Start () {
        Type = WeaponTypes.Home;
        OriginalTimeBetweenShots = timeBetweenShots;
        //OriginalAngleVariance = angleVariance;
        OriginalBulletTravelSpeed = bulletTravelSpeed;
        OriginalBulletDamage = BulletDamage;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //attack use logic
    public override void UseWeapon()
    {
        //check if input 
        if (Time.time > lastShotTime + timeBetweenShots)
        {
            //set last shot time to now
            lastShotTime = Time.time;

            //get a bullet
            GameObject bullet = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet.transform.position = transform.position + (transform.right * distanceToStart);
            //set the bullet's rotation to current rotation
            bullet.transform.rotation = transform.rotation;
            //set up bullet
            bullet.GetComponent<PlayerHomingBullet>().SetupVars(bulletTravelSpeed, maxHomingTime, homingStartDelay);

            //get a second bullet
            GameObject bullet2 = Instantiate(bulletObject, transform.position, transform.rotation);
            //set the bullets position to this pos
            bullet2.transform.position = transform.position + (-transform.right * distanceToStart);
            //set the bullet's rotation to current rotation
            bullet2.transform.rotation = transform.rotation;
            //set up bullet
            bullet2.GetComponent<PlayerHomingBullet>().SetupVars(bulletTravelSpeed, maxHomingTime, homingStartDelay);

            //play audio
            MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
            SoundSettings.Pitch = 1f;
            SoundSettings.SpatialBlend = 0f;
            MusicManager.GetInstance().PlaySoundAtLocation(bulletFireSound, transform.position, SoundSettings);
        }
    }

    public override void ApplyUpgrade(SoupUpgrade _Upgrade)
    {
        foreach (WeaponModifier Mod in _Upgrade.WeaponModifiers)
        {
            switch (Mod.Effect)
            {
                //case WeaponEffects.Spread:
                //    angleVariance = OriginalAngleVariance * Mod.Value;
                    //break;
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
