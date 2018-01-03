using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiyBase : MonoBehaviour {

    enum Ability
    {
        CLEARER
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
    

    // Use this for initialization
    void Start () {
        if (GetComponent<WhatCanIDO>())
        {
            canDo = GetComponent<WhatCanIDO>();
        }
        else
        {
            Debug.LogError("canDo can not be assigned. WhatCanIDO script not present on " + name);
        }
        bulletClear = abilityHolder.GetComponent<BulletClearer>();
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
            }
        }
    }
}
