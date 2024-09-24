using System.Collections;
using UnityEditor;
using UnityEngine;

#if(UNITY_EDITOR)
[CustomEditor(typeof(LOS))]
public class LOSEditor : Editor
{
    
    private void OnSceneGUI()
    {
        
        LOS los = (LOS)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(los.transform.position, Vector3.up, Vector3.forward, 360, los.viewRadius);
        Vector3 viewAngleA = los.DirFromAngle(-los.viewAngle / 2, false);
        Vector3 viewAngleB = los.DirFromAngle(los.viewAngle / 2, false);
        Handles.DrawLine(los.transform.position, los.transform.position + viewAngleA * los.viewRadius);
        Handles.DrawLine(los.transform.position, los.transform.position + viewAngleB * los.viewRadius);
        Handles.color = Color.red;
        foreach(Transform visibleTarget in los.visibleTargets)
        {
            Handles.DrawLine(los.transform.position, visibleTarget.position);
        }
        
    }
    
}
#else
#endif