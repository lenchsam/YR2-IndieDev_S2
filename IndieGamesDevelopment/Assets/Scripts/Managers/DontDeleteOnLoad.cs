using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class DontDeleteOnLoad : MonoBehaviour
{
    static DontDeleteOnLoad instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
