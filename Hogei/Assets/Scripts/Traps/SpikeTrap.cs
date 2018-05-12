using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikeTrap : GameEntity {

    private float timer = 0.0f;
    private float ResetTimer = 0.0f;
    public float SpikeDelay = 1.0f;
    public float SpikeResetTimer = 2.0f;
    private bool isTriggered = false;
    private bool NeedsReset = false;

    //spike controls
    public float SpikeHieght = 1.0f;
    public float SpikeSpeed = 1.0f;
    private float SpikeDecrease;
    Tween SpikeDeployment;


    public GameObject Spike;

    // Use this for initialization
    void Start () {
        SpikeDecrease = SpikeHieght * -2.0f;
    }

    // Update is called once per frame
    void Update() {

        if (isTriggered == true)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f)
            {
                SpikeDeployment = Spike.transform.DOMoveY(SpikeHieght, SpikeSpeed, false);
                isTriggered = false;
                ResetTimer = SpikeResetTimer;
                NeedsReset = true;
            }
        }
        if(NeedsReset == true)
        {
            ResetTimer -= Time.deltaTime;
            if(ResetTimer <= 0.0f)
            {
                Spike.transform.DOMoveY(SpikeDecrease, SpikeSpeed, false);
            }
        }
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isTriggered = true;
            timer = SpikeDelay;
        }
    }
}
