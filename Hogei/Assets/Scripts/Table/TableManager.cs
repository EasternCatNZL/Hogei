using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour {

    public int MapNodeLayer = 0;
    public List<TableMapNode> MapNodes;

    public CloudManager CloudMgt;

    private TableMapNode PrevLevel;

    private Animator Anim;
    private bool IsOpen = false;

	// Use this for initialization
	void Start () {
        //Get attached animator
		if(GetComponent<Animator>())
        {
            Anim = GetComponent<Animator>();
        }
        else
        {
            Debug.Log("No animator attached to " + gameObject.name);
        }
        if(MapNodes == null) MapNodes = new List<TableMapNode>();
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

    public void OpenMap()
    {
        if (CloudMgt)
        {
            CloudMgt.HideClouds();
        }
        Anim.SetTrigger("OpenMap");
        IsOpen = true;
    }

    public void CloseMap()
    {
        if (CloudMgt)
        {
            CloudMgt.ShowClouds();
        }
        Anim.SetTrigger("CloseMap");
        IsOpen = false;
    }

    public void ChangeConfiguration()
    {
        if(IsOpen)
        {
            CloseMap();
        }
        else
        {
            OpenMap();
        }
    }
}
