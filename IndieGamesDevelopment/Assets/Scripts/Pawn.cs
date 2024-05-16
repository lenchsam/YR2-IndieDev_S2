using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private bool isBuilding = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("GoToHouse");
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
            anim.SetBool("isBuilding", true);
        else
            anim.SetBool("isBuilding", false);
    }
    private IEnumerable goToHouse()
    {
        return null;
    }
}
