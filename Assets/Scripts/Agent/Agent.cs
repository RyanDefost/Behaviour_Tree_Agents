using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class Agent : MonoBehaviour
{
    public Blackboard blackboard = new();

    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] WaypointSystem waypoints;
    [SerializeField] NavMeshAgent navMeshAgent;

    [Space]
    [SerializeField] float speed;

    private Node baseBehaviour;

    private void Start()
    {
        CreateBehaviour();
    }

    private void Update()
    {
        baseBehaviour.Run();
    }

    private void CreateBehaviour()
    {
        baseBehaviour =
            new SelectorNode(
                new MoveToNode(this, this.navMeshAgent, this.waypoints, this.speed),
                new GetWayPointNode(this, this.waypoints)
            );

        baseBehaviour.SetupBlackboard(this.blackboard);
    }
}