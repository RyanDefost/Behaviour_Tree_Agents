using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    public float PointRange = 1;

    public Transform CurrentPoint { get; private set; }

    private void OnEnable()
    {
        SetCurrentPoint(points[0]);
    }

    public List<Transform> GetPoints()
    {
        return points;
    }

    public Transform NextPoint(Transform currentPoint)
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i].position == currentPoint.position)
            {
                if (i == 0) return points[^1];
                else return points[i - 1];
            }
        }

        Debug.LogError($"Next point in waypoint not found from [{currentPoint}]. Setting to default startPoint.");
        return points[0];
    }

    public Transform GetClosest(Vector3 currentPosition)
    {
        var closestPoint = points[0];
        foreach (var point in points)
        {
            if (Vector3.Distance(currentPosition, point.position) < Vector3.Distance(currentPosition, closestPoint.position))
            {
                closestPoint = point;
            }
        }

        return closestPoint;
    }

    public void SetCurrentPoint(Transform newPoint) => this.CurrentPoint = newPoint;
}