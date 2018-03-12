using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject GameoverScreen;
    public GameObject Player;

	// Use this for initialization
	void Start () {
		if(!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
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
}
