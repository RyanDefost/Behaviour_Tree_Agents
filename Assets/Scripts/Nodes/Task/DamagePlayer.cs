using UnityEngine;

namespace Nodes.Task
{
    public class DamagePlayer : TaskNode
    {
        private float timeInSeconds;
        private float timeRemaining;
        
        public DamagePlayer(Agent agent) : base(agent)
        {
            this.timeInSeconds = 2;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            this.timeRemaining = timeInSeconds;
        
            this.NodeName = $"{this.GetType().Name}";
        }

        public override Status OnUpdate()
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                return Status.RUNNING;
            }

            timeRemaining = timeInSeconds;
            return Status.SUCCESS;
        }
    }
}