using UnityEngine;

public class PrintNode : TaskNode
{
    private string debugMessage;
    public PrintNode(Agent agent, string message = "RUN") : base(agent)
    {
        this.debugMessage = message;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        Debug.Log(debugMessage);
        return Status.SUCCESS;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
