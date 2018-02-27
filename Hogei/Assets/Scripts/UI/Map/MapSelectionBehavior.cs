using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionBehaviour : MonoBehaviour {

    [Header("Starting node")]
    [Tooltip("The node that selection starts at")]
    public MapNode startNode;

    //control vars
    private MapNode currentNode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
                currentNode = startNode.CheckDirectionForNeighbour(MapNode.Connections.RIGHT);
            }
        }
        else if (Input.GetAxis("Horizontal") < 0.0f)
        {
            //check for adjacent left
            if (currentNode.CheckDirectionForNeighbour(MapNode.Connections.LEFT))
            {
                currentNode = startNode.CheckDirectionForNeighbour(MapNode.Connections.LEFT);
            }
        }
        else if (Input.GetAxis("Vertical") > 0.0f)
        {
            //check for adjacent left
            if (currentNode.CheckDirectionForNeighbour(MapNode.Connections.UP))
            {
                currentNode = startNode.CheckDirectionForNeighbour(MapNode.Connections.UP);
            }
        }
        else if (Input.GetAxis("Vertical") < 0.0f)
        {
            //check for adjacent left
            if (currentNode.CheckDirectionForNeighbour(MapNode.Connections.DOWN))
            {
                currentNode = startNode.CheckDirectionForNeighbour(MapNode.Connections.DOWN);
            }
        }
    }

    
}
