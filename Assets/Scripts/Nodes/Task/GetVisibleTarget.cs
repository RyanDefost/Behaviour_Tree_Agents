using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GetVisibleTarget : TaskNode
{
    private LayerMask targetMask;
    private float range;

    string BBVisibleTargets;
    private List<Transform> visibleTargets = new();

    public GetVisibleTarget(Agent agent, LayerMask targetMask, string BBVisibleTargets) : base(agent)
    {
        this.targetMask = targetMask;
        this.BBVisibleTargets = BBVisibleTargets;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.visibleTargets = Blackboard.GetValue<List<Transform>>(this.BBVisibleTargets);
    }
}
