using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    [Header("Player Settings")]
    public Transform PlayerSpawnPoint;
    [Header("Scene number")]
    public int sceneNumber = 1;

    [Header("Tags")]
    public string enemyTag = "Enemy";

    //all enemies in scene
    //[HideInInspector]
    public List<GameObject> enemiesInSceneCopyList = new List<GameObject>();
    //[HideInInspector]
    public List<GameObject> enemiesInSceneList = new List<GameObject>();

    private static SceneHandler singleton;

    private int MapSceneIndex = 0;

    void Awake()
    {
        singleton = this;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

        RefEnemies();
    }

    private void OnEnable()
    {
        EntityHealth.OnDeath += RefEnemies;
    }

    private void OnDisable()
    {
        EntityHealth.OnDeath -= RefEnemies;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public Transform GetPlayerSpawnPoint() { return PlayerSpawnPoint; }

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
        enemiesInSceneList.Clear();
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

    //Reload this scene
    public void Reload()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene(sceneNumber);
    }

    //Load the map scene
    public void LoadMapScene()
    {
        SceneManager.LoadScene(MapSceneIndex);
    }

    public void LoadScene(int _SceneIndex)
    {
        SceneManager.LoadScene(_SceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
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
