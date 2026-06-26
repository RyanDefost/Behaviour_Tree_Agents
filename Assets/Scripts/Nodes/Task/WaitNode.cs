using UnityEngine;

public class WaitNode : TaskNode
{
    private float timeInSeconds;
    private float timeRemaining;

    public WaitNode(BaseAgent baseAgent, float timeInSeconds) : base(baseAgent)
    {
        this.timeInSeconds = timeInSeconds;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.timeRemaining = timeInSeconds;
        
        this.NodeName = $"{this.GetType().Name} {this.timeRemaining}";
    }

    public override Status OnUpdate()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            return Status.RUNNING;
        }

        timeRemaining = timeInSeconds;
        
        return Status.SUCCESS;
    }
}
