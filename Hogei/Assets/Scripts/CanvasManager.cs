using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

    public static CanvasManager Singleton;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        if (Singleton)
        {
            Destroy(gameObject);
        }
        else if(!Singleton)
        {
            Singleton = this;
        }
    }
	
}
