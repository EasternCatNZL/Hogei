using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TableManager))]
[CanEditMultipleObjects]
public class TableManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TableManager TableMgt = (TableManager)target;

        if(GUILayout.Button("Change Config"))
        {
            TableMgt.ChangeConfiguration();
        }
    }
}
