using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionBehavior : MonoBehaviour {

    [Header("Starting node")]
    [Tooltip("The node that selection starts at")]
    public MapNode startNode;

    //control vars
    [Header("Current node")]
    public MapNode currentNode;

    private LineRenderer line;

    //script refs
    public MapCameraBehavior mapCamera;

	// Use this for initialization
	void Start () {
        currentNode = startNode;
	}
	
	// Update is called once per frame
	void Update () {
        MoveInput();

        SelectNode();
    }

    private void MoveInput()
    {
        MoveNode();
        MouseClickMap();
        
    }

    //Move node logic
    private void MoveNode()
    {
        //Check input
        if(Input.GetAxis("Horizontal") > 0.0f)
        {
            //check for adjacent right
            if (currentNode.CheckDirectionForNeighbour(MapNode.Connections.RIGHT))
            {
                mapCamera.SetupMovement(currentNode.CheckDirectionForNeighbour(MapNode.Connections.RIGHT).gameObject.transform.position);
                currentNode = currentNode.CheckDirectionForNeighbour(MapNode.Connections.RIGHT);
            }
        }
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            //check for adjacent left
            if (currentNode.CheckDirectionForNeighbour(MapNode.Connections.LEFT))
            {
                mapCamera.SetupMovement(currentNode.CheckDirectionForNeighbour(MapNode.Connections.LEFT).gameObject.transform.position);
                currentNode = currentNode.CheckDirectionForNeighbour(MapNode.Connections.LEFT);
            }
        }
        else if (Input.GetAxis("Vertical") > 0.0f)
        {
            //check for adjacent left
            if (currentNode.CheckDirectionForNeighbour(MapNode.Connections.UP))
            {
                mapCamera.SetupMovement(currentNode.CheckDirectionForNeighbour(MapNode.Connections.UP).gameObject.transform.position);
                currentNode = currentNode.CheckDirectionForNeighbour(MapNode.Connections.UP);
            }
        }
        else if (Input.GetAxis("Vertical") < 0.0f)
        {
            //check for adjacent left
            if (currentNode.CheckDirectionForNeighbour(MapNode.Connections.DOWN))
            {
                mapCamera.SetupMovement(currentNode.CheckDirectionForNeighbour(MapNode.Connections.DOWN).gameObject.transform.position);
                currentNode = currentNode.CheckDirectionForNeighbour(MapNode.Connections.DOWN);
            }
        }
    }

    //Mouse click logic
    private void MouseClickMap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MapNode clickedNode = null;
            RaycastHit hit;
            //ray cast from mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if ray hits
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<MapNode>())
                {
                    clickedNode = hit.collider.gameObject.GetComponent<MapNode>();
                }
            }

            if (clickedNode != null)
            {
                if (currentNode == clickedNode)
                {
                    MouseClickSelect();
                }
                else
                {
                    MouseClickMove(clickedNode);
                }
            }
        }
    }

    //Move via mouse click logic
    private void MouseClickMove(MapNode node)
    {
        mapCamera.SetupMovement(node.gameObject.transform.position);
        currentNode = node.gameObject.GetComponent<MapNode>();
    }

    //Select via mouse click
    private void MouseClickSelect()
    {
        currentNode.LoadMyScene();
    }

    //select node logic
    private void SelectNode()
    {
        //check for input
        if (Input.GetAxis("Submit") != 0){
            currentNode.LoadMyScene();
        }
    }

    //draw line to represent connections
    private void DrawLineBetweenNodes(MapNode thisNode, MapNode prevNode)
    {
        MapNode nextNode = null;
        //check for next node from current
        for(int i = 0; i < thisNode.myNeighbours.Length; i++)
        {

        }
    }
}
