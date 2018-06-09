using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableMapNode : MonoBehaviour {

    public bool IsUnlocked = false;
    [Header("Level Info")]
    public GameObject LevelName;
    public int LevelIndex;

    [Header("Materials")]
    public Material CompleteMat;
    public Material NextMat;
    public Material LockedMat;

    private string NameString;
    private bool Disabled = false;

    [Tooltip("The node required to unlock this one")]
    public TableMapNode RequiredNode;

    void Start()
    {
        if(LevelName) LevelName.transform.DOScaleY(0, 0f);
        NameString = LevelName.GetComponent<TextMesh>().text;
        if (RequiredNode == null)
        {
            IsUnlocked = true;
        }
    }

    public void ChangeSign()
    {
        MeshRenderer Renderer = GetComponent<MeshRenderer>();
        if(IsUnlocked)
        {
            if(RequiredNode.IsUnlocked)
            {
                Renderer.material = CompleteMat;
            }
            else
            {
                Renderer.material = NextMat;
            }
        }
        else
        {
            Renderer.material = LockedMat;
        }
    }

    private void OnMouseEnter()
    {
        if (!Disabled)
        {
            if (!IsUnlocked)
            {
                LevelName.GetComponent<TextMesh>().text = "Level Locked";
            }
            else
            {
                LevelName.GetComponent<TextMesh>().text = NameString;
            }
            LevelName.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutBounce);
        }
    }

    private void OnMouseExit()
    {
        if (!Disabled)
        {
            LevelName.transform.DOScaleY(0, 0.5f);
        }
    }


    public bool LoadLevel()
    {
        if (IsUnlocked)
        {
            SceneHandler.GetSceneHandler().LoadScene(LevelIndex);
            return true;
        }
        return false;
    }

    public void SetUnlocked() { IsUnlocked = true; }
    public void SetLocked() { IsUnlocked = false; }
}
