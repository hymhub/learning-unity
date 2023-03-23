using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime // 上一帧花费的时间
        // Debug.Log(1 / Time.deltaTime); // 帧率 FPS
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (transform.position.y < 0.5 && v < 0 || transform.position.y > 10 && v > 0)
        {
            v = 0;
        }

        if (transform.position.x < -10 && h > 0 || transform.position.x > 10 && h < 0)
        {
            h = 0;
        }
        // 运动距离 = 速度 * 时间
        transform.Translate(new Vector3(h, v, 0) * (speed * Time.deltaTime));
    }
}
