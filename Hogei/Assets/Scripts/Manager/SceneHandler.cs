using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneHandler : MonoBehaviour
{

    [Header("Player Settings")]
    public Transform PlayerSpawnPoint;
    [Header("Scene number")]
    public int sceneNumber = 1;

    [Header("Music Settings")]
    public AudioSource BackgroundMusic;

    [Header("UI Settigns")]
    public GameObject CountDownUI;
    public GameObject NotificationUI;

    [Header("Loading Screen Settings")]
    public GameObject LoadingScreenPrefab;

    [Header("Intro Settings")]
    [TextArea]
    public string IntroText = "";
    public float TimeShown = 0.8f;

    [Header("Tags")]
    public string enemyTag = "Enemy";

    //all enemies in scene
    //[HideInInspector]
    public List<GameObject> enemiesInSceneCopyList = new List<GameObject>();
    //[HideInInspector]
    public List<GameObject> enemiesInSceneList = new List<GameObject>();

    private int MapSceneIndex = 1;
    private LoadingScreenManager LoadingScreenRef;
    private bool LoadingScene = false;
    private AsyncOperation asyncLoad;

    void Awake()
    {
        Initialise();
    }

    void Initialise()
    {
        if (CountDownUI) CountDownUI.SetActive(false);
        if (NotificationUI) NotificationUI.SetActive(false);
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        RefEnemies();
        LevelIntro();
    }

    void LevelIntro()
    {
        if (NotificationUI && IntroText != "")
        {
            //Show Intro Text
            NotificationUI.SetActive(true);
            NotificationUI.GetComponent<Text>().text = IntroText;
            NotificationUI.transform.localScale = Vector3.zero;

            Sequence EnterExit = DOTween.Sequence();
            EnterExit.Append(NotificationUI.transform.DOScale(1f, 1f).SetEase(Ease.OutBack));
            EnterExit.Append(NotificationUI.transform.DOScale(0f, 1f).SetDelay(TimeShown));
            EnterExit.Play();
        }
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
    void Update()
    {
        //if(LoadingScene)
        //{
        //    if(asyncLoad.isDone)
        //    {
        //        LoadingScreenRef.FinishLoadingScreen();
        //    }
        //}
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
        if (LoadingScreenPrefab)
        {
            LoadingScene = true;
            LoadingScreenRef = Instantiate(LoadingScreenPrefab, Vector3.zero, Quaternion.identity).GetComponent<LoadingScreenManager>();
            LoadingScreenRef.GetComponent<LoadingScreenManager>().StartLoadingScreen();
            StartCoroutine(DelayedLoad(1f, MapSceneIndex));
        }
        else
        {
            Debug.Log(gameObject.name + ": No loadingscreen found");
            SceneManager.LoadScene(MapSceneIndex);
        }
    }

    public void LoadScene(int _SceneIndex)
    {
        if (LoadingScreenPrefab)
        {
            LoadingScene = true;
            LoadingScreenRef = Instantiate(LoadingScreenPrefab, Vector3.zero, Quaternion.identity).GetComponent<LoadingScreenManager>();
            LoadingScreenRef.GetComponent<LoadingScreenManager>().StartLoadingScreen();
            StartCoroutine(DelayedLoad(1f, _SceneIndex));
        }
        else
        {
            Debug.Log(gameObject.name + ": No loadingscreen found");
            SceneManager.LoadScene(_SceneIndex);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public GameObject GetCountDownUI()
    {
        return CountDownUI;
    }

    public GameObject GetNotificationUI()
    {
        return NotificationUI;
    }

    static public SceneHandler GetSceneHandler()
    {
        GameObject Obj = GameObject.Find("SceneHandler");
        if (Obj != null)
        {
            return Obj.GetComponent<SceneHandler>();
        }
        else return null;
    }

    IEnumerator DelayedLoad(float _Wait, int _SceneIndex)
    {
        // Wait until the asynchronous scene fully loads
        yield return new WaitForSeconds(_Wait);
        SceneManager.LoadScene(_SceneIndex);
    }
}
