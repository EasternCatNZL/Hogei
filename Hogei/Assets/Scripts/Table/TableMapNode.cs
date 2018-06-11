using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableMapNode : MonoBehaviour {

    public bool IsUnlocked = false;
    public bool IsNext = false;
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

    //script refs
    ControllerIndexedMenuItem item;

    void Start()
    {
        if(LevelName) LevelName.transform.DOScaleY(0, 0f);
        NameString = LevelName.GetComponent<TextMesh>().text;
        if (RequiredNode == null)
        {
            IsUnlocked = true;
        }
        ChangeSign();
    }

    public void ChangeSign()
    {
        MeshRenderer Renderer = GetComponent<MeshRenderer>();
        if(IsUnlocked)
        {
            if(IsNext)
            {
                Renderer.material = NextMat;
            }
            else
            {
                Renderer.material = CompleteMat;
            }
        }
        else
        {
            Renderer.material = LockedMat;
        }
    }

    private void OnMouseEnter()
    {
        OnSelected();
    }

    //when seleceted <- mouse hover
    public void OnSelected()
    {
        transform.DOComplete();
        transform.DOShakeRotation(1f, 5, 5, 10);
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

    public void OnMouseExit()
    {
        OnDeselected();
    }

    //when deselected
    public void OnDeselected()
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
            //reactivate mouse when leaving scene
            Cursor.visible = true;
            SceneHandler.GetSceneHandler().LoadScene(LevelIndex);            
            return true;
        }
        
        //if didn't work reselect self
        item.Selected();
        return false;
    }

    //when interacted with
    public void OnInteract()
    {
        LoadLevel();
    }

    public void SetUnlocked() { IsUnlocked = true; }
    public void SetLocked() { IsUnlocked = false; }
}
