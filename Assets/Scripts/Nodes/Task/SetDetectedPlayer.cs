using UnityEngine;

namespace Nodes.Task
{
    public class SetDetectedPlayer : TaskNode
    {
        private FieldOfView fov;
        
        public SetDetectedPlayer(BaseAgent baseAgent, FieldOfView fov) : base(baseAgent)
        {
            this.fov = fov;
        }

        public override Status OnUpdate()
        {
            if (fov.visibleTargets.Count < 0)
            {
                Blackboard.SetValue("DETECTEDPLAYER", false);
                return Status.SUCCESS;
            }

            return Status.FAILURE;
        }
    }
}