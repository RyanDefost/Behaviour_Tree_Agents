using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TargetInView : DecoratorNode
{
    private FieldOfView fov;
    private Vector3 target;
    private string BBTarget;

    public TargetInView(FieldOfView fov, string BBTarget)
    {
        this.fov = fov;
        this.BBTarget = BBTarget;
    }

    public override void OnEnter()
    {
        this.target = Blackboard.GetValue<Vector3>(this.BBTarget);
    }

    public override Status OnUpdate()
    {
        bool targetInRange = false;
        foreach (var rangeTarget in fov.allTargets)
        {
            if(target == rangeTarget.position)
                targetInRange = true;
        }
        if(!targetInRange)
            this.Blackboard.SetValue("DETECTEDPLAYER", targetInRange);
        
        if(this.Blackboard.GetValue<bool>("DETECTEDPLAYER"))
            return Status.SUCCESS;
        
        //Debug.Log(target);
        foreach (var visibleItem in fov.visibleTargets)
        {
            if (target == visibleItem.position)
            {
                this.Blackboard.SetValue("DETECTEDPLAYER", targetInRange);
                return Status.SUCCESS;
            }
        }
        return Status.FAILURE;
    }
}
