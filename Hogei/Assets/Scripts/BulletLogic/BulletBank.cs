﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBank : MonoBehaviour {

    private Queue<GameObject> regularBulletQueue = new Queue<GameObject>();
    private Queue<GameObject> setupStraightBulletQueue = new Queue<GameObject>();
    private Queue<GameObject> playerStraightBulletQueue = new Queue<GameObject>();

    [Header("Bullet Objects")]
    [Tooltip("Prefab of regular straight bullet")]
    public GameObject regularStraightBulletObject;
    [Tooltip("Prefab of setup straight bullet")]
    public GameObject setupStraightBulletObject;
    [Tooltip("Prefab of player straight bullet")]
    public GameObject playerStraightBullet;

    [Header("Number of bullets")]
    [Tooltip("Num of Regular Straight Bullets")]
    public int numRegularStraightBullets = 2000;
    [Tooltip("Num of Setup Straight Bullets")]
    public int numSetupStraightBullets = 2000;

    [Tooltip("Number of player straight bullets")]
    public int numPlayerStraightBullets = 2000;

	// Use this for initialization
	void Start () {
		//fill queues
        for (int i = 0; i < numRegularStraightBullets; i++)
        {
            GameObject newBullet = Instantiate(regularStraightBulletObject, transform.position, transform.rotation);
            newBullet.GetComponent<RegularStraightBullet>().SetBulletBank(this);
            newBullet.transform.SetParent(gameObject.transform);
            newBullet.SetActive(false);
            regularBulletQueue.Enqueue(newBullet);
        }

        for (int i = 0; i < numSetupStraightBullets; i++)
        {
            GameObject newBullet = Instantiate(setupStraightBulletObject, transform.position, transform.rotation);
            newBullet.GetComponent<SetupStraightBullet>().SetBulletBank(this);
            newBullet.transform.SetParent(gameObject.transform);
            newBullet.SetActive(false);
            setupStraightBulletQueue.Enqueue(newBullet);
        }

        for (int i = 0; i < numPlayerStraightBullets; i++)
        {
            GameObject newBullet = Instantiate(playerStraightBullet, transform.position, transform.rotation);
            newBullet.GetComponent<PlayerStraightBullet>().SetBulletBank(this);
            newBullet.transform.SetParent(gameObject.transform);
            newBullet.SetActive(false);
            playerStraightBulletQueue.Enqueue(newBullet);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //get bullet bullet from queue
    public GameObject GetRegularStraightBullet()
    {
        GameObject bullet = regularBulletQueue.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public GameObject GetSetupStraightBullet()
    {
        GameObject bullet = setupStraightBulletQueue.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public GameObject GetPlayerStraightBullet()
    {
        GameObject bullet = playerStraightBulletQueue.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    //return bullet to queue
    public void ReturnRegularStraightBullet(GameObject regularBullet)
    {
        regularBullet.SetActive(false);
        regularBulletQueue.Enqueue(regularBullet);
    }

    public void ReturnSetupStraightBullet(GameObject setupBullet)
    {
        setupBullet.SetActive(false);
        setupStraightBulletQueue.Enqueue(setupBullet);
    }

    public void ReturnPlayerStraightBullet(GameObject playerBullet)
    {
        playerBullet.SetActive(false);
        playerStraightBulletQueue.Enqueue(playerBullet);
    }
}
