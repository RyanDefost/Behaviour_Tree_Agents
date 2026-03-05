using UnityEngine;

public class MoveToNode : TaskNode
{
    private WaypointSystem waypoints;
    private Transform currentTarget;

    private Rigidbody rigidbody;
    private float speed;

    public MoveToNode(Agent agent, WaypointSystem waypoints, Rigidbody rigidbody, float speed) : base(agent)
    {
        this.waypoints = waypoints;
        this.rigidbody = rigidbody;
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
        this.rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);

        return Status.RUNNING;
    }
}
