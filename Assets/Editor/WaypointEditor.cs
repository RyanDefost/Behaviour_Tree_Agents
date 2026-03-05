using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointSystem))]
public class WaypointEditor : Editor
{
    private void OnSceneGUI()
    {
        WaypointSystem wayPoints = (WaypointSystem)target;

        List<Transform> points = wayPoints.GetPoints();
        for (int i = 0; i < points.Count; i++)
        {
            Handles.color = Color.white;
            if (i == 0) Handles.DrawLine(points[i].position, points[^1].position);
            else Handles.DrawLine(points[i].position, points[i - 1].position);

            Handles.color = Color.green;
            Handles.DrawWireArc(points[i].position, Vector3.up, Vector3.forward, 360, wayPoints.PointRange);
        }
    }
}
