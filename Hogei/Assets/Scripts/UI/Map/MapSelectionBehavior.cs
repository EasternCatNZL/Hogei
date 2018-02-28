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
        MoveNode();
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

    //Signal camera to follow
    private void SignalCameraFollow()
    {

    }
}
