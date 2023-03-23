using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rd;

    public int score = 0;

    public Text scoreText;
    
    public GameObject winText;

    private bool upIsDown = false;
    private bool downIsDown = false;
    private bool leftIsDown = false;
    private bool rightIsDown = false;
    public void OnUpDown()
    {
        upIsDown = true;
    }
    public void OnUpUp()
    {
        upIsDown = false;
    }
    public void OnDownDown()
    {
        downIsDown = true;
    }
    public void OnDownUp()
    {
        downIsDown = false;
    }
    public void OnLeftDown()
    {
        leftIsDown = true;
    }
    public void OnLeftUp()
    {
        leftIsDown = false;
    }
    public void OnRightDown()
    {
        rightIsDown = true;
    }
    public void OnRightUp()
    {
        rightIsDown = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // rd = GetComponent<Rigidbody>();
        // Debug.Log(rd);
    }

    // Update is called once per frame
    void Update()
    {
        // rd.AddForce(Vector3.right);
        // rd.AddForce(new Vector3(2, 0, 0));
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rd.AddForce(new Vector3(h, 0, v) * 5);
        if (upIsDown)
        {
            rd.AddForce(Vector3.forward * 5); 
        }
        if (downIsDown)
        {
            rd.AddForce(Vector3.back * 5);
        }
        if (leftIsDown)
        {
            rd.AddForce(Vector3.left * 5);
        }
        if (rightIsDown)
        {
            rd.AddForce(Vector3.right * 5);
        }
    }
    
    // // 碰撞发生
    // private void OnCollisionEnter(Collision collision)
    // {
    //     // collision.collider.tag
    //     if (collision.gameObject.CompareTag("Food"))
    //     {
    //         collision.gameObject.SetActive(false);
    //     }
    // }
    //
    // // 正在碰撞，持续触发
    // private void OnCollisionStay(Collision collisionInfo)
    // {
    //     
    // }
    //
    // // 碰撞离开
    // private void OnCollisionExit(Collision other)
    // {
    //     
    // }

    // 触发发生（进入区域）
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            other.gameObject.SetActive(false);
            scoreText.text = "分数: " + ++score * 10;
            if (score >= 14)
            {
                winText.SetActive(true);
            }
        }
    }
    // 持续触发中
    private void OnTriggerStay(Collider other)
    {
        
    }
    // 触发离开
    private void OnTriggerExit(Collider other)
    {
        
    }
}
