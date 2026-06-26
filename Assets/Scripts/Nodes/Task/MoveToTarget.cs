using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : TaskNode
{
    private NavMeshAgent navMeshAgent;

    private string BBTarget;
    private Vector3 target;
    private float speed;

    public MoveToTarget(BaseAgent baseAgent, NavMeshAgent navMeshAgent, string BBtarget, float speed) : base(baseAgent)
    {
        this.navMeshAgent = navMeshAgent;
        this.BBTarget = BBtarget;
        this.speed = speed;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.NodeName = $"{this.GetType().Name} {this.target}";
    }

    public override Status OnUpdate()
    {
        this.target = this.Blackboard.GetValue<Vector3>(BBTarget);
        
        Vector3 velocity = (target - BaseAgent.transform.position).normalized * this.speed;
        this.navMeshAgent.SetDestination(navMeshAgent.transform.position + velocity * Time.deltaTime);

        if (Vector3.Distance(BaseAgent.transform.position, target) <= 1.5f)
        {
            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}