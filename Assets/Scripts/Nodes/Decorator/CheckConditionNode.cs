using Unity.VisualScripting;
using UnityEngine;

public class CheckConditionNode<T> : DecoratorNode
{
    string condition;

    public CheckConditionNode(string BBkey)
    {
        this.condition = BBkey;
    }

    public override Status OnUpdate()
    {
        if (Blackboard.GetValue<bool>(this.condition)) return Status.SUCCESS;
        else return Status.FAILURE;
    }
}
