using UnityEngine;

public class CompositionNode : Node
{
    protected Node[] nodes;
    protected Node[] preConditions;

    private bool hasEnterd = false;

    public override Status OnUpdate()
    {
        return Status.SUCCESS; // BASE RETURN VALUE;
    }

    public override void SetupBlackboard(Blackboard blackboard)
    {
        base.SetupBlackboard(blackboard);

        foreach (var node in nodes)
        {
            node.SetupBlackboard(blackboard);
        }
    }
}
