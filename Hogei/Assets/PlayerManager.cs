using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject GameoverScreen;
    public GameObject Player;

    public static PlayerManager Singleton;

    public List<GameObject> Inventory;

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
        else
        {
            Debug.LogError(gameObject.name + ": The Player doesn't exist");
        }
        Inventory = new List<GameObject>();

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

    public void AddInventory(GameObject _NewObject)
    {
        Inventory.Add(_NewObject);
    }

    public void RemoveInventory(GameObject _ToRemove)
    {
        Inventory.Remove(_ToRemove);
    }

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
