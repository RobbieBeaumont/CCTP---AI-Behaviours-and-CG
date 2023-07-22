using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

// Class Authored by Robbie Beaumont
// Group behaviour tree class.

public class EnemyGroupBT : BehaviourTree.Tree
{
    public EnemyGroupState startingGroupState;

    public List<EnemyBT> _enemies = new List<EnemyBT>();
    public List<Transform> _defaultTargets = new List<Transform>();
    protected EnemyGroupState _groupState;

    private void Awake()
    {
        foreach (Transform child in this.transform)
        {
            foreach (Transform childTransform in child)
            {
                if (childTransform.CompareTag("Waypoint"))
                {
                    _defaultTargets.Add(childTransform);
                }
            }

            if (child.gameObject.CompareTag("Enemy"))
            {
                _enemies.Add(child.GetComponent<EnemyBT>());
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].SetDefaultTarget(_defaultTargets[i]);
            }
        }
    }

    // Constructs the group behaviour tree.
    protected override Node SetupTree()
    {
        Node rootChildNode = new Selector(new List<Node>
        {
            new Sequencer(new List<Node>
            {
                new ConditionCheckAllEnemiesState(_enemies, EnemyState.IDLE),
                new ActionChangeEnemyState(_enemies, EnemyState.HIDING)
            })
        });
        Debug.Log("Generating Group Enemy Behaviour Tree");
        return rootChildNode;
    }
}

public enum EnemyGroupState
{
    PASSIVE,
    AGGRESSIVE
}

