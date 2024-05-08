using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    [SerializeField] private float time;
    private float currentTime;
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            Destroy(gameObject);
        }
    }

}
