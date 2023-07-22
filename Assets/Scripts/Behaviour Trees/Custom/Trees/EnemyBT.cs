using UnityEngine;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine.AI;

// Class Authored by Robbie Beaumont
// Enemy Behaviour tree class.

public class EnemyBT : BehaviourTree.Tree
{
    public GameObject player;
    public HidingSpots hidingSpots;
    public Transform defaultTarget;
    public float awakeRange = 5.0f;
    public float attackRange = 3.0f;

    [Space]

    public float debugSphereSize = 0.3f;
    public float debugSphereHeight = 2.0f;

    protected EnemyState _enemyState = EnemyState.IDLE;
    private bool _isAwake = false;
    private NavMeshAgent _navMeshAgent;

    public void SetDefaultTarget(Transform defaultTarget) => this.defaultTarget = defaultTarget;
    public EnemyState GetEnemyState() => _enemyState;
    public void SetEnemyState(EnemyState state) => _enemyState = state;
    public bool GetIsAwake() => _isAwake;
    public void SetIsAwake(bool isAwake) => _isAwake = isAwake;


    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    //Constructs the behaviour tree.
    protected override Node SetupTree()
    {
        Node rootChildNode = new Selector(new List<Node>
        {
            new Sequencer(new List<Node>
            {
                new ConditionCompareEnemyStates(transform, EnemyState.IDLE),
                new Sequencer(new List<Node>
                {
                    new Inverter(
                        new ConditionIsPlayerInRange(transform, awakeRange, EnemyState.HIDING)),
                    new ActionWaitInSeconds(1, this),
                    new Success(
                        new ActionGenerateDestination(transform, defaultTarget, transform.GetComponent<EnemyData>().randomWalkRange)),
                    new ActionTravelToTarget(transform, _navMeshAgent, 0.01f)
                })
            }),
            new Sequencer(new List<Node> 
            {
                new ConditionCompareEnemyStates(transform, EnemyState.HIDING),
                new Sequencer(new List<Node> 
                {
                    new ActionWaitInSeconds(1, this),
                    new ConditionLineOfSightToWaypoint(transform, player, _navMeshAgent, hidingSpots),
                    new ActionTravelToTarget(transform, _navMeshAgent, 0.01f),
                    new ActionChanceToChangeState(transform, EnemyState.ATTACKING, 20)
                })
            }),
            new Sequencer(new List<Node>
            {
                new ConditionCompareEnemyStates(transform, EnemyState.ATTACKING),
                new Sequencer(new List<Node>
                {
                    new ActionWaitInSeconds(1, this),
                    new Success(
                        new ActionUpdateTarget(transform, player.transform)),
                    new Selector(new List<Node>
                    {
                        new Inverter(new ConditionCheckPlayerState(player.transform.GetComponent<PlayerData>(), PlayerState.DEFENDING)),
                        new Inverter(new ActionChanceToChangeState(transform, EnemyState.DELAY, 100))
                    }),
                    new ActionTravelToTarget(transform, _navMeshAgent, attackRange),
                    new ConditionIsPlayerInRange(transform, attackRange),
                    new ActionAttackTarget(player.transform.GetComponent<PlayerData>(), transform, attackRange)
                })
            }),
            new Sequencer(new List<Node>
            {
                new ConditionCompareEnemyStates(transform, EnemyState.DELAY),
                new ActionWaitInSeconds(1f, this),
                new Selector(new List<Node>
                {
                    new ActionChanceToChangeState(transform, EnemyState.HIDING, 50),
                    new ActionChanceToChangeState(transform, EnemyState.ATTACKING, 100)
                })
            })
        });
        Debug.Log("Generating Enemy Behaviour Tree");
        return rootChildNode;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Vector3 gizmoPos = new Vector3(pos.x, pos.y + debugSphereHeight, pos.z);
        if (_enemyState == EnemyState.IDLE)
        {
            Gizmos.color = Color.magenta;
        }
        else if(_enemyState == EnemyState.HIDING)
        {
            Gizmos.color = Color.yellow;
        }
        else if(_enemyState == EnemyState.ATTACKING)
        {
            Gizmos.color = Color.red;
        }
        else if(_enemyState == EnemyState.DELAY)
        {
            Gizmos.color = Color.black;
        }

        if (_isWaiting)
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawSphere(gizmoPos, debugSphereSize);
    }
}

public enum EnemyState
{
    NULL,
    IDLE,
    ATTACKING,
    HIDING,
    DELAY,
    DEFENDING
}
