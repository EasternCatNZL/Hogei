using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CloudManager))]
public class CloudManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CloudManager CloudMgt = (CloudManager)target;

        if(GUILayout.Button("Re-init clouds"))
        {
            CloudMgt.Init();
        }
    }
}
