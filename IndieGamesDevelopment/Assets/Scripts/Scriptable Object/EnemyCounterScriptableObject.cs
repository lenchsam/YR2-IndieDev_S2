using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCounter", menuName = "ScriptableObjects/EnemyCounter")]
public class EnemyCounterScriptableObject : ScriptableObject
{
    public int numberOfEnemies;
    public bool finishedSpawning;
}
