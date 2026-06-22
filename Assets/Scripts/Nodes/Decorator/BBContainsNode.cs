using UnityEngine;

public class BBContainsNode<T> : DecoratorNode
{
    string BBKey;

    public BBContainsNode(string BBKey)
    {
        this.BBKey = BBKey;
    }

    public override Status OnUpdate()
    {
        if (Blackboard.ContainsKey(BBKey)) return Status.FAILURE;
        else return Status.SUCCESS;
    }
}
