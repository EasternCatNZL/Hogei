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
        MouseClickMove();
        
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

    //Move via mouse click logic
    private void MouseClickMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Clicked");
            RaycastHit hit;
            //ray cast from mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if ray hits
            if(Physics.Raycast(ray, out hit))
            {
                print("Ray out");
                if(hit.collider.gameObject.GetComponent<MapNode>())
                {
                    print("Found");
                    mapCamera.SetupMovement(hit.collider.gameObject.transform.position);
                    currentNode = hit.collider.gameObject.GetComponent<MapNode>();
                }
            }
        }
    }

    //select node logic
    private void SelectNode()
    {
        //check for input
        if (Input.GetAxis("Submit") != 0){
            currentNode.LoadMyScene();
        }
    }
}
