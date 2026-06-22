using UnityEngine;

public class ParallelNode : CompositionNode
{
    private int index = 0;

    public ParallelNode(params Node[] nodes)
    {
        this.nodes = nodes;
        
        this.NodeName = $"{this.GetType().Name}";
    }

    public ParallelNode(Node[] preConditions, params Node[] nodes)
    {
        this.preConditions = preConditions;
        this.nodes = nodes;
        
        this.NodeName = $"{this.GetType().Name} + Conditions:";
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        if (!CheckPreConditions()) return Status.FAILURE;
        return CheckChildNodes();
    }

    private bool CheckPreConditions()
    {
        if (preConditions == null) return true;
        foreach (var condition in preConditions)
        {
            if (condition.Run() == Status.FAILURE) return false;
        }

        return true;
    }

    private Status CheckChildNodes()
    {
        for (; index < nodes.Length; index++)
        {
            nodes[index].Run();
        }
        index = 0;

        return Status.SUCCESS;
    }

}
