using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class Agent : MonoBehaviour
{
    public Blackboard blackboard = new();

    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] WaypointSystem waypoints;
    [SerializeField] NavMeshAgent navMeshAgent;

    [Space]
    [SerializeField] float speed;

    [Space]
    [SerializeField] Transform weaponTransform;
    public bool hasWeapon = false; //GET FROM BLACKBOARD!

    private Node baseBehaviour;
    private Vector3 playerPos = Vector3.positiveInfinity;

    private void Start()
    {
        UpdateSenses();
        CreateBehaviour();
    }

    private void Update()
    {
        UpdateSenses();
        baseBehaviour.Run();
    }

    private void UpdateSenses()
    {
        blackboard.SetValue("WEAPON", weaponTransform.position);
        blackboard.SetValue("PLAYER", fieldOfView.UpdateLastClosestVector(this.transform, playerPos));
        //blackboard.SetValue("PLAYER", fieldOfView.TryGetClosestVector(this.transform, 9, true));
        //blackboard.SetValue("PLAYERPOS", fieldOfView.TryGetClosestTransform(this.transform, 9, true));

        //blackboard.SetValue("HASWEAPON", hasWeapon);
    }

    private void CreateBehaviour()
    {
        baseBehaviour =
            new SelectorNode(
                new SelectorNode( // ATTACKING 'State'
                    new Node[] // preCon
                        { new TargetInView(this.fieldOfView, "PLAYER") },

                    new SequenceNode( // Get Weapon
                        new Node[] // preCon
                            { new InverterNode(new BBContainsNode<Vector3>("WEAPON")),
                              new InverterNode(new CheckConditionNode<bool>("HASWEAPON"))},

                        new PrintNode(this, "GET WEAPON"),
                        new MoveToTarget(this, this.navMeshAgent, "WEAPON", this.speed),
                        new PickupWeapon(this, "WEAPON"),
                        new MoveToTarget(this, this.navMeshAgent, "PLAYER", this.speed),
                        new WaitNode(this, 5f)
                    ),
                    
                    new SequenceNode( // Attack Player
                        new PrintNode(this, "ATTACK PLAYER"),
                        new MoveToTarget(this, this.navMeshAgent, "PLAYER", this.speed),
                        //  new DamageTarget()
                        new WaitNode(this, 1f)
                    )
                ),
                new SelectorNode( // PATROLLING 'State'
                    new Node[] // preCon
                        { new InverterNode(new TargetInView(this.fieldOfView, "PLAYER")) },

                    new MoveToNode(this, this.navMeshAgent, this.waypoints, this.speed),
                    new GetWayPointNode(this, this.waypoints),
                    new WaitNode(this, 1f) //Prob won't wait becuase of select node
                )
            );

        baseBehaviour.SetupBlackboard(this.blackboard);
    }
}