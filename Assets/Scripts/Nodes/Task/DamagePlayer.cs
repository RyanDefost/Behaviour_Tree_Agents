using UnityEngine;

namespace Nodes.Task
{
    public class DamagePlayer : TaskNode
    {
        public DamagePlayer(BaseAgent baseAgent) : base(baseAgent)
        {
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            this.NodeName = $"{this.GetType().Name}";
        }

        public override Status OnUpdate()
        {
            return Status.SUCCESS;
        }
    }
}