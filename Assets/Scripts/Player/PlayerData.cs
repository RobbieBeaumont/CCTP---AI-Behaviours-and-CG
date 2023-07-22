using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont

public class PlayerData : MonoBehaviour
{
    public int playerHealth = 100;
    public int attackDamage = 20;
    public float attackRange = 5.0f;
    public float attackCooldown = 0.5f;
    public float defenseDuration = 2.0f;
    public float defenseCooldown = 5.0f;
    public float defenseDamageReduction = 0.25f;
    [Space]
    public float GizmoSphereHeight = 2.0f;
    public float GizmoSphereRadius = 0.3f;

    private bool _attackReady = true;
    private bool _defenseReady = true;
    private int _currentHealth;
    private float _attackCooldownCount;
    private float _defenseCount;
    private float _defenseCooldownCount;

    private PlayerState _playerState = PlayerState.STANDING;
    private PlayerController _playerControls;
    private static int _enemyLayerMask = 1 << 2;

    public PlayerState GetPlayerState() => _playerState;
    public int GetCurrentHealth() => _currentHealth;

    void Awake()
    {
        _currentHealth = playerHealth;
        _playerControls = transform.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_currentHealth <= 0)
        {
            PlayerDeath();
        }

        if (_attackReady)
        {
            if (_playerControls.GetAttackPressed())
            {
                Attack();
            }
        }
        else 
        {
            AttackCooldown();
        }

        if (_playerControls.GetDefendPressed())
        {
            if (_defenseReady)
            {
                Defend();
            }
        }
        else
        {
            _playerState = PlayerState.STANDING;
        }
        if (!_defenseReady) 
        {
            DefendCooldown();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Vector3 gizmoPos = new Vector3(pos.x, pos.y + GizmoSphereHeight, pos.z);

        if (_playerState == PlayerState.STANDING)
        {
            Gizmos.color = Color.grey;
        }
        else if (_playerState == PlayerState.ATTACKING)
        {
            Gizmos.color = Color.red;
        }
        else if (_playerState == PlayerState.DEFENDING)
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawSphere(gizmoPos, GizmoSphereRadius);
    }

    //Attacks enemies within a given range.
    private void Attack()
    {
        _playerState = PlayerState.ATTACKING;
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, _enemyLayerMask);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                collider.transform.GetComponent<EnemyData>().TakeDamage(attackDamage);
            }
        }
        _attackReady = false;
        _playerState = PlayerState.STANDING;
    }

    //Defends using a block mechanic, limited usage.
    private void Defend()
    {
        _playerState = PlayerState.DEFENDING;
        _defenseCount += Time.deltaTime;
        if (_defenseCount >= defenseDuration)
        {
            _defenseReady = false;
            _playerState = PlayerState.STANDING;
            _defenseCount = 0;
        }
    }

    //Allows the player to take damage.
    public void TakeDamage(int attackDamage)
    {
        if (_playerState != PlayerState.DEFENDING)
        {
            _currentHealth -= attackDamage;
        }
        Debug.Log("Player Has Been Hit");
    }

    //Cooldown before next attack.
    private void AttackCooldown()
    {
        _attackCooldownCount += Time.deltaTime;
        if (_attackCooldownCount >= attackCooldown)
        {
            _attackReady = true;
            _attackCooldownCount = 0;
        }
    }

    //Cooldown before next defend.
    public void DefendCooldown()
    {
        _defenseCooldownCount += Time.deltaTime;
        if (_defenseCooldownCount >= defenseCooldown)
        {
            _defenseReady = true;
            _defenseCooldownCount = 0;
        }
    }

    private void PlayerDeath()
    {
        Destroy(transform.gameObject);
        Debug.Log("YOU HAVE DIED");
    }
}

public enum PlayerState
{
    STANDING,
    ATTACKING,
    DEFENDING
}
