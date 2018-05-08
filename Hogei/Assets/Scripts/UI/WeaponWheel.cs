using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour {

    public Texture2D PirateWheel;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, Screen.height - 50, 100, 50), PirateWheel, ScaleMode.ScaleToFit, true, 1.0f);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
