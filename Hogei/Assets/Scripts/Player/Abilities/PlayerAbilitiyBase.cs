using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiyBase : MonoBehaviour {

    enum Ability
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

    [Header("Ability holder")]
    public GameObject abilityHolder;

    //enum
    private Ability ability;

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
            ability = Ability.CLEARER;
        }
        else
        {
            Debug.LogError("canDo can not be assigned. WhatCanIDO script not present on " + name);
        }
        bulletClear = abilityHolder.GetComponent<BulletClearer>();
        //deflect = abilityHolder.GetComponent<DeflectShield>();
        //reverse = abilityHolder.GetComponent<ReversalShot>();
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
        //check if input
        if (Input.GetKeyDown(abilityOneKey))
        {
            //try to use current ability
            switch (ability)
            {
                case Ability.CLEARER:
                    bulletClear.UseAbility();
                    break;
                //case Ability.DEFLECTOR:
                //    deflect.UseAbility();
                //    break;
                //case Ability.REVERSAL:
                //    reverse.UseAbility();
                //    break;
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
