using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 模长控制子弹消失
        if (transform.position.magnitude > 50)
        {
            Destroy(gameObject);
        }
    }

    // 子弹开始向前运动
    public void Launch(Vector2 direction, float force)
    {
        _rigidbody2D.AddForce(direction * force);
    }

    // 碰撞检测
    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        if (col.gameObject.CompareTag("Robot"))
        {
            var enemyController = col.gameObject.GetComponent<EnemyController>();
            enemyController.Fix();
        }
    }
}
