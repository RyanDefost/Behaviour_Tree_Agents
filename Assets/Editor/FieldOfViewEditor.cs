using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

/// Script Credits
/// Sebastian Lague: https://www.youtube.com/watch?v=rQG9aUWarwE
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;

        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRange);

        Vector3 viewAngleA = fov.DiractionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DiractionFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRange);

        Handles.color = Color.red;
        foreach (var foundTarget in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, foundTarget.position);
        }
    }

}
