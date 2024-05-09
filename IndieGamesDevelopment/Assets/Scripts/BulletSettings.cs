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
    public Transform target;
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
        rotateBulletToTarget(target);
    }

    public void FireBullet()
    {
        rb.velocity = (direction - transform.position) * Speed;
    }
    private void rotateBulletToTarget(Transform target)
    {
        //keep rotating the defence in the direction of the target (the enemy)
        Quaternion rotation = Quaternion.LookRotation(target.transform.position - gameObject.transform.position, transform.TransformDirection(Vector3.forward));
        gameObject.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
}
