using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRange;
    [Range(0, 360)] public float viewAngle;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public List<Transform> visibleTargets = new();
    public List<Transform> allTargets = new();

    private bool isActive;
    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(FindTargetWithDelay(.2f));
    }
    private void OnDisable()
    {
        isActive = false;
        StopCoroutine(FindTargetWithDelay(.2f));
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (isActive)
        {
            yield return new WaitForSeconds(delay);
            FindVisableTargets();
        }
    }

    private void FindVisableTargets()
    {
        visibleTargets.Clear();
        allTargets.Clear();

        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRange, TargetMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            Transform target = targetsInView[i].transform;
            allTargets.Add(target);

            Vector3 targetDir = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, targetDir) < viewAngle / 2)
            {

                float distance = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, targetDir, distance, ObstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
    
    public Vector3 DiractionFromAngle(float angleDegrees, bool isGlobal)
    {
        if (!isGlobal) angleDegrees += transform.eulerAngles.y;

        return new Vector3(
            Mathf.Sin(angleDegrees * Mathf.Deg2Rad),
            0,
            Mathf.Cos(angleDegrees * Mathf.Deg2Rad)
        );
    }

    public Vector3 TryGetClosestVector(Transform agent, LayerMask layerMask, bool isVisable = true)
    {
        Vector3 closestTarget = Vector3.zero;
        float closestDistance = math.INFINITY;
        foreach (var transform in allTargets)
        {
            if (!visibleTargets.Contains(transform) && isVisable) continue;
            
            if(transform.gameObject.layer != layerMask) continue;
            
            var distance = Vector3.Distance(transform.position, agent.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = transform.position;
            }
        }

        return closestTarget;
    }

    public Vector3 UpdateLastClosestVector(Transform agent, Vector3 currentVector)
    {
        float closestDistance = math.INFINITY;
        foreach (var transform in allTargets)
        {
            if (!visibleTargets.Contains(transform)) continue;

            var distance = Vector3.Distance(transform.position, agent.position);
            if (distance < closestDistance)
            {
                currentVector = transform.position;
            }
        }

        return currentVector;
    }

    public bool DetectingPlayer(Transform target, bool currentState)
    {
        if(visibleTargets.Contains(target))
            return true;
        
        if(currentState && allTargets.Contains(target))
            return true;
        
        return false;
    }
}
