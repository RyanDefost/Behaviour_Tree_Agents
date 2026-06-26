using Nodes.Decorator;
using Nodes.Task;
using UnityEngine;

public class NinjaAgent : BaseAgent
{
    [SerializeField] private Player player;
    
    //Behaviour
    private Node baseBehaviour;
    
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 hidingSpotPosition;
    
    private bool isHiding;
    private bool isPlayerAttacked;

    private void Start()
    {
        stateDisplay = GetComponent<StateDisplay>();
        
        UpdateSenses();
        CreateBehaviour();
    }

    private void Update()
    {
        UpdateSenses();
        baseBehaviour.Run();
        
        nodeNames = baseBehaviour.GetName();
        stateDisplay.SetDisplay(this, nodeNames);
    }

    protected override void UpdateSenses()
    {
        blackboard.SetValue("PLAYER_POSITION", fieldOfView.TryGetClosestVector(this.transform, 9));
        this.playerPosition = blackboard.GetValue<Vector3>("PLAYER_POSITION");
        
        blackboard.SetValue("HIDE_SPOT", this.fieldOfView.TryGetClosestVector(this.transform, 10));
        this.hidingSpotPosition = blackboard.GetValue<Vector3>("HIDE_SPOT");
        
        blackboard.SetValue("PLAYER_IS_ATTACKED", this.player.isAttacked);
        this.isPlayerAttacked = blackboard.GetValue<bool>("PLAYER_IS_ATTACKED");
        
        blackboard.SetValue("IS_HIDING", Vector3.Distance(this.transform.position, this.fieldOfView.TryGetClosestVector(this.transform, 10)) < 1f);
        this.isHiding = blackboard.GetValue<bool>("IS_HIDING");
    }

    protected override void CreateBehaviour()
    {
        baseBehaviour =
            new SelectorNode(
                new SelectorNode( //HIDE STATE
                    new Node[] // preCon
                        { new CheckConditionNode<bool>("PLAYER_IS_ATTACKED") },
                    
                    new SequenceNode( //FIND HIDE STATE
                        new Node[] //preCon
                            { new InverterNode(new CheckConditionNode<bool>("IS_HIDING")) },

                        new MoveToTarget(this, this.navMeshAgent, "HIDE_SPOT", 5),
                        new SetBBCondition(this, "IS_HIDING", true)
                    ),
                    
                    new SequenceNode( //ASSIST STATE
                        new WaitNode(this, 3f),
                        new PrintNode(this, "THROWING BOMBS")
                        //new TrowSmokeBomb(this) 
                    )
                ),
                
                new SequenceNode( //FOLLOW STATE
                    new Node[] //preCon
                        { new InverterNode(new CheckDistanceNode(this, "PLAYER_POSITION", 3)) },
                    
                    new SetBBCondition(this, "IS_HIDING", false),
                    new MoveToTarget(this, this.navMeshAgent, "PLAYER_POSITION", 5)
                )
            );

        baseBehaviour.SetupBlackboard(this.blackboard);
    }
}