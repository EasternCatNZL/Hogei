using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour {

    [System.Serializable]
    public struct DungeonStatus
    {
        public string name;
        public int id;
        public bool complete;
    }

    [Header("Last dungeon")]
    [Tooltip("The last scene that the player was in")]
    public int lastDungeonSceneNum = 0;

    [Header("Dungeon progression")]
    public DungeonStatus[] dungeonStatus = new DungeonStatus[0];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
