using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont

public class EnemyData : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float randomWalkRange = 5.0f;
    public int enemyHealth = 100;
    public int attackDamage = 10;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = enemyHealth;
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public int GetAttackDamage() => attackDamage;

    //Lets enemy take damage.
    public void TakeDamage(int attackDamage)
    {
        _currentHealth -= attackDamage;
        Debug.Log("Enemy has been hit");
    }

    private void EnemyDeath()
    {
        Destroy(transform.parent.gameObject);
    }
}
