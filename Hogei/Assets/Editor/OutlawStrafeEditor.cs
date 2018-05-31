using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OutlawStrafe))]
public class OutlawStrafeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        OutlawStrafe Outlaw = (OutlawStrafe)target;


        if (GUILayout.Button("Move Outlaw"))
        {
            if (Outlaw.CanStrafe) Outlaw.CanStrafe = false;
            else Outlaw.CanStrafe = true;
        }
    }

}
