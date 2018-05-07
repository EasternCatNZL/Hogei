using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrap : MonoBehaviour {

    private bool isActive = false;
    public float Timer = 0.0f;
    private float ResetTimer;

    public GameObject Bubble;

	// Use this for initialization
	void Start () {
        ResetTimer = Timer;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0.0f)
            {
                BubbleSpawn();               
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isActive = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isActive = false;
        }
    }

    void BubbleSpawn()
    {
        Timer = ResetTimer;
        Instantiate(Bubble, transform.position, transform.rotation);
    }
}
