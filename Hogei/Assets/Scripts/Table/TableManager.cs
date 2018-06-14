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

    [Header("Soup manager")]
    public SoupManager soup;

    [Header("Menus")]
    public ControllerMenuNavigator navigator;
    public ControllerIndexedMenu mapMenu;
    public ControllerIndexedMenu openBoxMenu;
    

    [Header("Tags")]
    public string playerTag = "Player";

    [Header("Inputs")]
    public string cOpenBox = "CSwitch";

    [Header("Debugs")]
    public bool UnlockAllLevels = false;

    private Animator Anim;
    private bool IsOpen = false;

    //script refs
    WhatCanIDO canDo;

    // Use this for initialization
    void Start()
    {
        canDo = GameObject.FindGameObjectWithTag(playerTag).GetComponent<WhatCanIDO>();

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
        
        if(UnlockAllLevels)
        {
            UnlockLevels();
        }
        else
        {
            UnlockMapNodes();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (canDo.useKeyboard)
        {
            ClickMapNodeMouse();
        }
        //else if (canDo.useController)
        //{
        //    if (Luminosity.IO.InputManager.GetButton(cOpenBox))
        //    {
        //        ChangeConfiguration();
        //    }
        //}
	}

    //mouse click on map nodes
    void ClickMapNodeMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit RayHit = MouseTarget.GetWorldMouseHit(1 << MapNodeLayer);
            if (RayHit.collider)
            {
                GameObject ObjHit = RayHit.collider.gameObject;
                if (ObjHit.GetComponent<TableMapNode>())
                {
                    ObjHit.GetComponent<TableMapNode>().LoadLevel();
                }
            }
        }
    }

    //controller on map node

    public void UnlockMapNodes()
    {
        List<int> Unlocks = PlayerManager.GetInstance().GetLevelsCompleted();
        TableMapNode LastNode = null;
        foreach(TableMapNode _Node in MapNodes)
        {           
            if (_Node.RequiredNode == null)
            {
                _Node.IsUnlocked = true;
                _Node.ChangeSign();
                LastNode = _Node;
                continue;
            }
            if(Unlocks.Contains(_Node.RequiredNode.LevelIndex))
            {
                _Node.IsUnlocked = true;
                _Node.ChangeSign();
                LastNode = _Node;
                continue;
            }
            else
            {
                LastNode.IsNext = true;
                LastNode.ChangeSign();
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

                GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");
                //for all the objects
                for(int i = 0; i < ingredients.Length; i++)
                {
                    ingredients[i].GetComponent<SoupIngredient>().SendBackToBowl();
                }



                //close weapon invent
                WeapInvent.CloseInventory();
                
                
                //clear the soup
                soup.ClearSoup();

                if (navigator && mapMenu)
                {
                    //set menu to navigate to map menu
                    navigator.SetMenu(mapMenu);
                }
            }
            else//Show Hot Pot
            {
                OpenMap();
                WeapInvent.SetActive(true);
                OpenButton.enabled = false;
                CloseButton.enabled = true;
                if (navigator && openBoxMenu)
                {
                    //set menu to navigate to menu for open box
                    navigator.SetMenu(openBoxMenu);
                }
                
            }
        }
    }

    public void UnlockLevels()
    {
        foreach(TableMapNode _Node in MapNodes)
        {
            _Node.IsUnlocked = true;
            _Node.ChangeSign();
        }
    }

    public void DisableNodeInteraction()
    {
        foreach(TableMapNode _Node in MapNodes)
        {
            _Node.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    public void EnableNodeInteraction()
    {
        foreach (TableMapNode _Node in MapNodes)
        {
            _Node.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
    public bool GetIsOpen() { return IsOpen; }
}
