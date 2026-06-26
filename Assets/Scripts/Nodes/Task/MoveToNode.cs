using UnityEngine;
using UnityEngine.AI;

public class MoveToNode : TaskNode
{
    private NavMeshAgent navMeshAgent;
    private WaypointSystem waypoints;
    private Transform currentTarget;
    private float speed;

    public MoveToNode(BaseAgent baseAgent, NavMeshAgent navMeshAgent, WaypointSystem waypoints, float speed) : base(baseAgent)
    {
        this.navMeshAgent = navMeshAgent;
        this.waypoints = waypoints;
        this.speed = speed;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        currentTarget = waypoints.CurrentPoint;
        
        this.NodeName = $"{this.GetType().Name} {this.currentTarget.name}";
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        if (Vector3.Distance(BaseAgent.transform.position, currentTarget.position) <= 1)
        {
            return Status.FAILURE;  //SOULD NOT BE FAILURE, NEEDED TO TEST SOMETHING
        }                           //Implement ReverseNode;

        Vector3 velocity = (currentTarget.position - BaseAgent.transform.position).normalized * this.speed;
        this.navMeshAgent.SetDestination(navMeshAgent.transform.position + velocity * Time.deltaTime);

        return Status.RUNNING;
    }
}
