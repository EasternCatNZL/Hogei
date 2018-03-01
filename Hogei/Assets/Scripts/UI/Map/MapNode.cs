using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour {

    [System.Serializable]
    public enum Connections
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    [System.Serializable]
    public struct Neighbour
    {
        public MapNode neighbour;
        public Connections connection;
    }

    [Header("Scene")]
    [Tooltip("The scene this node directs to")]
    public int sceneNumber = 0;

    [Header("Connections")]
    public Neighbour[] myNeighbours = new Neighbour[0];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public MapNode CheckDirectionForNeighbour(Connections connect)
    {
        MapNode thisNeighbour = null;
        //check if any of the neighbours have connection in this direction
        for(int i = 0; i < myNeighbours.Length; i++)
        {
            if(myNeighbours[i].connection == connect)
            {
                thisNeighbour = myNeighbours[i].neighbour;
            }
        }

        return thisNeighbour;
    }
}
