using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntityHealth))]
[CanEditMultipleObjects]
public class EntityHealthEditor : Editor {

    //private EntityHealth EntHealth = null;

    //SerializedProperty DeathFunc;
    //SerializedProperty SerialHitVFX;

    private void OnEnable()
    {
        //EntHealth = (EntityHealth)target;
        //DeathFunc = serializedObject.FindProperty("DeathFunction");
        //SerialHitVFX = serializedObject.FindProperty("HitVFX");
    }

    public override void OnInspectorGUI()
    {
        EntityHealth Entity = (EntityHealth)target;
        ////Health Settings
        //GUILayout.Label("Health Settings", EditorStyles.boldLabel);

        //GUILayout.BeginHorizontal();
        //GUILayout.Label("Maximum Health", GUILayout.Width(215));
        //EntHealth.MaxHealth = EditorGUILayout.FloatField(EntHealth.MaxHealth);
        //GUILayout.EndHorizontal();

        ////VFX Settings
        //GUILayout.Label("VFX Settings", EditorStyles.boldLabel);

        //GUILayout.BeginHorizontal();
        //GUILayout.Label("On Hit Shake", GUILayout.Width(215));
        //EntHealth.OnHitShake = EditorGUILayout.Toggle(EntHealth.OnHitShake);
        //GUILayout.EndHorizontal();

        //EditorGUILayout.ObjectField(SerialHitVFX);

        //GUILayout.BeginHorizontal();
        //GUILayout.Label("Modify Hit VFX", GUILayout.Width(215));
        //EntHealth.ModHitVFX = EditorGUILayout.Toggle(EntHealth.ModHitVFX);
        //GUILayout.EndHorizontal();

        //if (EntHealth.ModHitVFX)
        //{          
        //    GUILayout.BeginHorizontal();
        //    GUILayout.Space(15);
        //    EntHealth.HitVFXScale = EditorGUILayout.Vector3Field(new GUIContent("Hit VFX Scale", "Multiply the Hit VFX scale by this vector"), Vector3.zero);
        //    GUILayout.EndHorizontal();
        //}
        ////Death Settings
        //GUILayout.Label("OnDeath Settings", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(DeathFunc, new GUIContent("Death Function"));
        //serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();


        if (GUILayout.Button("Kill Entity"))
        {
            Entity.Kill();
        }
    }
}
