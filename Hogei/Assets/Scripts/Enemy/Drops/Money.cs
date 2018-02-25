using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    GameObject Player;

    public int MinMoney = 0;
    public int MaxMoney = 5;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Player.GetComponent<Inventory>().InreaseMoney(Random.Range(MinMoney, MaxMoney));
            Destroy(gameObject);
        }
    }
}
