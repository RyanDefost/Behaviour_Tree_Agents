using UnityEngine;

public class TaskNode : Node
{
    protected BaseAgent BaseAgent;

    public TaskNode(BaseAgent baseAgent)
    {
        this.BaseAgent = baseAgent;
    }

    public override void OnEnter()
    {
        
    }


    public override Status OnUpdate()
    {
        //Debug.Log("Update state");
        return Status.SUCCESS; // BASE RETURN VALUE;
    }

    public override void OnExit()
    {
        
    }
}
