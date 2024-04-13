using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveScriptableObject", menuName = "ScriptableObjects/WaveScriptableObject")]
public class WaveScriptableObject : ScriptableObject
{
    public List<GameObject> Enemies = new List<GameObject>();
}
