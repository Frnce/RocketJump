using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletScript : MonoBehaviour
{
    public float speed;
    public bool onDetonate = false;
    Vector3 direction;
    Rigidbody2D rb2d;

    public void Setup(Vector3 _direction)
    {
        direction = _direction;
    }
    // Use this for initialization
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb2d.velocity = new Vector2(direction.x * speed, direction.y * speed);
        Destroy(gameObject, 1f);
        //TODO look for way for the player to knockback
    }
}
