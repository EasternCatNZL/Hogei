using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public enum WeaponTypes
    {
        None,
        Stream,
        Fert,
        Lighting,
        Explosive,
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

    public WeaponTypes Type;

    private SoupUpgrade Upgrade;

    public abstract void UseWeapon();
    public abstract void ApplyUpgrade(SoupUpgrade _Upgrade);

    public virtual void SetUpgrade(SoupUpgrade _NewUpgrade) { Upgrade = _NewUpgrade; }
    public virtual SoupUpgrade GetUpgrade() { return Upgrade; }
}
