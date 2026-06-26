using System.Collections.Generic;
using Nodes.Task;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class GuardAgent : BaseAgent
{
    [Space, SerializeField] float speed;
    
    [Space, SerializeField] Transform weaponTransform;
    public bool hasWeapon;
    [SerializeField] private Transform playerPos;
    public bool detectingPlayer;
    
    private Node baseBehaviour;

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
        stateDisplay.SetDisplay(this, nodeNames, new [] { "HASWEAPON", "DETECTEDPLAYER" });
    }

    protected override void UpdateSenses()
    {
        blackboard.SetValue("PLAYER", fieldOfView.TryGetClosestVector(this.transform, 9));
        
        blackboard.SetValue("WEAPON", weaponTransform.position);
        this.hasWeapon = this.blackboard.GetValue<bool>("HASWEAPON");
        
        this.detectingPlayer = blackboard.GetValue<bool>("DETECTEDPLAYER");
        blackboard.SetValue("DETECTEDPLAYER", fieldOfView.DetectingPlayer(playerPos, detectingPlayer));
    }

    protected override void CreateBehaviour()
    {
        baseBehaviour =
            new SelectorNode(
                new SelectorNode( // ATTACKING 'State'
                    new Node[] // preCon
                        { new CheckConditionNode<bool>("DETECTEDPLAYER") },

                    new SequenceNode( // Get Weapon
                        new Node[] // preCon
                        {
                            new InverterNode(new BBContainsNode<Vector3>("WEAPON")),
                            new InverterNode(new CheckConditionNode<bool>("HASWEAPON"))
                        },

                        new PrintNode(this, "GET WEAPON"),
                        new MoveToTarget(this, this.navMeshAgent, "WEAPON", this.speed),
                        new PickupWeapon(this, "WEAPON")
                    ),

                    new SequenceNode( // Attack Player
                        new PrintNode(this, "ATTACK PLAYER"),
                        new MoveToTarget(this, this.navMeshAgent, "PLAYER", this.speed),
                        
                        new WaitNode(this, 1f),
                        new SetDetectedPlayer(this, this.fieldOfView),
                        new DamagePlayer(this)
                    )
                ),
                
                new SelectorNode( // PATROLLING 'State'
                    new Node[] // preCon
                        { new InverterNode(new TargetInView(this.fieldOfView, "PLAYER")) },

                    new MoveToNode(this, this.navMeshAgent, this.waypoints, this.speed),
                    new GetWayPointNode(this, this.waypoints),
                    new WaitNode(this, 1f)
                )
            );

        baseBehaviour.SetupBlackboard(this.blackboard);
    }
}