using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class EntityHealth : MonoBehaviour {
    

    public delegate void DeathEvent();
    public static event DeathEvent OnDeath;

    public delegate void PlayerHealthEvent();
    public static event PlayerHealthEvent OnPlayerHealthUpdate;

    public bool isHit = false;

    public enum StatusEffects
    {
        CHAINLIGHTING,
        STUNNED,
    }

    private bool ChainLighting = false;
    private bool Stunned = false;
    
    bool InvincibilityFrame = false;
    bool FlashBack = false;
    float LastTime = 0f;

    [Header("Health Settings")]
    public float CurrentHealth;
    public float MaxHealth = 10;
    [Header("VFX Settings")]
    public bool OnHitShake = false;
    public GameObject[] DeathVFX;
    public GameObject HitVFX;
    public bool ModHitVFX = false;
    public Vector3 HitVFXScale;
    public bool HitAnimationOn = false;
    [Header("Sound Settings")]
    public bool StackSounds = false;
    [Range(0f,1f)]
    public float HitSoundVol = 1f;
    public Vector2 HitSoundPitchRange = Vector2.zero;
    public AudioClip[] HitSound = null;
    public AudioClip FinalHitSound = null;
    private AudioSource LastSound;

    public UnityEvent DeathFunction;

    //[Header("Audio")]
    //public AudioSource deathSound;

    bool DOTActive;
    float DOTDamage;
    float DOTDuration;
    float DOTStart;

	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
        if (GetComponent<MeshRenderer>()) GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        else if (GetComponentInChildren<SkinnedMeshRenderer>()) GetComponentInChildren<SkinnedMeshRenderer>().material.EnableKeyword("_EMISSION");
        //deathSound.playOnAwake = false;

    }

    private void OnDestroy()
    {
        if(gameObject.tag == "Enemy") if(OnDeath != null) OnDeath();
    }

    // Update is called once per frame
    void Update () {
        if(CurrentHealth <= 0.0f)
        {          
            if (gameObject.tag == "Enemy")
            {
                if(GetComponent<Drops>())GetComponent<Drops>().OnDeathDrop();
                //for room enemies
                if (transform.parent && transform.parent.GetComponent<RoomEnemyManager>())
                {
                    transform.parent.GetComponent<RoomEnemyManager>().enemyList.Remove(gameObject);
                }
            }
            if(FinalHitSound)
            {
                MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
                SoundSettings.Pitch = 1f;
                SoundSettings.SpatialBlend = 0f;
                SoundSettings.Volume = HitSoundVol;
                MusicManager.PlaySoundAtLocation(FinalHitSound, transform.position, SoundSettings);
            }
            if (DeathVFX.Length > 0)
            {
                foreach (GameObject vfx in DeathVFX)
                {
                    if (vfx)
                    {
                        Instantiate(vfx, transform.position, Quaternion.Euler(-90f, 0f, 0f));
                    }
                }
            }

            DeathFunction.Invoke();
            //If the player has zero health then disable their renderer
            if (gameObject.CompareTag("Player"))
            {
                Camera.main.GetComponentInParent<Follow>().SetStopFollowing(true);
                gameObject.transform.position += new Vector3(0f, 1000f, 0f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
		if(DOTActive)
        {
            CurrentHealth -= DOTDamage * Time.deltaTime;
            if(Time.time - DOTStart > DOTDuration)
            {
                DOTActive = false;
            }
        }
        if (FlashBack & Time.time - LastTime > 0.05f)
        {
            if (GetComponent<MeshRenderer>())
            {
                GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
                FlashBack = false;
                LastTime = 0f;
            }
            else if (GetComponentInChildren<SkinnedMeshRenderer>())
            {
                GetComponentInChildren<SkinnedMeshRenderer>().materials[0].SetFloat("_Emission", 0f);
                FlashBack = true;
                LastTime = Time.time;
            }
        }

    }

    public void DecreaseHealth(float _value)
    {
        isHit = true;
        DamageFlash();
        CurrentHealth -= _value;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (gameObject.tag.Equals("Player"))
        {
            OnPlayerHealthUpdate();
			Camera.main.GetComponent<Animator> ().SetTrigger ("ChromaBurst");
        }
        //Feedback
        if(HitVFX)//VFX
        {
            Instantiate(HitVFX, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        if(HitSound != null)//Sound
        {
            float Pitch = Random.Range(HitSoundPitchRange.x, HitSoundPitchRange.y);
            if (HitSound.Length == 1)
            {
                if (!StackSounds && LastSound == null)
                {
                    LastSound = MusicManager.PlaySoundAtLocation(HitSound[0], transform.position, Pitch, HitSoundVol);
                }
                else if(StackSounds)
                {
                    MusicManager.PlaySoundAtLocation(HitSound[0], transform.position, Pitch, HitSoundVol);
                }

            }
            else if(HitSound.Length > 1)
            {
                int RandomInt = Random.Range(0, HitSound.Length - 1);
                if (!StackSounds && LastSound == null)
                {                  
                    LastSound = MusicManager.PlaySoundAtLocation(HitSound[RandomInt], transform.position, Pitch, HitSoundVol);
                }
                else if(StackSounds)
                {                    
                    MusicManager.PlaySoundAtLocation(HitSound[RandomInt], transform.position, Pitch, HitSoundVol);
                }
            }
        }
        if (HitAnimationOn)
        {
            if (GetComponent<Animator>())//Animation
            {
                GetComponent<Animator>().SetTrigger("Hit" + Random.Range(1, 6));

            }
        }
        if(OnHitShake)//Shake
        {
            transform.DOComplete();
            transform.DOShakePosition(0.1f, 0.1f, 1);
        }
    }
    
    public void IncreaseHealth(float _value)
    {
        CurrentHealth += _value;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        if (gameObject.tag.Equals("Player"))
        {
            OnPlayerHealthUpdate();
        }
    }

    public void Revive()
    {
        CurrentHealth = MaxHealth;
    }

    //Deals the given damage spread over the time given
    public void DecreaseHealthOverTime(float _totalDamage, float _time)
    {
        DOTActive = true;
        DOTDamage = _totalDamage / _time;
        DOTDuration = _time;
    }

    void DamageFlash()
    {
        if (GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
            FlashBack = true;
            LastTime = Time.time;
        }
        else if(GetComponentInChildren<SkinnedMeshRenderer>())
        {
            GetComponentInChildren<SkinnedMeshRenderer>().materials[0].SetFloat("_Emission", 1f);
            //GetComponentInChildren<SkinnedMeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
            //print(GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name);
            FlashBack = true;
            LastTime = Time.time;
        }
    }

    public bool GetStatusEffect(StatusEffects _Effect)
    {
        switch(_Effect)
        {
            case StatusEffects.CHAINLIGHTING:
                return ChainLighting;
            case StatusEffects.STUNNED:
                return Stunned;
            default:
                return false;
        }
    }

    public void SetStatusEffect(StatusEffects _Effect, bool _NewValue)
    {
        switch (_Effect)
        {
            case StatusEffects.CHAINLIGHTING:
                ChainLighting = _NewValue;
                break;
            case StatusEffects.STUNNED:
                Stunned = _NewValue;
                break;
            default:
                break;
        }
    }
}
