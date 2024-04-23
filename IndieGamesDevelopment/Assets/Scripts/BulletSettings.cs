using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletSettings : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Rigidbody2D rb;
    public Vector3 direction;
    //destroy bullet when it hits an enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
            Destroy(gameObject);
    }
    //destroy bullet when it goes off the screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FireBullet()
    {
        rb.velocity = (direction - transform.position) * Speed;
    }
}
