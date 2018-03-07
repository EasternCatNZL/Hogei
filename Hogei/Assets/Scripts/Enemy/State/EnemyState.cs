using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    [Header("States")]
    public bool isSlowed = false;

    [Header("Active")]
    [Tooltip("Check if enemy is active")]
    public bool isActive = false;
    public bool isPaused = false;

    [Header("Modifiers")]
    [Tooltip("Slow modifier")]
    public float slowModifier = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        PauseHandler.PauseEvent += OnPause;
        PauseHandler.UnpauseEvent += OnUnpause;
        //print("Subscribed to event");
    }

    private void OnDisable()
    {
        PauseHandler.PauseEvent -= OnPause;
        PauseHandler.UnpauseEvent -= OnUnpause;
        //print("Unsubscribed to event");
    }

    //get set methods
    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive(bool active)
    {
        isActive = active;
    }

    //Pause events
    void OnPause()
    {
        isPaused = true;
    }

    private void OnUnpause()
    {
        isPaused = false;
    }
}
