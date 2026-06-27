using UnityEngine;

namespace Nodes.Task
{
    public class DamagePlayer : TaskNode
    {
        Player player;
        
        public DamagePlayer(BaseAgent baseAgent, Player player) : base(baseAgent)
        {
            this.player = player;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            this.NodeName = $"{this.GetType().Name}";
        }

        public override Status OnUpdate()
        {
            Debug.Log("HURT");
            this.player.AttackPlayer(this.BaseAgent);
            return Status.SUCCESS;
        }
    }
}