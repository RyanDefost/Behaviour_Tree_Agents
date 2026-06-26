namespace Nodes.Task
{
    public class SetBBCondition : TaskNode
    {
        private string condition;
        private bool setState;
        
        public SetBBCondition(BaseAgent baseAgent, string BBKey, bool state) : base(baseAgent)
        {
            this.condition = BBKey;
            this.setState = state;
        }

        public override Status OnUpdate()
        {
            this.Blackboard.SetValue<bool>(this.condition, this.setState);
            return Status.SUCCESS;
        }
    }
}