﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiyBase : MonoBehaviour {

    public enum Ability
    {
        CLEARER,
        DEFLECTOR,
        REVERSAL,
        DASH
    }

    [Header("Inputs")]
    [Tooltip("Slow ability key")]
    public KeyCode abilityOneKey = KeyCode.F;
    [Tooltip("Shield ablity key")]
    public KeyCode abilityTwoKey = KeyCode.G;
    [Tooltip("Ability 1 axis")]
    public string abitlityOneAxis = "Skill1";
    [Tooltip("Ability 2 axis")]
    public string abilityTwoAxis = "Skill2";
    [Tooltip("Ability 3 axis")]
    public string abitlityThreeAxis = "Skill3";

    [Header("Ability holder")]
    public GameObject abilityHolder;

    //enum
    public Ability ability;
    public Ability abilityOne;
    public Ability abilityTwo;
    public Ability abilityThree;

    //script refs
    private WhatCanIDO canDo;
    private BulletClearer bulletClear;
    private DeflectShield deflect;
    private ReversalShot reverse;
    private PlayerDash dash;
    

    // Use this for initialization
    void Start () {
        if (GetComponent<WhatCanIDO>())
        {
            canDo = GetComponent<WhatCanIDO>();
            //testing
            //ability = Ability.DASH;
        }
        else
        {
            Debug.LogError("canDo can not be assigned. WhatCanIDO script not present on " + name);
        }
        bulletClear = abilityHolder.GetComponent<BulletClearer>();
        deflect = abilityHolder.GetComponent<DeflectShield>();
        reverse = abilityHolder.GetComponent<ReversalShot>();
        dash = abilityHolder.GetComponent<PlayerDash>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canDo.canAbility)
        {
            UseAbility();
        }
	}

    //ability use logic
    private void UseAbility()
    {
        UseAbilityOne();
        UseAbilityTwo();
        UseAbilityThree();
    }

    private void UseAbilityOne()
    {
        //check if input
        if (Input.GetAxisRaw(abitlityOneAxis) != 0)
        {
            //try to use current ability
            switch (abilityOne)
            {
                case Ability.CLEARER:
                    bulletClear.UseAbility();
                    break;
                case Ability.DEFLECTOR:
                    deflect.UseAbility();
                    break;
                case Ability.REVERSAL:
                    reverse.UseAbility();
                    break;
                case Ability.DASH:
                    dash.Use();
                    break;
            }
        }
    }

    private void UseAbilityTwo()
    {
        //check if input
        if (Input.GetAxisRaw(abilityTwoAxis) != 0)
        {
            //try to use current ability
            switch (abilityTwo)
            {
                case Ability.CLEARER:
                    bulletClear.UseAbility();
                    break;
                case Ability.DEFLECTOR:
                    deflect.UseAbility();
                    break;
                case Ability.REVERSAL:
                    reverse.UseAbility();
                    break;
                case Ability.DASH:
                    dash.Use();
                    break;
            }
        }
    }

    private void UseAbilityThree()
    {
        //check if input
        if (Input.GetAxisRaw(abitlityThreeAxis) != 0)
        {
            //try to use current ability
            switch (abilityThree)
            {
                case Ability.CLEARER:
                    bulletClear.UseAbility();
                    break;
                case Ability.DEFLECTOR:
                    deflect.UseAbility();
                    break;
                case Ability.REVERSAL:
                    reverse.UseAbility();
                    break;
                case Ability.DASH:
                    dash.Use();
                    break;
            }
        }
    }

    //change ability logic <- testing only
    private void ChangeAbility()
    {
        //check if input
        if (Input.GetKeyDown(abilityTwoKey))
        {

        }
    }
}
