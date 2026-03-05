using UnityEngine;

public class InverterNode : DecoratorNode
{
    private Status inputStatus;

    public InverterNode(Status status)
    {
        this.inputStatus = status;
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        switch (this.inputStatus)
        {
            case Status.SUCCESS: return Status.FAILURE;
            case Status.FAILURE: return Status.SUCCESS;
            case Status.RUNNING: return Status.RUNNING;
            default: break;
        }       //NOT SURE IF IS CORRECT.

        return Status.RUNNING;
    }
}