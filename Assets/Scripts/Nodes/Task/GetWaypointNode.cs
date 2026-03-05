using UnityEngine;

public class GetWayPointNode : TaskNode
{
    private WaypointSystem waypoints;
    private Transform currentPoint;

    public GetWayPointNode(Agent agent, WaypointSystem waypoints) : base(agent)
    {
        this.waypoints = waypoints;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this.currentPoint = this.waypoints.CurrentPoint;
    }

    public override Status OnUpdate()
    {
        base.OnUpdate();

        Transform nextPoint = this.waypoints.NextPoint(this.currentPoint);
        this.waypoints.SetCurrentPoint(nextPoint);

        return Status.SUCCESS;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
