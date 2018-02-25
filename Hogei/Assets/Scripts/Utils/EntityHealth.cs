﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour {
    
    public delegate void DeathEvent();
    public static event DeathEvent OnDeath;

    public delegate void PlayerHealthEvent();
    public static event PlayerHealthEvent OnPlayerHealthUpdate;
    
    bool InvincibilityFrame = false; 

    public float CurrentHealth;
    [Tooltip("Maximum health the entity can have")]
    public float MaxHealth = 10;

    //[Header("Audio")]
    //public AudioSource deathSound;

    bool DOTActive;
    float DOTDamage;
    float DOTDuration;
    float DOTStart;

	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
        //deathSound.playOnAwake = false;

    }
	
	// Update is called once per frame
	void Update () {
        if(CurrentHealth <= 0.0f)
        {
            if (gameObject.tag == "Enemy")
            {
                if (OnDeath != null) OnDeath();
                Destroy(gameObject);
                //for room enemies
                if (transform.parent.GetComponent<RoomEnemyManager>())
                {
                    transform.parent.GetComponent<RoomEnemyManager>().enemyList.Remove(gameObject);
                }
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
	}

    public void DecreaseHealth(float _value)
    {
        CurrentHealth -= _value;
        if (CurrentHealth < 0) CurrentHealth = 0;
        if (transform.tag.Equals("Player"))
        {
            OnPlayerHealthUpdate();
        }
    }
    
    public void IncreaseHealth(float _value)
    {
        CurrentHealth += _value;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        if (transform.tag.Equals("Player"))
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
}
