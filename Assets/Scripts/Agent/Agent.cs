using Unity.VisualScripting;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Blackboard blackboard = new();

    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private WaypointSystem waypoints;
    [SerializeField] private new Rigidbody rigidbody;

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
                new MoveToNode(this, this.waypoints, this.rigidbody, 6),
                new GetWayPointNode(this, this.waypoints)
            );

        baseBehaviour.SetupBlackboard(blackboard);
    }
}

///
/// What states should it have
/// Go to position
/// Pickup item
/// Look for ItemType
/// Follow path
/// Attack "object"