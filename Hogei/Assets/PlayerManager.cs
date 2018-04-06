﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject GameoverScreen;
    public GameObject Player;

    public static PlayerManager Singleton;

    private List<Weapon> WeaponInventory;
    private List<SoupUpgrade> SoupInventory;
    public int[] IngredientInventory;
    //The two weapons the player has equiped for a dungeon
    private Weapon PrimaryWeapon;
    private Weapon SecondaryWeapon;
    //The two upgrades applied to those weapons(Might just have one universal upgrade?)
    private SoupUpgrade PrimarySoup;
    private SoupUpgrade SecondarySoup;


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        if(!Singleton)
        {
            Singleton = this;
        }
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        IngredientInventory = new int[SoupIngredient.GetIngredientTypeCount()];

    }

    void OnEnable()
    {
        EntityHealth.OnPlayerHealthUpdate += CheckGameOver;
    }

    void OnDisable()
    {
        EntityHealth.OnPlayerHealthUpdate -= CheckGameOver;
    }

    void CheckGameOver()
    {
        if(Player.GetComponent<EntityHealth>().CurrentHealth <= 0)
        {
            GameoverScreen.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Getters and Setters
    public Weapon GetPrimary() { return PrimaryWeapon; }
    public void SetPrimary(Weapon _NewWeapon) { PrimaryWeapon = _NewWeapon; }

    public Weapon GetSecondary() { return SecondaryWeapon; }
    public void SetSecondary(Weapon _NewWeapon) { SecondaryWeapon = _NewWeapon; }

    public int[] GetIngredientInventory() { return IngredientInventory; }
    public void AddIngredientInventory(SoupIngredient _NewObject) { IngredientInventory[(int)_NewObject.Type] += 1; }
    public void RemoveIngredientInventory(SoupIngredient _ToRemove) { IngredientInventory[(int)_ToRemove.Type] -= 1; }

    public List<SoupUpgrade> GetSoupInventory() { return SoupInventory; }
    public void AddSoupInventory(SoupUpgrade _NewUpgrade) { SoupInventory.Add(_NewUpgrade); }
    public void RemoveSoupInventory(SoupUpgrade _ToRemove) { SoupInventory.Remove(_ToRemove); }

    public List<Weapon> GetWeaponInventory() { return WeaponInventory; }
    public void AddWeaponInventory(Weapon _NewWeapon) { WeaponInventory.Add(_NewWeapon); }
    public void RemoveWeaponInventory(Weapon _ToRemove) { WeaponInventory.Remove(_ToRemove); }

    public static PlayerManager GetInstance()
    {
        if(!Singleton)
        {
            GameObject Instance = new GameObject();
            Instance.AddComponent<PlayerManager>();
            Singleton = Instance.GetComponent<PlayerManager>();
        }
        return Singleton;
    }
}
