using UnityEngine;

namespace Nodes.Task
{
    public class TrowSmokeBombNode : TaskNode
    {
        private Player player;
        private GameObject smokeBomb;
        
        public TrowSmokeBombNode(BaseAgent baseAgent) : base(baseAgent) { }

        public override void OnEnter()
        {
            this.player = this.Blackboard.GetValue<Player>("PLAYER");
            this.smokeBomb = this.Blackboard.GetValue<GameObject>("SMOKE_BOMB");
        }

        public override Status OnUpdate()
        {
            Vector3 attackerPos = this.player.attacker.transform.position;
            
            float randomPosX = Random.Range(attackerPos.x - 1, attackerPos.x + 1);
            float randomPosZ = Random.Range(attackerPos.z - 1, attackerPos.z + 1);
            Vector3 throwPosition = new (randomPosX, 0, randomPosZ);
            
            Object.Instantiate(this.smokeBomb, throwPosition, Quaternion.identity);
            return Status.SUCCESS;
        }
    }
}