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
        //Debug.Log(target);
        foreach (var visibleItem in fov.visibleTargets)
        {
            if (target == visibleItem.position)
            {
                return Status.SUCCESS;
            }
        }

        return Status.FAILURE;
    }
}
