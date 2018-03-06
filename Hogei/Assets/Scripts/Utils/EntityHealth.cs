using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour {
    
    public delegate void DeathEvent();
    public static event DeathEvent OnDeath;

    public delegate void PlayerHealthEvent();
    public static event PlayerHealthEvent OnPlayerHealthUpdate;

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

    public float CurrentHealth;
    [Tooltip("Maximum health the entity can have")]
    public float MaxHealth = 10;

    public GameObject DeathVFX;
    public GameObject HitVFX;

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
	
	// Update is called once per frame
	void Update () {
        if(CurrentHealth <= 0.0f)
        {          
            if (gameObject.tag == "Enemy")
            {
                if (OnDeath != null) OnDeath();
                if(GetComponent<Drops>())GetComponent<Drops>().OnDeathDrop();
                //for room enemies
                if (transform.parent.GetComponent<RoomEnemyManager>())
                {
                    transform.parent.GetComponent<RoomEnemyManager>().enemyList.Remove(gameObject);
                }
            }
            if (DeathVFX)
            {
                Instantiate(DeathVFX, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            }
            Destroy(gameObject);
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
                GetComponentInChildren<SkinnedMeshRenderer>().materials[0].SetColor("_EmissionColor", Color.black);
                FlashBack = true;
                LastTime = Time.time;
            }
        }

    }

    public void DecreaseHealth(float _value)
    {
        DamageFlash();
        CurrentHealth -= _value;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (gameObject.tag.Equals("Player"))
        {
            OnPlayerHealthUpdate();
        }
        if(HitVFX)
        {
            Instantiate(HitVFX, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        if(GetComponent<Animator>())
        {
            GetComponent<Animator>().SetTrigger("Hit"+ Random.Range(1,6));
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
            GetComponentInChildren<SkinnedMeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
            print(GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name);
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
