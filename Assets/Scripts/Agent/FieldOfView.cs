using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRange;
    [Range(0, 360)] public float viewAngle;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    public List<Transform> visibleTargets = new();

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
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRange, TargetMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            Transform target = targetsInView[i].transform;

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
}
