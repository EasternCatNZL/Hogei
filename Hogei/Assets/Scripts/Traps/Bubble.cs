using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    private bool PlayerTrapped = false;
    public float MoveSpeed = 0.0f;
    public float DeletionRange = 0.0f;
    public float TrapDuration = 0.0f;
    private float timer;

    private WhatCanIDO Player;

	// Use this for initialization
	void Start () {
        timer = TrapDuration;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<WhatCanIDO>();        
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.up * (Time.deltaTime * MoveSpeed), Space.World);

        if (PlayerTrapped)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                PlayerTrapped = false;
                Player.canAbility = true;
                Player.canMove = true;
                Player.canShoot = true;                
            }
        }
	}

    void Bubbled()
    {
        timer = TrapDuration;
        PlayerTrapped = true;
        Player.canAbility = false;
        Player.canMove = false;
        Player.canShoot = false;
    }

    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag.Equals("Player"))
        {
            Bubbled();
        }
    }
}