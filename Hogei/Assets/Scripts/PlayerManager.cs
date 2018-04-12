using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public HealthBarNotched HealthBar = null;
    public GameObject GameoverScreen = null;
    public GameObject Player = null;

    public static PlayerManager Singleton;

    private List<Weapon> WeaponInventory;
    public List<SoupUpgrade> SoupInventory;
    public int[] IngredientInventory;
    //The two weapons the player has equiped for a dungeon
    private Weapon PrimaryWeapon;
    private Weapon SecondaryWeapon;
    //The two upgrades applied to those weapons(Might just have one universal upgrade?)
    private SoupUpgrade PrimarySoup;
    private SoupUpgrade SecondarySoup;

    private bool SceneLoaded = false;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        if(!Singleton)
        {
            Singleton = this;
            Init();
        }
        else if(Singleton != this)
        {
            Debug.Log("Player Manager already exists destroying self " + gameObject.name);
            Destroy(gameObject);
        }
        if (Player)
        {
            PrimaryWeapon = Player.GetComponentInChildren<PlayerStreamShot>();
        }
    }

    void Init()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        IngredientInventory = new int[SoupIngredient.GetIngredientTypeCount()];
        if (SoupInventory == null) SoupInventory = new List<SoupUpgrade>();
        if (WeaponInventory == null) WeaponInventory = new List<Weapon>();
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
            GameoverScreen.SetActive(true);
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
        if (HealthBar)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                DontDestroyOnLoad(HealthBar.gameObject);
                HealthBar.gameObject.SetActive(false);
            }
            else
            {
                HealthBar.gameObject.SetActive(true);
            }
        }
        SceneLoaded = false;
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player)
        {
            //Player.SetActive(true);
            Player.transform.position = SceneHandler.GetSceneHandler().GetPlayerSpawnPoint().position;
            Player.GetComponent<EntityHealth>().Revive();
            if (Camera.main.GetComponentInParent<Follow>())
            {
                Camera.main.GetComponentInParent<Follow>().SetStopFollowing(false);
            }
            Debug.Log(Time.time + ": " + gameObject.name + " - Setting up player weapons...");
            Player.GetComponent<PlayerAttack>().SetupWeapons();
            if (SoupInventory.Count > 0)
            {
                Debug.Log(Time.time + ": " + gameObject.name + " - Applying weapon upgrades...");
                PrimarySoup = SoupInventory[0];
                ApplyUpgrades();
            }
    
        }
        if(GameoverScreen) GameoverScreen.SetActive(false);
        SceneLoaded = true;
    }

    private void ApplyUpgrades()
    {
        if(PrimaryWeapon && PrimarySoup)PrimaryWeapon.ApplyUpgrade(PrimarySoup);
    }

    //Getters and Setters
    public Weapon GetPrimary() { return PrimaryWeapon; }
    public void SetPrimary(Weapon _NewWeapon) { PrimaryWeapon = _NewWeapon; }

    public Weapon GetSecondary() { return SecondaryWeapon; }
    public void SetSecondary(Weapon _NewWeapon) { SecondaryWeapon = _NewWeapon; }

    public int[] GetIngredientInventory() { return IngredientInventory; }
    public void AddIngredientInventory(SoupIngredient _NewObject) { IngredientInventory[(int)_NewObject.Type] += 1; }
    public void RemoveIngredientInventory(SoupIngredient _ToRemove) { IngredientInventory[(int)_ToRemove.Type] -= 1; }
    public int GetIngredientAmount(SoupIngredient.IngredientType _Type) { return IngredientInventory[(int)_Type]; }

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
