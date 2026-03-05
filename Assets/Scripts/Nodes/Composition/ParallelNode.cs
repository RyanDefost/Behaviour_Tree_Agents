using UnityEngine;

public class ParallelNode : CompositionNode
{
    private int index = 0;

    public ParallelNode(params Node[] nodes)
    {
        this.nodes = nodes;
    }

    public ParallelNode(Node[] preConditions, params Node[] nodes)
    {
        this.preConditions = preConditions;
        this.nodes = nodes;
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        if (!CheckPreConditions()) return Status.FAILURE;
        return CheckChildNodes();
    }

    private bool CheckPreConditions() //NEEDS TO BE IMPLEMENTED!
    {
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
