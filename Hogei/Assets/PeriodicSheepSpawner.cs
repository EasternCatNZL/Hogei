using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicSheepSpawner : MonoBehaviour {

    public float Delay = 30;
    public GameObject SheepPrefab;
    private float StartTime;

    void Start()
    {
        StartTime = Time.time;
    }
	// Update is called once per frame
	void Update () {
		if(Time.time - StartTime > Delay)
        {
            SpawnSheep();
        }
	}

    void SpawnSheep()
    {
        GameObject Sheep = Instantiate(SheepPrefab, transform.position, transform.rotation);
        Sheep.GetComponent<SheepBehaviour>().isActive = true;
        Sheep.GetComponent<Animator>().SetTrigger("ChargeUp");
        Sheep.GetComponent<Animator>().SetTrigger("Charge");
        StartTime = Time.time;
    }
}
