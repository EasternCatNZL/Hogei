using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    Vector4[,] Tiles;
    public Rect windowRect;

    [MenuItem("Window/Level Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }
	// Use this for initialization
	void Start () {
        Tiles = new Vector4[10,10];
        windowRect = new Rect(20, 20, 120, 50);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(GUIUtility.GUIToScreenPoint(Event.current.mousePosition));
	}

    void OnGUI()
    {
        BeginWindows();        
        windowRect = GUI.Window(0, windowRect, WindowTestFunction, "Level Editor");
        EndWindows();
    }

    void WindowTestFunction(int _WindowID)
    {
        GUI.DragWindow();
    }

    void OnRenderObject()
    {

    }
}
