using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckyTankPhaseManager : BossHandler
{

    [Header("Phase 1 vars")]
    [Tooltip("Time between attacks")]
    public float timeBetweenAttacks = 5.0f;

    [Header("Script refs")]
    public DuckyTurret turretOne;
    public DuckyTurret turretTwo;
    public DuckyBeakCannon beak;

    //control vars
    private float lastSequenceTime = 0.0f; //time attack sequence was last executed

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > lastSequenceTime + timeBetweenAttacks)
        {
            AttackSequence();
        }
	}

    //attack sequence logic
    private void AttackSequence()
    {
        //set timing
        lastSequenceTime = Time.time;
        //fire turrets
        turretOne.FireTurrets();
        turretTwo.FireTurrets();
        beak.StartLaser();
    }
}
