using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : TaskNode
{
    private NavMeshAgent navMeshAgent;

    private string BBTarget;
    private Vector3 target;
    private float speed;

    public MoveToTarget(Agent agent, NavMeshAgent navMeshAgent, string BBtarget, float speed) : base(agent)
    {
        this.navMeshAgent = navMeshAgent;
        this.BBTarget = BBtarget;
        this.speed = speed;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.target = this.Blackboard.GetValue<Vector3>(BBTarget);
    }

    public override Status OnUpdate()
    {
        Debug.Log("MOVING TO " + target);

        Vector3 velocity = (target - Agent.transform.position).normalized * this.speed;
        this.navMeshAgent.SetDestination(navMeshAgent.transform.position + velocity * Time.deltaTime);

        Debug.Log(Vector3.Distance(Agent.transform.position, target));
        if (Vector3.Distance(Agent.transform.position, target) <= 1.5f)
        {
            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }
}