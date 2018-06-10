using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public ProgressBar HealthBar = null;
    public GameObject GameoverScreen = null;
    public GameObject Player = null;

    public static PlayerManager Singleton;

    private List<Weapon> WeaponInventory;
    public List<SoupUpgrade> SoupInventory;
    public int[] IngredientInventory;

    [Header("Debug Settings")]
    public bool FullIngredInventory = false;
    //The two weapons the player has equiped for a dungeon
    private Weapon.WeaponTypes PrimaryWeapon = Weapon.WeaponTypes.None;
    private Weapon.WeaponTypes SecondaryWeapon = Weapon.WeaponTypes.None;
    //The two upgrades applied to those weapons(Might just have one universal upgrade?)
    private SoupUpgrade PrimarySoup;
    private SoupUpgrade SecondarySoup;
    //Progression Variables
    private Dictionary<Weapon.WeaponTypes, bool> WeaponUnlocks;
    public List<int> LevelsCompleted;

    private bool SceneLoaded = false;
    private bool SetupDone = false;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        if(!Singleton)
        {
            Singleton = this;
        }
        else if(Singleton != this)
        {
            Debug.Log("Player Manager already exists destroying self " + gameObject.name);
            Destroy(gameObject);
        }

    }

    void Init()
    {
        if (SceneLoaded) return;
        Debug.Log(Time.time + ": " + gameObject.name + " - Scene Setup...");
        Debug.Log(Time.time + ": " + gameObject.name + " - Setting up progression...");
        //Setup Player Progression
        if (WeaponUnlocks == null) WeaponUnlocks = new Dictionary<Weapon.WeaponTypes, bool>();
        if (LevelsCompleted == null) LevelsCompleted = new List<int>();
        Debug.Log(Time.time + ": " + gameObject.name + " - Setting up player...");
        //Setup Player Character
        if (!Player) Player = GameObject.FindGameObjectWithTag("Player");
        if (Player)
        {
            DontDestroyOnLoad(Player);
            //Get the players spawn
            Debug.Log(Time.time + ": " + gameObject.name + " - Getting player spawn...");
            Player.transform.position = SceneHandler.GetSceneHandler().GetPlayerSpawnPoint().position;
            //Revive the player
            Debug.Log(Time.time + ": " + gameObject.name + " - Reviving player...");
            Player.GetComponent<EntityHealth>().Revive();
            //Set the camera to follow the player
            Debug.Log(Time.time + ": " + gameObject.name + " - Setting camera to follow player...");
            if (Camera.main.GetComponentInParent<Follow>())
            {
                Camera.main.GetComponentInParent<Follow>().SetStopFollowing(false);
            }
            //Setup Soup Upgrades
            if (SoupInventory != null && SoupInventory.Count > 0)
            {
                Debug.Log(Time.time + ": " + gameObject.name + " - Selecting primary soup...");
                if (PrimarySoup == null) PrimarySoup = SoupInventory[0];
            }
            //Setup the player's movement
            Debug.Log(Time.time + ": " + gameObject.name + " - Setting up player movement...");
            Player.GetComponent<PlayerController>().MovementAlignment = SceneHandler.GetSceneHandler().transform;
            //Setup the player's weapons
            if (PrimaryWeapon == Weapon.WeaponTypes.None && SecondaryWeapon == Weapon.WeaponTypes.None)
            {
                Debug.Log(Time.time + ": " + gameObject.name + " - No weapons selected; setting default weapons (Stream + Shotgun)...");
                Player.GetComponent<PlayerAttack>().SetupWeapons(Weapon.WeaponTypes.Stream, Weapon.WeaponTypes.Bloom, PrimarySoup, SecondarySoup);
                PrimaryWeapon = Weapon.WeaponTypes.Stream;
                SecondaryWeapon = Weapon.WeaponTypes.Bloom;
            }
            else
            {
                Debug.Log(Time.time + ": " + gameObject.name + " - Setting up player weapons...");
                Player.GetComponent<PlayerAttack>().SetupWeapons(PrimaryWeapon, SecondaryWeapon, PrimarySoup, SecondarySoup);
            }
            //Change WhatCanIDo script
            Debug.Log(Time.time + ": " + gameObject.name + " - Configuring WhatCanIDo script...");
            WhatCanIDO PlayerPermissions = Player.GetComponent<WhatCanIDO>();
            if (SceneManager.GetActiveScene().buildIndex == 1) //If its the map scene
            {
                Player.GetComponent<PlayerAttack>().ClearWeapons();               
                PlayerPermissions.canShoot = false;
                PlayerPermissions.canMove = false;
                PlayerPermissions.canAbility = false;
            }
            else
            {
                PlayerPermissions.canShoot = true;
                PlayerPermissions.canMove = true;
                PlayerPermissions.canAbility = true;
            }
        }
        Debug.Log(Time.time + ": " + gameObject.name + " - Setting up Healthbar...");
        //Setup HealthBar
        if (!HealthBar)
        {
            if (GameObject.Find("HealthBar")) HealthBar = GameObject.Find("HealthBar").GetComponent<ProgressBar>();
            else Debug.LogWarning("NO HEALTH BAR UI IN SCENE");
        }
        if (HealthBar)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                DontDestroyOnLoad(HealthBar.gameObject);
                HealthBar.DisableSprites();
            }
            else
            {
                HealthBar.EnableSprites();
                HealthBar.EntityHealth = Player.GetComponent<EntityHealth>();
            }
        }
        Debug.Log(Time.time + ": " + gameObject.name + " - Setting up gameover screen...");
        //Setup GameoverScreen
        if (!GameoverScreen) GameoverScreen = GameObject.Find("Gameover");
        if (GameoverScreen) GameoverScreen.SetActive(false);
        Debug.Log(Time.time + ": " + gameObject.name + " - Setting up player inventories...");
        //Setup Inventories
        if (IngredientInventory == null || IngredientInventory.Length <= 0) IngredientInventory = new int[SoupIngredient.GetIngredientTypeCount()];
        if(FullIngredInventory)
        {
            for(int i = 0; i < IngredientInventory.Length; ++i)
            {
                IngredientInventory[i] = 10;
            }
        }
        //Soup settings
        Debug.Log(Time.time + ": " + gameObject.name + " - Setting up soup inventories...");
        if (SoupInventory == null) SoupInventory = new List<SoupUpgrade>();
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SoupInventory.Clear();
            PrimarySoup = null;
            SecondarySoup = null;
        }
        if (WeaponInventory == null) WeaponInventory = new List<Weapon>();
        //Setup for cursor
        //if (SceneManager.GetActiveScene().buildIndex == 1) Cursor.visible = true; //If its the map scene show it
        //else Cursor.visible = false;
        Debug.Log(Time.time + ": " + gameObject.name + " - ...Setup Done");
    }

    void OnEnable()
    {
        EntityHealth.OnPlayerHealthUpdate += CheckGameOver;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnDisable()
    {
        EntityHealth.OnPlayerHealthUpdate -= CheckGameOver;
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void CheckGameOver()
    {
        if(SceneLoaded && Player.GetComponent<EntityHealth>().CurrentHealth <= 0)
        {
            if (GameoverScreen)
            {
                GameoverScreen.SetActive(true);
            }
            else Debug.LogWarning("NO GAMEOVER SCREEN UI IN SCENE");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
	}

    private void OnSceneLoad(Scene _Scene, LoadSceneMode _Mode)
    {
        SceneLoaded = false;
        Init();
        SceneLoaded = true;
    }

    //Getters and Setters
    //Weapons
    public Weapon.WeaponTypes GetPrimary() { return PrimaryWeapon; }
    public void SetPrimary(Weapon.WeaponTypes _NewWeapon) { PrimaryWeapon = _NewWeapon; }

    public Weapon.WeaponTypes GetSecondary() { return SecondaryWeapon; }
    public void SetSecondary(Weapon.WeaponTypes _NewWeapon) { SecondaryWeapon = _NewWeapon; }
    //Soup
    public SoupUpgrade GetPrimarySoup() { return PrimarySoup; }
    public void SetPrimarySoup(SoupUpgrade _NewSoup) { PrimarySoup = _NewSoup; }

    public SoupUpgrade GetSecondarySoup() { return SecondarySoup; }
    public void SetSecondarySoup(SoupUpgrade _NewSoup) { SecondarySoup = _NewSoup; }
    //Inventories
    //Ingredients
    public int[] GetIngredientInventory() { return IngredientInventory; }
    public void AddIngredientInventory(SoupIngredient _NewObject) { IngredientInventory[(int)_NewObject.Type] += 1; }
    public void RemoveIngredientInventory(SoupIngredient _ToRemove) { IngredientInventory[(int)_ToRemove.Type] -= 1; }
    public int GetIngredientAmount(SoupIngredient.IngredientType _Type) { return IngredientInventory[(int)_Type]; }
    //Soups
    public List<SoupUpgrade> GetSoupInventory() { return SoupInventory; }
    public void AddSoupInventory(SoupUpgrade _NewUpgrade) { SoupInventory.Add(_NewUpgrade); }
    public void RemoveSoupInventory(SoupUpgrade _ToRemove) { SoupInventory.Remove(_ToRemove); }
    //Weapons
    public List<Weapon> GetWeaponInventory() { return WeaponInventory; }
    public void AddWeaponInventory(Weapon _NewWeapon) { WeaponInventory.Add(_NewWeapon); }
    public void RemoveWeaponInventory(Weapon _ToRemove) { WeaponInventory.Remove(_ToRemove); }
    //Weapon Unlock Dictionary
    public Dictionary<Weapon.WeaponTypes, bool> GetWeaponUnlocks() { return WeaponUnlocks; }
    //Level Completed List
    public void AddLevelCompleted(int _LevelIndex) { LevelsCompleted.Add(_LevelIndex); }
    public List<int> GetLevelsCompleted() { return LevelsCompleted; }

    public static PlayerManager GetInstance()
    {
        if(!Singleton)
        {
            GameObject Instance = new GameObject();
            Instance.name = "PlayerManager";
            Instance.AddComponent<PlayerManager>();
            Singleton = Instance.GetComponent<PlayerManager>();
            Singleton.Init();
        }
        return Singleton;
    }

    public void LoadScene(int _Index)
    {
        SceneManager.LoadScene(_Index);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
