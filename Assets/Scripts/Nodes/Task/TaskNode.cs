using UnityEngine;

public class TaskNode : Node
{
    protected Agent Agent;

    public TaskNode(Agent agent)
    {
        this.Agent = agent;
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
