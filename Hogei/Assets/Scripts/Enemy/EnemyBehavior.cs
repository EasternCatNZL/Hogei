using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    [HideInInspector]
    public bool isActive = false; //check if active

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Activate() { isActive = true; }
    public virtual void Deactivate() { isActive = false; }

    //On death func
    public virtual void AmDead()
    {

    }
}
