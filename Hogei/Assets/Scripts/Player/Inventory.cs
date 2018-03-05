using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int Money = 0;
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InreaseMoney(int _value)
    {
        Money += _value;
    }

    void DecreaseMoney(int _value)
    {
        Money -= _value;
        if(Money < 0)
        {
            Money = 0;
        }
    }

    public void Purchase(int _value)
    {
        if(Money > _value)
        {
            DecreaseMoney(_value);
        }
    }

}
