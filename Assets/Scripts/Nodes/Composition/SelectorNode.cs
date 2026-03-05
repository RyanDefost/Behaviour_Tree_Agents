using UnityEngine;

public class SelectorNode : CompositionNode
{
    private int index = 0;

    public SelectorNode(params Node[] nodes)
    {
        this.nodes = nodes;
    }

    public SelectorNode(Node[] preConditions, params Node[] nodes)
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
                case Status.SUCCESS: index = 0; return Status.SUCCESS;
                case Status.FAILURE: continue;
                case Status.RUNNING: index = 0; return Status.RUNNING;
            }
        }
        index = 0;

        return Status.FAILURE;
    }
}
