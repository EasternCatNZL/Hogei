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

    public bool HasInvincibilityFrames = false;
    public float InvincibilityFrameDuration = 0.1f;
    public bool InvincibilityFrame = false;
    private float LastInvincibilityFrame = 0f;
    bool FlashBack = false;
    float LastTime = 0f;

    [Header("Health Settings")]
    public float CurrentHealth;
    public float MaxHealth = 10;
    [Header("VFX Settings")]
    public bool OnHitShake = false;
    public GameObject[] DeathVFX;
    public GameObject HitVFX;
    //public bool ModHitVFX = false;
    public Vector3 HitVFXScale;
    [Header("Animation Settings")]
    public bool HitAnimationOn = false;
    public bool ScaleUpBeforeDeath = false;
    [Header("Sound Settings")]
    public bool StackSounds = false;
    [Range(0f,1f)]
    public float HitSoundVol = 1f;
    public Vector2 HitSoundPitchRange = Vector2.zero;
    public AudioClip[] HitSound = null;
    public AudioClip FinalHitSound = null;
    private AudioSource LastSound;

    [Header("Function To Call When First Hit")]
    public UnityEvent HitTriggerFunction;

    [Header("Function To Call On Death")]
    public UnityEvent DeathFunction;

    private bool Dead = false;

    //[Header("Audio")]
    //public AudioSource deathSound;

    bool DOTActive = false;
    float DOTDamage = 0f;
    float DOTDuration = 0f;
    float DOTStart = 0f;

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

    //Update is called once per frame
    void Update () {
        if(InvincibilityFrame)
        {
            if(Time.time - LastInvincibilityFrame > InvincibilityFrameDuration)
            {
                InvincibilityFrame = false;
            }
        }
        //If the current health is zero or below(ie Entity is dead)
        if (!Dead && CurrentHealth <= 0.0f)
        {
            Dead = true;
            if (gameObject.tag == "Enemy")
            {
                if(GetComponent<Drops>())GetComponent<Drops>().OnDeathDrop();
                //for room enemies
                if (transform.parent && transform.parent.GetComponent<RoomEnemyManager>())
                {
                    transform.parent.GetComponent<RoomEnemyManager>().enemyList.Remove(gameObject);
                }
            }
            //Play death sound
            if(FinalHitSound)
            {
                MusicManager.AudioSourceSettings SoundSettings = new MusicManager.AudioSourceSettings();
                SoundSettings.Pitch = 1f;
                SoundSettings.SpatialBlend = 0f;
                SoundSettings.Volume = HitSoundVol;
                MusicManager.GetInstance().PlaySoundAtLocation(FinalHitSound, transform.position, SoundSettings);
            }
            //Instantiate Death VFX
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
            //Called DeathFuncton
            if(DeathFunction != null) DeathFunction.Invoke();
            //If the player has zero health then place them out of sight
            if (gameObject.CompareTag("Player"))
            {
                Camera.main.GetComponentInParent<Follow>().SetStopFollowing(true);
                gameObject.transform.position += new Vector3(0f, -1000f, 0f);
            }
            //Check if the entity should scale up before death
            else if(ScaleUpBeforeDeath)
            {
                transform.DOScale(1.5f, 0.1f);
                Destroy(gameObject, 0.1f);
            }
            else//Otherwise destroy the entity
            {
                Destroy(gameObject);
            }
        }
        //If there is a damage over time effect active 
		if(DOTActive)
        {
            CurrentHealth -= DOTDamage * Time.deltaTime;
            if(Time.time - DOTStart > DOTDuration)
            {
                DOTActive = false;
            }
        }
        //Set the emission value of the entities material back to zero
        if (FlashBack & Time.time - LastTime > 0.05f)
        {
            if (GetComponent<MeshRenderer>())
            {
                GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
                FlashBack = false;
                LastTime = 0f;
                GetComponent<MeshRenderer>().materials[0].SetFloat("_Emission", 0f);
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
        //Check if the entity is invicible
        if(InvincibilityFrame)
        {
            return;
        }
        //Call first hit function and set entity to isHit
        if (!isHit && HitTriggerFunction != null)
        {
            HitTriggerFunction.Invoke();
            isHit = true;
        }
        //Decrease the health of the entity        
        CurrentHealth -= _value;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (gameObject.tag.Equals("Player"))
        {
            if(OnPlayerHealthUpdate != null) OnPlayerHealthUpdate();
			Camera.main.GetComponent<Animator> ().SetTrigger ("ChromaBurst");
        }
        //Feedback
        DamageFlash();
        if (HitVFX)//VFX
        {
            Instantiate(HitVFX, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        //Play hit sound if it exists
        if(HitSound != null)
        {
            float Pitch = Random.Range(HitSoundPitchRange.x, HitSoundPitchRange.y);
            //Play the one sound
            if (HitSound.Length == 1)
            {
                if (!StackSounds && LastSound == null)
                {
                    LastSound = MusicManager.GetInstance().PlaySoundAtLocation(HitSound[0], transform.position, Pitch, HitSoundVol);
                }
                else if(StackSounds)
                {
                    MusicManager.GetInstance().PlaySoundAtLocation(HitSound[0], transform.position, Pitch, HitSoundVol);
                }

            }
            //Pick a random available sound
            else if(HitSound.Length > 1)
            {
                int RandomInt = Random.Range(0, HitSound.Length - 1);
                if (!StackSounds && LastSound == null)
                {                  
                    LastSound = MusicManager.GetInstance().PlaySoundAtLocation(HitSound[RandomInt], transform.position, Pitch, HitSoundVol);
                }
                else if(StackSounds)
                {
                    MusicManager.GetInstance().PlaySoundAtLocation(HitSound[RandomInt], transform.position, Pitch, HitSoundVol);
                }
            }
        }
        //Play hit animation if it exists
        if (HitAnimationOn)
        {
            if (GetComponent<Animator>())//Animation
            {
                GetComponent<Animator>().SetTrigger("Hit" + Random.Range(1, 6));

            }
        }
        //Play DOShake if wanted
        if(OnHitShake)
        {
            transform.DOComplete();
            transform.DOShakePosition(0.1f, 0.1f, 1);
        }
        if(HasInvincibilityFrames && !InvincibilityFrame)
        {
            SetInvincible(InvincibilityFrameDuration);
        }
    }
    
    public void IncreaseHealth(float _value)
    {
        //Increase health of the entity
        CurrentHealth += _value;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        if (gameObject.tag.Equals("Player")) 
        {
            //Called the player health update event
            OnPlayerHealthUpdate();
        }
    }

    public void Revive()
    {
        CurrentHealth = MaxHealth;
    }

    public void Kill()
    {
        CurrentHealth = 0f;
    }

    public void SetInvincible(float _Duration)
    {
        InvincibilityFrame = true;
        LastInvincibilityFrame = Time.time;
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
        //Set the emission value of the entities material to max
        if (GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
            FlashBack = true;
            LastTime = Time.time;
            GetComponent<MeshRenderer>().materials[0].SetFloat("_Emission", 1f);
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
