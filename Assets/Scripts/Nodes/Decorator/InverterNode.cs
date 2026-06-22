using UnityEngine;

public class InverterNode : DecoratorNode
{
    private Node inputNode;

    public InverterNode(Node node)
    {
        this.inputNode = node;
    }

    public override Status OnUpdate()
    {
        var status = inputNode.Run();

        switch (status)
        {
            case Status.SUCCESS: return Status.FAILURE;
            case Status.FAILURE: return Status.SUCCESS;
            case Status.RUNNING: return Status.RUNNING;
            default: break;
        }       //NOT SURE IF IS CORRECT.

        return Status.RUNNING;
    }

    public override void SetupBlackboard(Blackboard blackboard)
    {
        base.SetupBlackboard(blackboard);

        this.inputNode.SetupBlackboard(blackboard);
    }
}