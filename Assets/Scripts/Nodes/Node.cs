using UnityEngine;

public enum Status { RUNNING, SUCCESS, FAILURE }

public abstract class Node
{
    public Blackboard Blackboard;

    private bool hasEnterd = false;

    public Status Run()
    {
        if (!hasEnterd)
        {
            OnEnter();
            hasEnterd = true;
        }

        var result = OnUpdate();

        if (result != Status.RUNNING)
        {
            OnExit();
            hasEnterd = false;
        }

        return result;
    }

    public virtual void OnEnter() { }
    public abstract Status OnUpdate();
    public virtual void OnExit() { }

    public virtual void SetupBlackboard(Blackboard blackboard)
    {
        this.Blackboard = blackboard;
    }
}
