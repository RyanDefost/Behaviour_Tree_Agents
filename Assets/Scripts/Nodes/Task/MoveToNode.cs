using UnityEngine;
using UnityEngine.AI;

public class MoveToNode : TaskNode
{
    private NavMeshAgent navMeshAgent;
    private WaypointSystem waypoints;
    private Transform currentTarget;
    private float speed;

    public MoveToNode(Agent agent, NavMeshAgent navMeshAgent, WaypointSystem waypoints, float speed) : base(agent)
    {
        this.navMeshAgent = navMeshAgent;
        this.waypoints = waypoints;
        this.speed = speed;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        currentTarget = waypoints.CurrentPoint;
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        if (Vector3.Distance(Agent.transform.position, currentTarget.position) <= 1)
        {
            Debug.Log("GOT TO POINT");
            return Status.FAILURE;  //SOULD NOT BE FAILURE, NEEDED TO TEST SOMETHING
        }                           //Implement ReverseNode;

        Vector3 velocity = (currentTarget.position - Agent.transform.position).normalized * this.speed;
        this.navMeshAgent.SetDestination(navMeshAgent.transform.position + velocity * Time.deltaTime);

        return Status.RUNNING;
    }
}
