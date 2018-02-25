﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarNotched : MonoBehaviour {

    public EntityHealth TargetHealth;
    public GameObject Notch;
    public float NotchPadding = 1.2f;

    public GameObject[] Notches;
    private int NumNotches;
   

    // Use this for initialization
    void Start () {
        CreateNotches();
	}

    void OnEnable()
    {
        EntityHealth.OnPlayerHealthUpdate += UpdateNotches;
    }

    void OnDisable()
    {
        EntityHealth.OnPlayerHealthUpdate -= UpdateNotches;
    }

    private void CreateNotches()
    {      
        NumNotches = (int)TargetHealth.MaxHealth;
        Notches = new GameObject[NumNotches];
        float xPos = 0f;
        Color NotchColor = Color.red;
        for(int i = 0; i < NumNotches; ++i)
        {
            GameObject temp = Instantiate(Notch, this.transform);
            temp.transform.localPosition = new Vector3(-xPos, 0f, 0f);
            temp.GetComponent<SpriteRenderer>().color = NotchColor;
            Notches[i] = temp;
            xPos += NotchPadding;
            if(i + 1> (NumNotches/3) * 2)
            {
                NotchColor = Color.yellow;
            }
            else if(i + 1> NumNotches/3 )
            {
                NotchColor = new Color(255f/255f, 128f/255f, 0f);
            }
        }
    }

    private void UpdateNotches()
    {
        if ((int)TargetHealth.MaxHealth > NumNotches)
        {
            CreateNotches();
        }
        int CurrentHealth = (int)TargetHealth.CurrentHealth;
        print(CurrentHealth);
        for (int i = 0; i < NumNotches; ++i)
        {
            if(i < CurrentHealth)
            {
                Notches[i].SetActive(true);
            }
            else
            {
                Notches[i].SetActive(false);
            }
            
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}