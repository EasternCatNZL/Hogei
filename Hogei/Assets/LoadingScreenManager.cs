using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingScreenManager : MonoBehaviour {

    [Header("Image Refs")]
    public GameObject Background;
    public GameObject LoadingText;
    [Header("Chicken Settings")]
    public GameObject Chicken;
    public float JumpHeight;
    public float JumpDelay;

    private bool EnableChickenJump = false;
    private float LastTime;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
        Background.transform.localPosition = new Vector3(0f, GetComponent<RectTransform>().rect.height, 0f);
        LoadingText.transform.localScale = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (EnableChickenJump)
        {
            if (Time.time - LastTime > JumpDelay)
            {
                ChickenJump();
                LastTime = Time.time;
            }
        }
	}

    void ChickenJump()
    {
        float RectWidth = GetComponent<RectTransform>().rect.width;
        float StartingHeight = -(GetComponent<RectTransform>().rect.height / 2 + Chicken.GetComponent<RectTransform>().rect.height / 2);
        float StartingX = Random.Range(-(RectWidth/2), RectWidth/2);
        Chicken.transform.localPosition = new Vector3(StartingX, StartingHeight, 0f);
        Chicken.transform.DOJump(Chicken.transform.position, JumpHeight, 1, 2f);
    }

    public void StartLoadingScreen()
    {
        Background.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBounce);
        LoadingText.transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
        //EnableChickenJump = true;
    }

    public void FinishLoadingScreen()
    {
        Background.transform.DOLocalMoveY(GetComponent<RectTransform>().rect.height, 1f).SetEase(Ease.InOutBack);
        EnableChickenJump = false;
        Destroy(gameObject, 1f);
    }
    private void OnSceneLoad(Scene _Scene, LoadSceneMode _Mode)
    {
        FinishLoadingScreen();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

}
