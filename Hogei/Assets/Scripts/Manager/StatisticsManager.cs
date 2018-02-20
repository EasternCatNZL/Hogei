using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour {

    int EnemyDeathCount = 0;
    public static StatisticsManager Singleton;

	// Use this for initialization
	void Start () {
		if (Singleton == null)
        {
            Singleton = this;
        }
	}

    void OnEnable()
    {
        EntityHealth.OnDeath += IncreaseDeathCount;
    }

    void OnDisable()
    {
        EntityHealth.OnDeath -= IncreaseDeathCount;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IncreaseDeathCount()
    {
        EnemyDeathCount += 1;
    }
}
