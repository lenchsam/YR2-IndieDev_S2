using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class DontDeleteOnLoad : MonoBehaviour
{
    static DontDeleteOnLoad instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
