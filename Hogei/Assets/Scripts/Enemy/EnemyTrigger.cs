using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

    [Header("Tags")]
    public string targetTag = "Player";
    public string bulletTag = "Bullet";

    [Header("Trigger")]
    public bool isTriggered = false; //checks to see if trigger has been triggered
    public bool doTriggerPlayer = true; //checks to see if player should trigger
    public bool doTriggerBullet = false; //checks to see if bullet should trigger

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
