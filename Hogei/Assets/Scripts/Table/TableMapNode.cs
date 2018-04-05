using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableMapNode : MonoBehaviour {

    public bool IsUnlocked = false;
    [Header("Level Info")]
    public GameObject LevelName;
    public int LevelIndex;

    public TableMapNode NextNode;

    private void OnMouseEnter()
    {
        LevelName.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutBounce);
    }

    private void OnMouseExit()
    {
        LevelName.transform.DOScaleY(0, 0.5f);
    }

    public void LoadLevel()
    {
        SceneHandler.GetSceneHandler().LoadScene(LevelIndex);
    }
}
