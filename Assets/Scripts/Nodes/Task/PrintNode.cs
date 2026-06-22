using Unity.VisualScripting;
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
        
        this.NodeName = $"{this.GetType().Name} {this.debugMessage}";
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        Debug.Log(debugMessage);
        return Status.SUCCESS;
    }
}
