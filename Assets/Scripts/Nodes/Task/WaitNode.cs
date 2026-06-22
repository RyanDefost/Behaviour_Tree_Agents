using UnityEngine;

public class WaitNode : TaskNode
{
    private float timeInSeconds;
    private float timeRemaining;

    public WaitNode(Agent agent, float timeInSeconds) : base(agent)
    {
        this.timeInSeconds = timeInSeconds;
    }

    public override void OnEnter()
    {
        this.timeRemaining = timeInSeconds;
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
