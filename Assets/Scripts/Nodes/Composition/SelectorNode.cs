using UnityEngine;

public class SelectorNode : CompositionNode
{
    private int index = 0;

    public SelectorNode(params Node[] nodes)
    {
        this.nodes = nodes;
        
        this.NodeName = $"{this.GetType().Name}";
    }

    public SelectorNode(Node[] preConditions, params Node[] nodes)
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
