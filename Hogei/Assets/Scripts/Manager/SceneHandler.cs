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
    public List<GameObject> enemiesInSceneCopyList = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> enemiesInSceneList = new List<GameObject>();

    private static SceneHandler singleton;

	// Use this for initialization
	void Start () {
        singleton = this;
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
            enemiesInSceneCopyList.Add(objectClone);
        }
    }

    //Ref all enemies in scene
    void RefEnemies()
    {
        //find all enemies in the scene
        GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag(enemyTag);
        //Create a copy of all objects and place into list
        for (int i = 0; i < enemiesInScene.Length; i++)
        {
            enemiesInSceneList.Add(enemiesInScene[i]);
        }
    }

    //Get list of enemies
    public List<GameObject> GetActiveList()
    {
        return enemiesInSceneList;
    }

    static public SceneHandler GetSceneHandler()
    {
        if (!singleton)
        {
            GameObject newObject = new GameObject();
            newObject.AddComponent<SceneHandler>();
            singleton = newObject.GetComponent<SceneHandler>();
        }
        return singleton;
    }
}
