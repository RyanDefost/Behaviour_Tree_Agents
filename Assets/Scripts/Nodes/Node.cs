using Unity.VisualScripting;
using UnityEngine;

public enum Status { RUNNING, SUCCESS, FAILURE }

public abstract class Node
{
    public Blackboard Blackboard;

    public bool hasEnterd { get; private set; } = false;
    public string NodeName = "Node";

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

    public virtual void OnEnter()
    {
    } //NodeName = this.GetType().Name;

    public abstract Status OnUpdate();

    public virtual void OnExit()
    {
    }

    public virtual void SetupBlackboard(Blackboard blackboard)
    {
        this.Blackboard = blackboard;
    }

    public virtual string GetName() =>  this.NodeName;

}
