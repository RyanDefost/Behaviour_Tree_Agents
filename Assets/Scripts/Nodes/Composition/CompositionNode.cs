using System.Linq;
using UnityEngine;

public class CompositionNode : Node
{
    protected Node[] nodes;
    protected Node[] preConditions;

    public bool hasEnterd  = false;

    public override Status OnUpdate()
    {
        return Status.SUCCESS; // BASE RETURN VALUE;
    }

    public override void SetupBlackboard(Blackboard blackboard)
    {
        base.SetupBlackboard(blackboard);

        foreach (var node in nodes)
        {
            node.SetupBlackboard(this.Blackboard);
        }

        if (preConditions == null) return;
        foreach (var preCon in preConditions)
        {
            preCon.SetupBlackboard(this.Blackboard);
        }
    }
    
    public override string GetName()
    {
        string baseName = this.NodeName;

        if(preConditions != null) baseName = preConditions.Where(node => node.hasEnterd)
            .Aggregate(baseName, (current, node) => current + ("\n preCon: " + node.GetName()));

        if(nodes != null) baseName = nodes.Where(node => node.hasEnterd).
            Aggregate(baseName, (current, node) => current + ("\n Node: " + node.GetName()));

        return baseName;
    }
}
