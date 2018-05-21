using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HoverOverText))]
[CanEditMultipleObjects]
public class HoverOverTextEditor : Editor {

    private HoverOverText m_HOTE = null;

    private void OnEnable()
    {
        m_HOTE = (HoverOverText)target;
    }

    public override void OnInspectorGUI()
    {
        m_HOTE.TextObject = (GameObject) EditorGUILayout.ObjectField("Text Object: ",m_HOTE.TextObject, typeof(GameObject), true);
        EditorGUILayout.Separator();

        m_HOTE.CreateOwnTextObject = EditorGUILayout.Toggle("Create Own Text Object?", m_HOTE.CreateOwnTextObject);
        
        EditorGUILayout.BeginVertical();
        if(m_HOTE.CreateOwnTextObject)
        {
            EditorGUILayout.Separator();
            m_HOTE.Text = EditorGUILayout.TextField("Text", GUILayout.MinHeight(50));
            m_HOTE.TextFont = (Font) EditorGUILayout.ObjectField("Text Font:", m_HOTE.TextFont, typeof(Font), false);
            m_HOTE.FontMaterial = (Material)EditorGUILayout.ObjectField("Font Material:", m_HOTE.FontMaterial, typeof(Material), false);
            m_HOTE.TextSize = EditorGUILayout.FloatField("Text Size:", 1f);
            m_HOTE.TextOffset = EditorGUILayout.Vector3Field("Text Offset:", Vector3.zero);
            m_HOTE.FaceCamera = EditorGUILayout.Toggle("Face Camera:", m_HOTE.FaceCamera);
        }
        EditorGUILayout.EndVertical();
    }
}
