using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingShot : Weapon
{

    [Tooltip("Max time bullet has homing propeties")]
    public float maxHomingTime = 4.0f;
    [Tooltip("Delay time before bullet begins homing propeties")]
    public float homingStartDelay = 1.0f;

    private static bool Initialised = false;
    private static WeaponStats OriginalStats;

    // Use this for initialization
    void Start()
    {
        Type = WeaponTypes.Home;
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
        int TotalDamage = 0;
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
        timeBetweenShots = OriginalStats.TimeBetweenShots - (OriginalStats.TimeBetweenShots * TotalFirerate);
        bulletTravelSpeed = OriginalStats.BulletSpeed + (OriginalStats.BulletSpeed * TotalSpeed);
    }
}
