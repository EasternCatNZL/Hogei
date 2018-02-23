using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour {

    [Header("Scene number")]
    public int sceneNumber = 1;

    [Header("Tags")]
    public string enemyTag = "Enemy";

    //all enemies in scene
    [HideInInspector]
    public List<GameObject> enemiesInSceneList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Gets all enemies in scene and create a copy
    void CopyEnemies()
    {
        //find all enemies in the scene
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag(enemyTag);
        //Create a copy of all objects and place into list
        for (int i = 0; i < enemiesInScene.Length; i++)
        {
            GameObject objectClone = enemiesInScene[i];
            enemiesInSceneList.Add(objectClone);
        }
    }
}
