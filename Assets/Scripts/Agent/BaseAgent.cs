using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAgent : MonoBehaviour
{
    public Blackboard blackboard = new();

    [SerializeField] protected FieldOfView fieldOfView;
    [SerializeField] protected WaypointSystem waypoints;
    [SerializeField] protected NavMeshAgent navMeshAgent;

    protected StateDisplay StateDisplay;
    protected string NodeNames;
    
    protected abstract void UpdateSenses();
    
    protected abstract void CreateBehaviour();
}