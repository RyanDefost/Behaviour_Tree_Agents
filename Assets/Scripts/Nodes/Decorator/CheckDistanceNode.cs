using UnityEngine;

namespace Nodes.Decorator
{
    public class CheckDistanceNode : DecoratorNode
    {
        private readonly BaseAgent agent;
        private readonly string targetPosition;
        
        private readonly float distance;
        
        public CheckDistanceNode(BaseAgent agent, string BBKey, float distance)
        {
            this.agent = agent;
            this.targetPosition = BBKey;
            this.distance = distance;
        }
        
        public override Status OnUpdate()
        {
            return Vector3.Distance(this.agent.transform.position, Blackboard.GetValue<Vector3>(this.targetPosition)) < this.distance 
                ? Status.SUCCESS : Status.FAILURE;
        }
    }
}