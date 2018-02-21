using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour {

    public static StatisticsManager Singleton;

    bool StatisticsVisible = false;
    public int EnemyDeathCount = 0;
    public int BulletShotCount = 0;
    
    public static StatisticsManager GetStatsManager()
    {
        if (Singleton == null)
        {
            GameObject StatsMgr = new GameObject();
            StatsMgr.name = "StatisticsManager";
            DontDestroyOnLoad(StatsMgr);
            StatsMgr.AddComponent<StatisticsManager>();
            Singleton = StatsMgr.GetComponent<StatisticsManager>();           
        }
        return Singleton;
    }

	// Use this for initialization
	void Start () {
        if(Singleton == null)
        {
            DontDestroyOnLoad(this);
            Singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void OnEnable()
    {
        EntityHealth.OnDeath += IncreaseDeathCount;
        PlayerAttack.OnShoot += IncreaseShotCount;
    }

    void OnDisable()
    {
        EntityHealth.OnDeath -= IncreaseDeathCount;
        PlayerAttack.OnShoot -= IncreaseShotCount;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IncreaseDeathCount()
    {
        EnemyDeathCount += 1;
    }

    void IncreaseShotCount()
    {
        BulletShotCount += 1;
    }

    public int GetEnemyDeathCount()
    {
        return EnemyDeathCount;
    }
}
