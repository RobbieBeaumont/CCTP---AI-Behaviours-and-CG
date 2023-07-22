using UnityEngine;
using UnityEditor;

// Class Authored by Robbie Beaumont


[CustomEditor(typeof(EnemyManager))]
public class EnemyManageCI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyManager enemyManagerScript = (EnemyManager)target;

        if(GUILayout.Button("Spawn Enemies"))
        {
            enemyManagerScript.SpawnEnemies();
        }
    }
}