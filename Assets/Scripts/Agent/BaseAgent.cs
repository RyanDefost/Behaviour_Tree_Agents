using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAgent : MonoBehaviour
{
    public Blackboard blackboard = new();

    [SerializeField] protected FieldOfView fieldOfView;
    [SerializeField] protected WaypointSystem waypoints;
    [SerializeField] protected NavMeshAgent navMeshAgent;

    public StateDisplay stateDisplay;
    public string nodeNames;
    
    protected abstract void UpdateSenses();
    
    protected abstract void CreateBehaviour();
}