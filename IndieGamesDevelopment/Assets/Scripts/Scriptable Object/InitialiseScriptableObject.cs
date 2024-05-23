using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseScriptableObject : MonoBehaviour
{
    [SerializeField] private WaveScriptableObject SO_pawnLocations;
    [SerializeField] private AmountOfPawnsScriptableObject SO_amountOfPawns;
    // Start is called before the first frame update
    void Start()
    {
        SO_pawnLocations.gameObjectList.Clear();
        SO_amountOfPawns.amountOfPawns = 0;
    }
}
