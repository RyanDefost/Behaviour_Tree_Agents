using UnityEngine;

public class SequenceNode : CompositionNode
{
    private int index = 0;

    public SequenceNode(params Node[] nodes)
    {
        this.nodes = nodes;
    }

    public SequenceNode(Node[] preConditions, params Node[] nodes)
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
            switch (nodes[index].Run())
            {
                case Status.SUCCESS: continue;
                case Status.FAILURE: index = 0; return Status.FAILURE;
                case Status.RUNNING: return Status.RUNNING;
            }
        }

        index = 0;
        return Status.SUCCESS;
    }

}
