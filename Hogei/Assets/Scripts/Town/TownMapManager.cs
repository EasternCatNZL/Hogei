using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TownMapManager : MonoBehaviour {

    private bool StatsVisible = false;
    //UI Elements
    public GameObject EnemyDeathsUI;
    public GameObject BulletShotsUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(int _LevelIndex)
    {
        SceneManager.LoadScene(_LevelIndex);
    }

    public void ToggleStats()
    {
        if (!StatsVisible)
        {
            EnemyDeathsUI.GetComponent<Text>().text = "Enemy Death's " + StatisticsManager.GetStatsManager().GetEnemyDeathCount();
            EnemyDeathsUI.SetActive(true);
        }
        else if(StatsVisible)
        {
            EnemyDeathsUI.SetActive(false);
        }
    }
}
