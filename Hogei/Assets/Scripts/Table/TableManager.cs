using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableManager : MonoBehaviour {

    public int MapNodeLayer = 0;
    public List<TableMapNode> MapNodes;

    [Header("Camera Settings")]
    public Transform HotPotCameraPosition;
    public Transform MapCameraPosition;

    public CloudManager CloudMgt;

    [Header("Buttons")]
    public Collider OpenButton;
    public Collider CloseButton;

    [Header("Weapon Selection Settings")]
    public WeaponInventory WeapInvent;

    private Animator Anim;
    private bool IsOpen = false;

    // Use this for initialization
    void Start()
    {
        //Get attached animator
        if (GetComponent<Animator>())
        {
            Anim = GetComponent<Animator>();
        }
        else
        {
            Debug.Log("No animator attached to " + gameObject.name);
        }
        if (MapNodes == null) MapNodes = new List<TableMapNode>();
        UnlockMapNodes();
    }
	
	// Update is called once per frame
	void Update () {        
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit RayHit = MouseTarget.GetWorldMouseHit(1 << MapNodeLayer);
            if(RayHit.collider)
            {
                GameObject ObjHit = RayHit.collider.gameObject;
                if(ObjHit.GetComponent<TableMapNode>())
                {
                    ObjHit.GetComponent<TableMapNode>().LoadLevel();
                }
            }
        }
	}

    public void UnlockMapNodes()
    {
        List<int> Unlocks = PlayerManager.GetInstance().GetLevelsCompleted();
        foreach(TableMapNode _Node in MapNodes)
        {
            if (_Node.RequiredNode == null)
            {
                _Node.IsUnlocked = true;
                continue;
            }
            if(Unlocks.Contains(_Node.RequiredNode.LevelIndex))
            {
                _Node.IsUnlocked = true;
                continue;
            }
        }
    }

    public void OpenMap()
    {
        if (CloudMgt)
        {
            CloudMgt.HideClouds();
        }
        //Trigger animation
        Anim.SetTrigger("OpenMap");
        //Move the camera to the correct position
        Sequence MoveCamera = DOTween.Sequence();
        MoveCamera.Insert(0, Camera.main.transform.DOMove(HotPotCameraPosition.position, 1.5f).SetEase(Ease.InQuart));
        MoveCamera.Insert(0, Camera.main.transform.DORotateQuaternion(HotPotCameraPosition.rotation, 1.5f).SetEase(Ease.InQuart));
        MoveCamera.Play();
        IsOpen = true;
    }

    public void CloseMap()
    {
        if (CloudMgt)
        {
            CloudMgt.ShowClouds();
        }
        //Trigger animation
        Anim.SetTrigger("CloseMap");
        //Move the camera to the correct position
        Sequence MoveCamera = DOTween.Sequence();
        MoveCamera.Insert(0, Camera.main.transform.DOMove(MapCameraPosition.position, 1.5f).SetEase(Ease.InQuart));
        MoveCamera.Insert(0, Camera.main.transform.DORotateQuaternion(MapCameraPosition.rotation, 1.5f).SetEase(Ease.InQuart));
        MoveCamera.Play();
        IsOpen = false;
    }

    public void ChangeConfiguration()
    {
        if (!Anim.IsInTransition(0))
        {
            if (IsOpen)//Show MAp
            {
                CloseMap();
                WeapInvent.SetActive(false);
                OpenButton.enabled = true;
                CloseButton.enabled = false;
            }
            else//Show Hot Pot
            {
                OpenMap();
                WeapInvent.SetActive(true);
                OpenButton.enabled = false;
                CloseButton.enabled = true;
            }
        }
    }

    public bool GetIsOpen() { return IsOpen; }
}
