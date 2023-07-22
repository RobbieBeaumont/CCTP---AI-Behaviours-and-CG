using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class Authored by Robbie Beaumont


public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPointTransforms;
    public GameObject[] spawnPoints;
    public GameObject enemy;

    void Start()
    {
        spawnPointTransforms = gameObject.GetComponentsInChildren<Transform>();

        int array_length = 0;

        for (int i = 0; i < spawnPointTransforms.Length; i++)
        {
            if (spawnPointTransforms[i].CompareTag("EnemySpawn"))
            {
                array_length++;
            }
        }

        spawnPoints = new GameObject[array_length];

        int available_index = 0;

        for (int i = 0; i < spawnPointTransforms.Length; i++)
        {
            if (spawnPointTransforms[i].CompareTag("EnemySpawn"))
            {
                spawnPoints[available_index] = spawnPointTransforms[i].gameObject;
                available_index++;
            }
        }
    }

    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 position = spawnPoints[i].transform.position;
            position.y++;
            Instantiate(enemy, position, Quaternion.identity);
        }
    }
}
