using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : GameEntity {

    //[HideInInspector]
    public bool isActive = false; //check if active
    [HideInInspector]
    public InfinateSpawner SpawnerParent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    protected void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    public virtual void Activate() { isActive = true; }
    public virtual void Deactivate() { isActive = false; }

    //On death func
    public virtual void AmDead()
    {

    }

    protected virtual void OnPause()
    {

    }

    protected virtual void OnUnpause()
    {

    }
}
